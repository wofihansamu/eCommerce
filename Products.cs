using System;
class Catalog
{
    public static int[] productId = new int[100];
    public static string[] name = new string[100];
    public static double[] price = new double[100];
    public static string[] category = new string[100];
    public static int[] stock = new int[100];
    public static int productCount = 0;

    public static void AddProduct (int id, string n, double p, string c, int s)
    {
        productId [productCount] = id;
        name [productCount] = n;
        price [productCount] = p;
        category [productCount] = c;
        stock [productCount] = s;
        productCount++;
    }
    public static void menuProduct()
    {

        if (productCount == 0)
        {
            AddProduct(0, "Kaos Polos Hitam", 75000, "Fashion", 50);
            AddProduct(1, "Kemeja Flanel", 150000, "Fashion", 29);
            AddProduct(2, "Celana Jeans", 250000, "Fashion", 20);
            
        }

        int itemsPerPage = 10;
        int currentPage = 0;
        int totalPage = (productCount + itemsPerPage - 1) / itemsPerPage;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== KATALOG PRODUK ====");
            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, productCount);
            for (int i = start; i < end; i++)
            {
                string rupiah = $"Rp{price[i]:N0}".Replace(",",".");             
                Console.WriteLine ($"{productId[i]}. {name[i]} - {rupiah} | stok {stock[i]} | {category[i]}");
            } 

            Console.WriteLine($"Halaman {currentPage + 1}/{totalPage}");
            Console.WriteLine("[<-/p] Prev  [->/n] Next  [0-2] Tambah ke keranjang  [ESC/q] Kembali");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.RightArrow || key == ConsoleKey.N)
            {
                if(currentPage < totalPage - 1) currentPage++;
            }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.P)
            {
                if(currentPage > 0) currentPage--;
            }
            else if (key == ConsoleKey.Escape || key == ConsoleKey.Q)
            {
                break;
            }
            else if (key >= ConsoleKey.D0 && key <= ConsoleKey.D2)
            {
                int id = (int)(key-ConsoleKey.D0);
                if (id >= start && id <= end)
                {
                    Console.WriteLine();
                    while (true)
                    {
                        Console.Write($"Qty untuk {name[id]} stok {stock[id]}: ");
                        int inputQty = int.Parse(Console.ReadLine()!);
                        if (inputQty >= 1 && inputQty <= stock[id])
                        {
                            stock[id] = stock[id] - inputQty;
                            Console.WriteLine();
                            Console.WriteLine($"Ditambahkan: {name[id]}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Input tidak valid, masukkan angka 1-{stock[id]}.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine ("Tekan tombol apa saja untuk lanjut...");
                    Console.ReadKey(true);
                } 
            }
        }
    }   
    
}    