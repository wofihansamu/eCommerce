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
        _addProd(1, "Sepatu Kets", 18000, 20000, "Sepatu", 10);
        _addProd(2, "Baju Formal L", 17000, 25000, "Baju", 5);
        _addProd(3, "Topi Petani", 14000, 15000, "Topi", 20);
        _addProd(4, "Jazz", 80000, 100000, "Baju", 16);
        _addProd(5, "Jam Tangan 24x1", 150000, 200000, "Accesoris", 5);
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
        return -1;
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
                    int idToEdit = Convert.ToInt32(Console.ReadLine());
                    modifyProductByID(idToEdit);
                    break;
                case 4:
                    Console.WriteLine("Masukkan Produk ID untuk di hapus : ");
                    int idToDelete = Convert.ToInt32(Console.ReadLine());
                    deleteProductByID(idToDelete);
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

    static void _addProd(int _id, string _name, double _cost, double _price, string _kategori, int _stock=0)
    {
        productId[productCount] = _id;
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
            Console.WriteLine("Enter Product ID : ");
            int.TryParse(Console.ReadLine(), out int prodId);
            int isExist = getIndexProductByID(prodId);
            if (isExist >= 0)
            {
                Console.WriteLine("Produk ID sudah ada!!");
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
                _addProd(prodId, inp_name, inp_cost, inp_price, inp_kategori, inp_stock);
                Console.WriteLine("=== Produk berhasil ditambahkan ! ===");
                Console.ReadLine();
            }
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