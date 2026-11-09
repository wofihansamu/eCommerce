using System;
class Catalog
{
public static void TambahKeKeranjang(int id, int qty)
{
    int productIndex = Products.getIndexProductByID(id);

    for (int i = 0; i < Cart.cartCount; i++)
    {
        if (id == Cart.productId[i])
        {
            double totalBaruQtyCart = Cart.productQty[i] + qty;

            if (totalBaruQtyCart <= Products.stock[productIndex])
            {
                Cart.productQty[i] += qty;
                return;
            }
            else
            {
                Console.WriteLine("Jumlah melebihi stok produk");
                return;
            }
        }
    }

    if (qty <= Products.stock[productIndex])
    {
        Cart.productId[Cart.cartCount] = id;
        Cart.productQty[Cart.cartCount] = qty;
        Cart.cartCount++;
        return;
    }
    else
    {
        Console.WriteLine("Jumlah melebihi stok produk");
        return;
    }
}
    public static void daftarMenu()
    {
        Console.Clear();
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
    //kategori unik
    string[] cat = new string[Products.productCount];
    int catUnique = 0;

    for (int i = 0; i < Products.productCount; i++)
    {
        bool sudahAda = false;

        for (int j = 0; j < catUnique; j++)
        {
            if (Products.category[i] == cat[j])
            {
                sudahAda = true;
                break;
            }
        }

        if (sudahAda == false)
        {
            cat[catUnique] = Products.category[i];
            catUnique++;
        }
    }

    //kategori
    Console.Clear();
    Console.WriteLine("=== Pilih Kategori ===");

    for (int i = 0; i < catUnique; i++)
    {
        Console.WriteLine($"{i}. {cat[i]}");
    }

    Console.WriteLine("Pilih nomor: ");
    int.TryParse(Console.ReadLine(), out int catChoice);

    if (catChoice < 0 || catChoice >= catUnique)
        return;

    string selected = cat[catChoice];

    //indeks produk sesuai kategori
    int[] filter = new int[Products.productCount];
    int filterCount = 0;

    for (int i = 0; i < Products.productCount; i++)
    {
        if (Products.category[i] == selected)
        {
            filter[filterCount] = i;
            filterCount++;
        }
    }

    int itemsPerPage = 10;
    int currentPage = 0;
    int totalPage = (filterCount + itemsPerPage - 1) / itemsPerPage;

    while (true)
    {
        Console.Clear();
        Console.WriteLine($"=== Kategori: {selected} ===");

        int start = currentPage * itemsPerPage;
        int end = Math.Min(start + itemsPerPage, filterCount);

        for (int i = start; i < end; i++)
        {
            int prodIdx = filter[i];
            int nomorUrut = i - start;

            Console.WriteLine($"{nomorUrut}. {Products.name[prodIdx]} - Rp{Products.price[prodIdx]:N0} | Stok {Products.stock[prodIdx]}");
        }

        Console.WriteLine($"Halaman {currentPage + 1}/{totalPage}");
        Console.WriteLine("[<-/p] Prev  [->/n] Next  [0-9] Tambah ke keranjang  [ESC/q] Kembali");

        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.RightArrow || key == ConsoleKey.N)
        {
            if (currentPage < totalPage - 1)
                currentPage++;
        }
        else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.P)
        {
            if (currentPage > 0)
                currentPage--;
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

            if (input >= start && input < end)
            {
                int id = filter[input];

                Console.WriteLine();

                while (true)
                {
                    Console.Write($"Qty untuk {Products.name[id]} stok {Products.stock[id]}: ");
                    int inputQty = int.Parse(Console.ReadLine()!);

                    if (inputQty >= 1)
                    {
                        TambahKeKeranjang(Products.productId[id], inputQty);
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Input tidak valid, masukkan angka 1-{Products.stock[id]}.");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Tekan tombol apa saja untuk lanjut...");
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
        int[] filter = new int[Products.productCount];
        int filterCount = 0;

        for (int i = 0; i < Products.productCount; i++)
        {
            if (Products.name[i].ToLower().Contains(keyword.ToLower()) || 
            Products.category[i].ToLower().Contains(keyword.ToLower()))
            {
                filter[filterCount] = i;
                filterCount++;
            }
        }

        if (filterCount == 0)
        {
            Console.WriteLine ("Tidak ditemukan");
            Console.ReadLine();
            return;
        }
        int itemsPerPage = 10;
        int currentPage = 0;
        int totalPage = (filterCount + itemsPerPage - 1) / itemsPerPage;

        while (true)
        {
            Console.Clear();
            Console.WriteLine ($"=== Hasil cari: {keyword} ===");
            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, filterCount);

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
                        if (inputQty >= 1)
                        {
                            TambahKeKeranjang(Products.productId[id], inputQty);
                            Console.WriteLine();   
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
                        int inputQty = int.TryParse(Console.ReadLine(), out int qty) ? qty : 0;
                        if (inputQty >= 1)
                        {
                            Console.WriteLine();
                            
                            TambahKeKeranjang(Products.productId[id], inputQty);
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