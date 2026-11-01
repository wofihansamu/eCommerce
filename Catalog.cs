using System;
class Catalog
{


    public static void daftarMenu()
    {
        Console.WriteLine("===== Katalog ======");
        Console.WriteLine("1. Semua Produk");
        Console.WriteLine("2. Produk berdasarkan kategori");
        Console.WriteLine("3. Cari Produk");

        // bikin fungsi pilih
        Console.Write("Silakan pilih menu yang ingin dilihat: ");
        int choose = int.TryParse(Console.ReadLine(), out int pilih) ? pilih : 0;
        switch (choose)
        {
            case 1 :
                menuProduct();
                break;
            case 2:
                byCategory();
                break;
            case 3:
                search();
                break;
            default:
                break;
        }
    }


    public static void byCategory()
    {
        List<string> cat = new List<string>();
        for (int i = 0; i < Products.productCount; i++)
        {
            if (!cat.Contains(Products.category[i]))//menampung kategori unik
                cat.Add(Products.category[i]);
        }
        Console.Clear();
        Console.WriteLine("=== Pilih Kategori ===");
        for (int i = 0; i < cat.Count; i++)
        {
            Console.WriteLine($"{i}. {cat[i]}");
        }
        Console.WriteLine("Pilih nomor: ");
        int.TryParse(Console.ReadLine(), out int catChoice);
        if (catChoice <0 || catChoice >= cat.Count) return; 
        
        string selected = cat[catChoice];

        List<int> filter = new List<int>();
        for (int i = 0; i < Products.productCount; i++)
        {
            if (Products.category[i] == selected) filter.Add(i);
        }
        int itemsPerPage = 10;
        int currentPage = 0;
        int totalPage = (filter.Count + itemsPerPage - 1) / itemsPerPage;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine ($"=== Kategori: {selected} ===");
            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, filter.Count);

            for (int i = start; i < end; i++)
            {
              int prodIdx = filter[i];
              int nomorUrut = i - start;
              Console.WriteLine ($"{nomorUrut}. {Products.name[prodIdx]} - Rp{Products.price[prodIdx]:N0} | Stok {Products.stock[prodIdx]}");
            }
            Console.WriteLine($"Halaman {currentPage + 1}/{totalPage}");
            Console.WriteLine("[<-/p] Prev  [->/n] Next  [0-9] Tambah ke keranjang  [ESC/q] Kembali");
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
            else if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
            {
                char keyChar = (char)key;
                string charString = keyChar.ToString();
                int nomerKey = int.Parse(charString);

                int input = start + nomerKey;

                if (input >= start && input <= end)
                {
                    int id = filter[input];
                    Console.WriteLine();
                    while (true)
                    {   
                        Console.Write($"Qty untuk {Products.name[id]} stok {Products.stock[id]}: ");
                        int inputQty = int.Parse(Console.ReadLine()!);
                        if (inputQty >= 1 && inputQty <= Products.stock[id])
                        {
                            Products.stock[id] = Products.stock[id] - inputQty;
                            Console.WriteLine();
                            Console.WriteLine($"Ditambahkan: {Products.name[id]}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Input tidak valid, masukkan angka 1-{Products.stock[id]}.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine ("Tekan tombol apa saja untuk lanjut...");
                    Console.ReadKey(true);
                }
                
            }
        }
        
    }

    public static void search()
    {
        Console.Clear();
        Console.WriteLine("Cari (nama/kategori):");
        string keyword = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(keyword)) return;
        List<int> filter = new List<int>();
        for (int i = 0; i < Products.productCount; i++)
        {
            if (Products.name[i].ToLower().Contains(keyword.ToLower()) || 
                Products.category [i].ToLower().Contains(keyword.ToLower())) filter.Add(i);
        }

        if (filter.Count == 0)
        {
            Console.WriteLine ("Tidak ditemukan");
            Console.ReadLine();
            return;
        }
        
        int itemsPerPage = 10;
        int currentPage = 0;
        int totalPage = (filter.Count + itemsPerPage - 1) / itemsPerPage;

        while (true)
        {
            Console.Clear();
            Console.WriteLine ($"=== Hasil cari: {keyword} ===");
            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, filter.Count);

            for (int i = start; i < end; i++)
            {
              int prodIdx = filter[i];
              int nomorUrut = i - start;
              Console.WriteLine ($"{nomorUrut}. {Products.name[prodIdx]} - Rp{Products.price[prodIdx]:N0} | Stok {Products.stock[prodIdx]}");
            }
            Console.WriteLine($"Halaman {currentPage + 1}/{totalPage}");
            Console.WriteLine("[<-/p] Prev  [->/n] Next  [0-9] Tambah ke keranjang  [ESC/q] Kembali");
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
            else if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
            {
                char keyChar = (char)key;
                string charString = keyChar.ToString();
                int nomerKey = int.Parse(charString);

                int input = start + nomerKey;

                if (input >= start && input <= end)
                {
                    int id = filter[input];
                    Console.WriteLine();
                    while (true)
                    {   
                        Console.Write($"Qty untuk {Products.name[id]} stok {Products.stock[id]}: ");
                        int inputQty = int.Parse(Console.ReadLine()!);
                        if (inputQty >= 1 && inputQty <= Products.stock[id])
                        {
                            Products.stock[id] = Products.stock[id] - inputQty;
                            Console.WriteLine();
                            //menunggu fitur cart
                            Console.WriteLine($"Ditambahkan: {Products.name[id]}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Input tidak valid, masukkan angka 1-{Products.stock[id]}.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine ("Tekan tombol apa saja untuk lanjut...");
                    Console.ReadKey(true);
                }
                
            }
        }
        

    }
    // Menu Nomer 1
    public static void menuProduct()
    {
        int itemsPerPage = 10;
        int currentPage = 0;
        int totalPage = (Products.productCount + itemsPerPage - 1) / itemsPerPage;

        while (true)
        {

            Console.Clear();
            Console.WriteLine("=== KATALOG PRODUK ====");
            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, Products.productCount);
            
            int nomer = 0;
            for (int i = start; i < end; i++)
            {
                string rupiah = $"Rp{Products.price[i]:N0}";             
                Console.WriteLine ($"{nomer}. {Products.name[i]} - {rupiah} | stok {Products.stock[i]} | {Products.category[i]}");
                nomer++;
            } 

            Console.WriteLine($"Halaman {currentPage + 1}/{totalPage}");
            Console.WriteLine("[<-/p] Prev  [->/n] Next  [0-9] Tambah ke keranjang  [ESC/q] Kembali");


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
            else if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
            {
                // int id = (int)(key-ConsoleKey.D0);
                //  nomer n = start + nomer ke-n - 1
                // (char)key = 'A' / '1'
                // char -> integer === char -> string -> integer
                // ((char)key).toString() 
                // int.Parse(string)
                char keyChar = (char)key;
                string charString = keyChar.ToString();
                int nomerKey = int.Parse(charString);

                int id = start + nomerKey;

                if (id >= start && id <= end)
                {
                    Console.WriteLine();
                    while (true)
                    {


                        Console.Write($"Qty untuk {Products.name[id]} stok {Products.stock[id]}: ");
                        int inputQty = int.Parse(Console.ReadLine()!);
                        if (inputQty >= 1 && inputQty <= Products.stock[id])
                        {
                            Products.stock[id] = Products.stock[id] - inputQty;
                            Console.WriteLine();
                            //menunggu fitur cart
                            Console.WriteLine($"Ditambahkan: {Products.name[id]}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Input tidak valid, masukkan angka 1-{Products.stock[id]}.");
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