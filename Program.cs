class Program
{

    public static string lines35 = new String('=',35);
    static void Main(string[] args)
    {
        Products.seedData();
        while (true) {
            MenuAwal();
        }
    }

    public static void MenuAwal(){
        Console.Clear();
        Console.WriteLine("=== Menu Awal ===");
        Console.WriteLine(lines35);
        Console.WriteLine($"= {"1. Login",-31} =");
        Console.WriteLine($"= {"2. Register",-31} =");
        Console.WriteLine($"= {"0. Keluar",-31} =");
        Console.WriteLine(lines35);
        Console.Write("Pilih menu: ");
        int choose = int.TryParse(Console.ReadLine(), out int result) ? result : -1;
        switch (choose){
            case 1:
                Users.Login();
                MenuUtama();
                break;
            case 2:
                Users.Register();
                break;
            case 0:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                Console.ReadKey();
                MenuAwal();
                break;
        }
    }

    public static void MenuUtama(){
        Console.Clear();
        Console.WriteLine("=== Menu Utama ===");
        Console.WriteLine(lines35);
        Console.WriteLine($"= {"1. Lihat Produk",-31} =");
        Console.WriteLine($"= {"2. Cari/Filter Produk",-31} =");
        Console.WriteLine($"= {"3. Keranjang",-31} =");
        Console.WriteLine($"= {"4. Checkout",-31} =");
        Console.WriteLine($"= {"5. Riwayat Transaksi",-31} =");
        Console.WriteLine($"= {"6. Kelola Produk (Admin)",-31} =");
        Console.WriteLine($"= {"7. Laporan Penjualan (Admin)",-31} =");
        Console.WriteLine($"= {"0. Logout/Keluar",-31} =");
        Console.WriteLine(lines35);
        Console.Write("Pilih menu: ");
        int choose = int.TryParse(Console.ReadLine(), out int result) ? result : -1;
        switch (choose){
            case 0:
                MenuAwal();
                break;
            case 6:
                Products.menuProduct();
                break;
            default:
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                Console.ReadKey();
                MenuUtama();
                break;
        }
    }
}