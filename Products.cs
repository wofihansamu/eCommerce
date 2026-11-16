using static System.Console;
using System.Data.Common;
class Products {
    
    const int MAX_PRODUCT_COUNT = 100;
    public static int[] productId = new int[MAX_PRODUCT_COUNT];
    public static string[] name = new string[MAX_PRODUCT_COUNT];
    public static double[] price = new double[MAX_PRODUCT_COUNT];
    public static double[] cost = new double[MAX_PRODUCT_COUNT];
    public static string[] category = new string[MAX_PRODUCT_COUNT];
    public static int[] stock = new int[MAX_PRODUCT_COUNT];

    public static int productCount = 0;

    public static void seedData()
    {
        _addProd("Sepatu Kets", 18000, 20000, "Sepatu", 10);
        _addProd("Baju Formal L", 17000, 25000, "Baju", 5);
        _addProd("Topi Petani", 14000, 15000, "Topi", 20);
        _addProd("Jazz", 80000, 100000, "Baju", 16);
        _addProd("Jam Tangan 24x1", 150000, 200000, "Accesoris", 5);
        _addProd("Kacamata Hitam", 25000, 35000, "Aksesoris", 15);
        _addProd("Celana Jeans Pria", 85000, 120000, "Celana", 12);
        _addProd("Kaos Polos Putih", 20000, 30000, "Baju", 50);
        _addProd("Sepatu Pantofel", 120000, 150000, "Sepatu", 8);
        _addProd("Tas Ransel Kanvas", 65000, 90000, "Tas", 25);
        _addProd("Ikat Pinggang Kulit", 45000, 60000, "Aksesoris", 30);
        _addProd("Topi Baseball", 15000, 25000, "Topi", 40);
        _addProd("Kemeja Flanel", 55000, 85000, "Baju", 18);
        _addProd("Dompet Pria", 40000, 75000, "Aksesoris", 22);
        _addProd("Celana Chino", 70000, 95000, "Celana", 14);
        _addProd("Jaket Denim", 110000, 160000, "Baju", 10);
        _addProd("Sepatu Running", 140000, 190000, "Sepatu", 15);
        _addProd("Gelang Tangan", 5000, 15000, "Aksesoris", 100);
        _addProd("Sandal Gunung", 45000, 65000, "Sepatu", 20);
        _addProd("Jas Hujan Motor", 35000, 50000, "Aksesoris", 25);
        _addProd("Batik Lengan Pendek", 60000, 90000, "Baju", 15);
        _addProd("Celana Pendek Santai", 25000, 40000, "Celana", 35);
        _addProd("Tas Selempang", 35000, 55000, "Tas", 18);
        _addProd("Kupluk Rajut", 12000, 20000, "Topi", 25);
        _addProd("Sepatu Boots Kulit", 180000, 250000, "Sepatu", 5);
    }

