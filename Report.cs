using System.Globalization;

namespace ECommerceConsole;

/// <summary>
/// Laporan penjualan. Fitur untuk admin: ringkasan toko, laporan penjualan harian,
/// dan laba/rugi per transaksi (bisa di-export ke CSV). Fitur customer: riwayat sendiri
/// (tanpa informasi modal/laba).
/// </summary>
public static class Report
{
    // ---------- Helper tampilan ----------

    /// <summary>Format Rupiah; angka negatif tampil rapi sebagai "-Rp1,000".</summary>
    private static string Rp(decimal value) =>
        (value < 0 ? "-Rp" : "Rp") + Math.Abs(value).ToString("N0");

    private static string Status(decimal profit) =>
        profit > 0 ? "UNTUNG" : profit < 0 ? "RUGI" : "IMPAS";

    private static string Num(decimal value) =>
        value.ToString(CultureInfo.InvariantCulture);

    private static void PrintSummary(List<Order> orders)
    {
        decimal revenue = orders.Sum(o => o.Total);
        decimal cost = orders.Sum(o => o.TotalCost);
        decimal profit = revenue - cost;
        int itemsSold = orders.Sum(o => o.Items.Sum(i => i.Quantity));
        int profitCount = orders.Count(o => o.Profit > 0);
        int lossCount = orders.Count(o => o.Profit < 0);

        Console.WriteLine("\n  ---------- RINGKASAN ----------");
        Console.WriteLine($"  Jumlah transaksi    : {orders.Count} ({profitCount} untung, {lossCount} rugi)");
        Console.WriteLine($"  Total item terjual  : {itemsSold}");
        Console.WriteLine($"  Total pendapatan    : {Rp(revenue)}");
        Console.WriteLine($"  Total modal (HPP)   : {Rp(cost)}");
        Console.WriteLine($"  LABA/RUGI BERSIH    : {Rp(profit)}  [{Status(profit)}]");

        if (orders.Any(o => o.HasUnknownCost))
            Console.WriteLine("\n  ! Catatan: sebagian item belum punya harga modal (0), laba bisa lebih besar dari sebenarnya.");
    }

    // ---------- 1. Ringkasan seluruh toko ----------

    public static void GenerateSalesReport()
    {
        var orders = FileHandler.LoadOrders();

        Console.WriteLine("\n=== Laporan Penjualan Toko (Semua Waktu) ===");
        if (orders.Count == 0)
        {
            Console.WriteLine("  Belum ada transaksi.");
            return;
        }

        PrintSummary(orders);

        Console.WriteLine("\n  Rincian per transaksi:");
        foreach (var o in orders.OrderByDescending(o => o.Date))
        {
            Console.WriteLine($"   - {o.OrderId} | {o.Username,-12} | {o.Date:dd-MM-yyyy} | {Rp(o.Total)}");
        }
    }

    // ---------- 2. Laporan penjualan harian ----------

    /// <summary>
    /// Tarik laporan penjualan untuk satu hari (default hari ini):
    /// daftar transaksi, pendapatan, modal, dan laba/rugi hari itu.
    /// </summary>
    public static void GenerateDailySalesReport()
    {
        DateTime date = InputHelper.ReadDateOrToday("Tanggal laporan (dd-MM-yyyy, kosong = hari ini): ");

        var orders = FileHandler.LoadOrders()
            .Where(o => o.Date.Date == date.Date)
            .OrderBy(o => o.Date)
            .ToList();

        Console.WriteLine($"\n=== Laporan Penjualan Harian: {date:dd-MM-yyyy} ===");
        if (orders.Count == 0)
        {
            Console.WriteLine("  Tidak ada transaksi pada tanggal ini.");
            return;
        }

        Console.WriteLine($"  {"Jam",-6} {"No. Transaksi",-18} {"Pelanggan",-12} {"Pendapatan",14} {"Modal",14} {"Laba/Rugi",14}  Status");
        Console.WriteLine("  " + new string('-', 88));
        foreach (var o in orders)
        {
            Console.WriteLine($"  {o.Date:HH:mm}  {o.OrderId,-18} {o.Username,-12} {Rp(o.Total),14} {Rp(o.TotalCost),14} {Rp(o.Profit),14}  {Status(o.Profit)}");
        }

        PrintSummary(orders);

        if (InputHelper.Confirm("\nExport laporan ini ke CSV?"))
        {
            var lines = new List<string> { "orderid,jam,pelanggan,pendapatan,modal,laba_rugi,status" };
            lines.AddRange(orders.Select(o =>
                string.Join(",", o.OrderId, o.Date.ToString("HH:mm"), o.Username,
                    Num(o.Total), Num(o.TotalCost), Num(o.Profit), Status(o.Profit))));

            decimal revenue = orders.Sum(o => o.Total);
            decimal cost = orders.Sum(o => o.TotalCost);
            lines.Add(string.Join(",", "TOTAL", "", "", Num(revenue), Num(cost), Num(revenue - cost), Status(revenue - cost)));

            string path = FileHandler.SaveReportFile($"laporan_harian_{date:yyyy-MM-dd}.csv", lines);
            Console.WriteLine($"  -> Laporan disimpan di: {path}");
        }
    }

