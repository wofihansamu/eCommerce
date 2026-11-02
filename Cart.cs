class Cart
{
    const int MAX_CART_COUNT = 100;

    public static int[] productId = new int[MAX_CART_COUNT];
    public static double[] productQty = new double[MAX_CART_COUNT];
    public static int cartCount = 0;
    public static void AddToCart(int id, int qty)
    {
        productId[cartCount] = id;
        productQty[cartCount] = qty;
        cartCount++;
    }

    public static void List()
    {
        Console.Clear();
        Console.WriteLine("==== Isi Keranjang ====");
        double subTotal = 0;
        if (cartCount > 0)
        {
            for (int i = 0; i < cartCount; i++)
            {
                int productIndex = Products.getIndexProductByID(productId[i]);
                double totalPrice = productQty[i] * Products.price[productIndex];
                subTotal += totalPrice;
                Console.WriteLine($" {i + 1}. {Products.name[productIndex]}");
                Console.WriteLine($" Qty {productQty[i]:N2} @ {Products.price[productIndex]:N0} = {totalPrice:N0}");
                Console.WriteLine();
            }
            Console.WriteLine($" == TOTAL BELANJA : {subTotal:N0} ==");
            Console.WriteLine(":: Pilih Nomer Barang untuk ubah Qty atau hapus ::");
            int pilih = int.TryParse(Console.ReadLine(), out int pil) ? pil : 0;
            if (pilih > 0) {
                OpsiMenu(pilih-1);
            }

        }
        else
        {
            Console.WriteLine("Keranjang Masih Kosong, Silahkan pilih produk yang anda minati");
            Console.ReadLine();
        }
    }
    
    public static void OpsiMenu(int index)
    {
        int productIndex = Products.getIndexProductByID(productId[index]);
        double totalPrice = productQty[index] * Products.price[productIndex];
        Console.WriteLine($" {index + 1}. {Products.name[productIndex],25}");
        Console.WriteLine($" Qty {productQty[index]:N2} @ {Products.price[productIndex]:N0} = {totalPrice:N0}");
        Console.WriteLine("----------------");
        Console.WriteLine("1. Edit Qty");
        Console.WriteLine("2. Hapus");
        Console.WriteLine("0. Batal");
        int pilih = int.TryParse(Console.ReadLine(), out int pil) ? pil : 0;
        switch (pilih)
        {
            case 1:
                ChangeQty(index);
                break;
            case 2:
                DeleteProduct(index);
                break;
        }
    }
    public static void DeleteProduct(int index)
    {
        for (int i = index; i < cartCount; i++)
        {
            productId[i] = productId[i + 1];
            productQty[i] = productQty[i + 1];
        }
        cartCount--;
        Console.WriteLine("Produk berhasil dihapus");
        Console.ReadLine();
    }
    
    public static void ChangeQty(int index)
    {
        Console.Write("Masukkan Qty Baru : ");
        double qtyBaru = double.TryParse(Console.ReadLine(), out double qtbaru) ? qtbaru : 1;
        if (qtyBaru > 0)
        {
            productQty[index] = qtyBaru;
            Console.WriteLine("Qty Berhasil diganti!");
            Console.ReadLine();
        }
    }
}