using System;
class Report
{
    // Transaction hanya menyimpan userId & productId (angka), jadi nama dicari manual lewat loop
    static string getUserNameById(int id)
    {
        for (int i = 0; i < Users.userCount; i++)
        {
            if (Users.userId[i] == id)
            {
                return Users.userName[i];
            }
        }
        return "-";
    }

    static string getProductName(int id)
    {
        int idx = Products.getIndexProductByID(id);
        if (idx == -1)
        {
            return "(produk tidak ada)";
        }
        return Products.name[idx];
    }

    static string getStatus(double profit)
    {
        if (profit > 0) return "Untung";
        if (profit < 0) return "Rugi";
        return "Impas";
    }

    // Menu 6 (Admin): riwayat transaksi semua pembeli + untung/rugi tiap transaksi
    public static void GenerateSalesReport()
    {
        Console.Clear();
        Console.WriteLine("=== Laporan Penjualan ===");
        Console.WriteLine(Program.lines35);

        if (Transaction.transactionCount == 0)
        {
            Console.WriteLine("Belum ada transaksi.");
            Console.ReadLine();
            return;
        }

        double grandTotal = 0;
        double grandCost = 0;
        int trxCount = 0;
        int i = 0;

        // Satu transaksi bisa punya beberapa baris (1 baris = 1 produk),
        // semua baris dengan transactionId yang sama dihitung sebagai satu transaksi
        while (i < Transaction.transactionCount)
        {
            int trxId = Transaction.transactionId[i];
            Console.WriteLine($"\nTransaksi #{trxId} | {Transaction.date[i]} | Pembeli: {getUserNameById(Transaction.userId[i])}");

            double trxTotal = 0;
            double trxCost = 0;

            while (i < Transaction.transactionCount && Transaction.transactionId[i] == trxId)
            {
                // totalPrice = uang yang dibayar, modal = cost per unit x jumlah
                double lineCost = Transaction.cost[i] * Transaction.quantity[i];
                double lineProfit = Transaction.totalPrice[i] - lineCost;

                Console.WriteLine($"  - {getProductName(Transaction.productId[i])} x{Transaction.quantity[i]} | Jual: Rp{Transaction.totalPrice[i]:N0} | Modal: Rp{lineCost:N0} | {getStatus(lineProfit)}: Rp{Math.Abs(lineProfit):N0}");

                trxTotal += Transaction.totalPrice[i];
                trxCost += lineCost;
                i++;
            }

            double trxProfit = trxTotal - trxCost;
            Console.WriteLine($"  Total: Rp{trxTotal:N0} | {getStatus(trxProfit)}: Rp{Math.Abs(trxProfit):N0}");

            grandTotal += trxTotal;
            grandCost += trxCost;
            trxCount++;
        }

        double grandProfit = grandTotal - grandCost;
        Console.WriteLine();
        Console.WriteLine(Program.lines35);
        Console.WriteLine($"Jumlah transaksi : {trxCount}");
        Console.WriteLine($"Total pendapatan : Rp{grandTotal:N0}");
        Console.WriteLine($"Total modal      : Rp{grandCost:N0}");
        Console.WriteLine($"{getStatus(grandProfit)} bersih     : Rp{Math.Abs(grandProfit):N0}");
        Console.ReadLine();
    }
}