    // ---------- 3. Untung/rugi setiap transaksi ----------

    /// <summary>
    /// Tampilkan laba/rugi tiap transaksi beserta rincian per item.
    /// Isi nomor transaksi untuk satu transaksi saja, kosongkan untuk semua.
    /// </summary>
    public static void GenerateProfitLossReport()
    {
        string orderId = InputHelper.ReadOptionalString("No. transaksi (kosongkan untuk semua): ");

        var orders = FileHandler.LoadOrders()
            .Where(o => string.IsNullOrWhiteSpace(orderId) ||
                        o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase))
            .OrderBy(o => o.Date)
            .ToList();

        Console.WriteLine("\n=== Laporan Untung/Rugi per Transaksi ===");
        if (orders.Count == 0)
        {
            Console.WriteLine("  Transaksi tidak ditemukan.");
            return;
        }

        foreach (var o in orders)
        {
            Console.WriteLine($"\n  {o.OrderId} | {o.Date:dd-MM-yyyy HH:mm} | pelanggan: {o.Username}");
            foreach (var item in o.Items)
            {
                Console.WriteLine($"     {item.Product.ProductId,-6} x{item.Quantity,-3} jual {Rp(item.LineTotal),12}  modal {Rp(item.LineCost),12}  selisih {Rp(item.LineTotal - item.LineCost),12}");
            }

            decimal discountAmount = o.Subtotal - o.Total;
            Console.WriteLine($"     Diskon {o.DiscountPercent}%                                      -{Rp(discountAmount)}");
            Console.WriteLine($"     Pendapatan {Rp(o.Total)} - Modal {Rp(o.TotalCost)} = {Rp(o.Profit)}  [{Status(o.Profit)}]");

            if (o.HasUnknownCost)
                Console.WriteLine("     ! Ada item tanpa harga modal.");
        }

        PrintSummary(orders);

        if (InputHelper.Confirm("\nExport laporan ini ke CSV?"))
        {
            var lines = new List<string> { "orderid,tanggal,pelanggan,subtotal,diskon_persen,pendapatan,modal,laba_rugi,status" };
            lines.AddRange(orders.Select(o =>
                string.Join(",", o.OrderId, o.Date.ToString("yyyy-MM-dd HH:mm"), o.Username,
                    Num(o.Subtotal), Num(o.DiscountPercent), Num(o.Total),
                    Num(o.TotalCost), Num(o.Profit), Status(o.Profit))));

            string path = FileHandler.SaveReportFile($"laba_rugi_transaksi_{DateTime.Now:yyyyMMdd_HHmmss}.csv", lines);
            Console.WriteLine($"  -> Laporan disimpan di: {path}");
        }
    }

    // ---------- Riwayat customer (tanpa info modal) ----------

    /// <summary>Tampilkan riwayat transaksi milik satu username saja (dipakai customer).</summary>
    public static void ShowUserHistory(string username)
    {
        var orders = FileHandler.LoadOrders()
            .Where(o => o.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(o => o.Date)
            .ToList();

        Console.WriteLine("\n=== Riwayat Transaksi Saya ===");
        if (orders.Count == 0)
        {
            Console.WriteLine("  Belum ada transaksi.");
            return;
        }

        foreach (var o in orders)
        {
            Console.WriteLine($"   - {o.OrderId} | {o.Date:dd-MM-yyyy HH:mm} | {Rp(o.Total)} (diskon {o.DiscountPercent}%)");
        }
    }
}
