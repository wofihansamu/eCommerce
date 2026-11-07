class Checkout
{
    public static int trxId = 1;

    public static void Process ()
    {
        if (Cart.cartCount == 0) // cek keranjang kosong atau tidak
        {
            Console.WriteLine ("Keranjang belanja masih kosong. ");
            Console.ReadLine();
            return;
        }

        double totalBelanja = 0;
        for (int i = 0; i < Cart.cartCount; i++) // cek isi keranjang sebelum pembayaran
        {
            int productIndex = Products.getIndexProductByID(Cart.productId[i]);
            if (productIndex == -1)
            {
                Console.WriteLine ("Produk tidak ditemukan");
                Console.ReadLine();
                return;
            }

            if (Cart.productQty[i] <= Products.stock[productIndex])
            {
                double totalPrice = Products.price[productIndex] * Cart.productQty[i];
                totalBelanja += totalPrice;
                Console.WriteLine ($"{Products.name[productIndex]} | Jumlah: {Cart.productQty[i]} | Harga: Rp{Products.price[productIndex]:N0} | Total: Rp{totalPrice:N0}");   
            }
            else
            {
                Console.WriteLine ("Stok tidak cukup");
                continue;
            }
        }
        if (totalBelanja == 0)
        {
            Console.WriteLine ("Stok tidak mencukupi, tidak ada produk yang dapat diproses.");
            return;
        }
        Console.WriteLine ($"Total Belanja: Rp{totalBelanja:N0}");
        Console.WriteLine ("Apakah anda ingin melanjutkan pembayaran?");
        Console.WriteLine ("1. Ya");
        Console.WriteLine ("2. Tidak");
        Console.Write ("Pilih: ");
        int pilih = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        if (pilih == 1)
        {
            Console.WriteLine ("Pembayaran dilanjutkan");
            bool[] berhasilDibeli = new bool[Cart.cartCount];
            for (int i = 0; i < Cart.cartCount; i++)
            {
                int productIndex = Products.getIndexProductByID(Cart.productId[i]);
                if (Cart.productQty[i] <= Products.stock[productIndex])
                {
                    berhasilDibeli[i] = true;
                    Console.WriteLine($"Stok sebelum: {Products.stock[productIndex]}");
                    Products.stock[productIndex] -= (int)Cart.productQty[i];
                    Console.WriteLine($"Stok sesudah: {Products.stock[productIndex]}");
                    double totalPrice = Products.price[productIndex] * Cart.productQty[i];
                    Transaction.addTransaction
                    (
                        trxId,                             // trxId
                        1,                                 // trxuserId (Budi)
                        Cart.productId[i],                 // trxProductId
                        (int)Cart.productQty[i],           // trxQty
                        0,                                 // trxDiscount
                        Products.cost[productIndex],       // trxCost
                        Products.price[productIndex],      // trxRetail 
                        totalPrice                         // trxTotal
                    );
                }
            }

            trxId++;       
            int j = 0;

            for (int i = 0; i < Cart.cartCount; i++)
                {
                    if (berhasilDibeli[i] == false)
                    {
                        Cart.productId[j] = Cart.productId[i];
                        Cart.productQty[j] = Cart.productQty[i];
                        j++;
                    }
                }
            Cart.cartCount = j;
            Console.WriteLine("Checkout selesai.");
            Console.ReadLine();
        }
        
        else if (pilih == 2)
            {
                Console.WriteLine("Pesanan dibatalkan");
            }
        
        else
            {
                Console.WriteLine ("Pilihan tidak valid");
            }
    }
}