    public static int BinSearch(int[] dataArray, int target)
    {
        int left = 0;
        int right = dataArray.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (dataArray[mid] == target)
            {
                return mid;
            }
            else if (dataArray[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return Constant.KOSONG;
    }

    public static int getIndexProductByID(int id)
    {
        if (productCount > 0)
        {
            for (int i = 0; i < productCount; i++)
            {
                if (productId[i] == id)
                {
                    return i;
                }
            }

        }
        return Constant.KOSONG;
    }

    public static void menuProduct()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== Menu Admin : Kelola Produk ======");
            Console.WriteLine("1. List Produk");
            Console.WriteLine("2. Tambah Produk");
            Console.WriteLine("3. Edit Produk");
            Console.WriteLine("4. Hapus Produk");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("==========================");
            int choice = int.TryParse(Console.ReadLine(), out int result) ? result : -1;
            switch (choice)
            {
                case 1:
                    listProduct();
                    break;
                case 2:
                    addProduct();
                    break;
                case 3:
                    Console.WriteLine("Masukkan Produk ID untuk di Edit : ");
                    try
                    {
                        int idToEdit = Convert.ToInt32(Console.ReadLine());
                        modifyProductByID(idToEdit);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input tidak valid! Harap masukkan ID berupa angka bulat.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan saat mengedit produk: " + ex.Message);
                        Console.ReadLine();
                    }
                    break;
                case 4:
                    try
                    {
                        Console.WriteLine("Masukkan Produk ID untuk di hapus : ");
                        int idToDelete = Convert.ToInt32(Console.ReadLine());
                        deleteProductByID(idToDelete);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input tidak valid! Harap masukkan ID berupa angka bulat.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan saat mengedit produk: " + ex.Message);
                        Console.ReadLine();
                    }
                    break;
                case 0:
                    Program.MenuUtama();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    public static void listProduct()
    {
        Console.Clear();
        Console.WriteLine("====== Daftar Produk ======");
        if (productCount > 0)
        {

            Console.WriteLine($"{"No",2}  | {"ID",-3} | {"Nama Produk",-20} | {"Stock",-10} | {"Cost",-10} | {"Harga",-10} | {"Kategori",-20} |");
            Console.WriteLine(new String('-', 73));
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine($"{i+1,2}. | {productId[i],-3} | {name[i],-20} | {String.Format("{0:N0}",stock[i]),-10} | {String.Format("{0:N0}",cost[i]) ,-10} | {String.Format("{0:N0}",price[i]) ,-10} | {category[i],-20} |");
            }
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Products.Error : Produk Kosong !!");
            Console.ReadLine();
        }
    }

    static void _addProd(string _name, double _cost, double _price, string _kategori, int _stock=0)
    {
        productId[productCount] = productId[productCount-1]+1;
        name[productCount] = _name;
        cost[productCount] = _cost;
        price[productCount] = _price;
        category[productCount] = _kategori;
        stock[productCount] = _stock;
        productCount++;
    }
    public static void addProduct()
    {
        Console.Clear();
        Console.WriteLine("====== Add Product ======");
        if (productCount >= MAX_PRODUCT_COUNT)
        {
            Console.WriteLine("Maximum Product Count Reached");
            Console.ReadLine();
        }
        else
        {
                Console.WriteLine("Enter Product Name : ");
                string inp_name = Console.ReadLine() ?? "";
                Console.WriteLine("Enter Product Cost : ");
                double inp_cost = double.TryParse(Console.ReadLine(), out double cst) ? cst : 0;
                Console.WriteLine("Enter Product Price : ");
                double inp_price = double.TryParse(Console.ReadLine(), out double prc) ? prc : 0;
                Console.WriteLine("Enter Product Category : ");
                string inp_kategori = Console.ReadLine() ?? "";
                Console.WriteLine("Enter Product Stock : ");
                int inp_stock = int.TryParse(Console.ReadLine(), out int st) ? st : 0;
                _addProd(inp_name, inp_cost, inp_price, inp_kategori, inp_stock);
                Console.WriteLine("=== Produk berhasil ditambahkan ! ===");
                Console.ReadLine();
        }
    }

    public static void modifyProductByID(int id)
    {
        if(productCount > 0)
        {
            int indexToModify = getIndexProductByID(id);
            if (indexToModify != -1)
            {
                Console.WriteLine($"Enter Product Name ({name[indexToModify]}) : ");

                String nameToEdit = Console.ReadLine() ?? name[indexToModify];
                if (!string.IsNullOrEmpty(nameToEdit)) name[indexToModify] = nameToEdit;
                
                Console.WriteLine($"Enter Product Cost ({cost[indexToModify]:N0}) : ");
                cost[indexToModify] = double.TryParse(Console.ReadLine(), out double inp_cost) ? inp_cost : cost[indexToModify];

                Console.WriteLine($"Enter Product Price ({price[indexToModify]:N0}) : ");
                price[indexToModify] = double.TryParse(Console.ReadLine(), out double inp_price) ? inp_price : price[indexToModify];

                Console.WriteLine($"Enter Product Category ({category[indexToModify]}) : ");
                String categoryToEdit = Console.ReadLine() ?? category[indexToModify];
                if(!string.IsNullOrEmpty(categoryToEdit)) category[indexToModify] = categoryToEdit;

                Console.WriteLine($"Enter Product Stock ({stock[indexToModify]:N0}) : ");
                stock[indexToModify] = int.TryParse(Console.ReadLine(), out int inp_stock) ? inp_stock : stock[indexToModify];
            } else
            {
                Console.WriteLine("Products.Error : Product ID not found");
                Console.ReadLine();
            }
        } else
        {
            Console.WriteLine("Products.Error : No Product to be modify");
            Console.ReadLine();
        }
    }
    
    public static void deleteProductByID(int id)
    {
        if (productCount > 0)
        {
            int indexToDelete = getIndexProductByID(id);
            Console.WriteLine("Nama Produk : " + name[indexToDelete]);
            Console.WriteLine("Apakah anda yakin ingin menghapus produk dengan ID " + id + " ? (Y/N)");
            String confirmation = Console.ReadLine();
            if (confirmation.ToUpper() != "Y")
            {
                Console.WriteLine("Produk dengan ID " + id + " batal dihapus");
                Console.ReadLine();
                return;
            }
            for (int i = indexToDelete; i < productCount; i++)
            {
                productId[i] = productId[i + 1];
                name[i] = name[i + 1];
                price[i] = price[i + 1];
                category[i] = category[i + 1];
                stock[i] = stock[i + 1];
            }
            productCount--;
            Console.WriteLine("Produk dengan ID " + id + " berhasil dihapus");
            Console.ReadLine();
        } else
        {
            Console.WriteLine("Products.Error : Produk Kosong !!");
            Console.ReadLine();
        }
    }
}