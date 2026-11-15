class Checkout
{
    
    public static int GetNextTrxId()
    {
        int lastTrxId = 0;
        for (int i = 0; i < Transaction.transactionCount; i++)
            {
                if (Transaction.transactionId[i] > lastTrxId)
                {
                    lastTrxId = Transaction.transactionId[i];
                }
            }

        return lastTrxId + 1;
    }
    
    public static void Process ()
    {
        double totalBelanja = 0;
        double totalBayar = 0;
        double diskon = 0;
        // Cek keranjang kosong atau tidak
        if (Cart.cartCount == 0)
        {
            Console.WriteLine ("Keranjang belanja masih kosong. ");
            Console.ReadLine();
            return;
        }
        // Cek isi keranjang sebelum pembayaran
        for (int i = 0; i < Cart.cartCount; i++)
        {
            int productIndex = Products.getIndexProductByID(Cart.productId[i]);
            if (productIndex == -1)
            {
                Console.WriteLine ("Produk tidak ditemukan");
                Console.ReadLine();
                return;
            }
                double totalPrice = Products.price[productIndex] * Cart.productQty[i];
                totalBelanja += totalPrice;
                Console.WriteLine ($"{Products.name[productIndex]} | Jumlah: {Cart.productQty[i]} | Harga: Rp{Products.price[productIndex]:N0} | Total: Rp{totalPrice:N0}");   
            
        }
     
        //Input kode voucher (jika ada) & hitung total bayar
        Console.Write("Masukkan kode voucher (jika ada): ");
        string voucher = Console.ReadLine();
        totalBayar = totalBelanja;
        if (voucher == "")
        {
            diskon = 0;
            totalBayar = totalBelanja;
        }
        else if (voucher == "HEMAT10")
        {
            diskon = 10.0 / 100 * totalBelanja;
            totalBayar = totalBelanja - diskon;
        }
        else if (voucher == "POTONG5K" && totalBelanja >= 50000)
        {
            diskon = 5000;
            totalBayar = totalBelanja - diskon;
        }
        else if (voucher == "POTONG5K" && totalBelanja < 50000)
        {
            Console.WriteLine($"Tambah Rp{50000 - totalBelanja:N0}, untuk mendapatkan diskon");
        }
        else
        {
            Console.WriteLine("Kode voucher tidak valid");
        }
        
        // Membuat struk pembayaran
        Console.WriteLine("=== STRUK PEMBAYARAN ===");
        Console.WriteLine($"Total Belanja : Rp{totalBelanja:N0}");
        Console.WriteLine($"Diskon        : Rp{diskon:N0}");
        Console.WriteLine($"Total Bayar   : Rp{totalBayar:N0}");

        // User konfirmasi pembayaran
        Console.WriteLine ("Apakah anda ingin melanjutkan pembayaran?");
        Console.WriteLine ("1. Ya");
        Console.WriteLine ("2. Tidak");
        Console.Write ("Pilih: ");
        int pilih = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

        if (pilih == 1)
        {
            int trxId = GetNextTrxId();
            Console.WriteLine ("Pembayaran dilanjutkan");
            for (int i = 0; i < Cart.cartCount; i++)
            {
                int productIndex = Products.getIndexProductByID(Cart.productId[i]);
                double totalPrice = Products.price[productIndex] * Cart.productQty[i];
                double diskonPerProduk = 0;
                diskonPerProduk = totalPrice / totalBelanja * diskon; // totalBelanja adalah total awal
                Transaction.addTransaction
                    (
                        trxId,                             // trxId
                        Users.sessionUserId,               // trxuserId
                        Cart.productId[i],                 // trxProductId
                        (int)Cart.productQty[i],           // trxQty
                        diskonPerProduk,                   // trxDiscount
                        Products.cost[productIndex],       // trxCost
                        Products.price[productIndex],      // trxRetail 
                        totalPrice                         // trxTotal
                    );
            }
            Cart.cartCount = 0;
            Console.WriteLine($"ID Transaksi : TRX{trxId:D3}");
            Console.WriteLine("Checkout berhasil dilakukan!");
            Console.ReadLine();
        }
        
        else if (pilih == 2)
        {
            Console.WriteLine("Pesanan dibatalkan.");
        }
    
        else
            {
                Console.WriteLine ("Pilihan tidak valid.");
            }
    }
} 