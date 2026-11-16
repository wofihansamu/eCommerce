public static class DataFile
{

    public static Array[] kolomUser =
        {
            Users.userId,
            Users.userName,
            Users.userPassword,
            Users.role
        };
    public static Array[] kolomProduct =
        {
            Products.productId,
            Products.name,
            Products.price,
            Products.cost,
            Products.category,
            Products.stock,
        };
    public static Array[] kolomCart =
        {
            Cart.userId,
            Cart.productId,
            Cart.productQty,
        };
    public static Array[] kolomTransaction =
        {
            Transaction.date,
            Transaction.transactionId,
            Transaction.userId,
            Transaction.productId,
            Transaction.quantity,
            Transaction.discount,
            Transaction.cost,
            Transaction.retail,
            Transaction.totalPrice,
        };

    public static void LoadAllData()
    {
        Users.userCount = loadData(Constant.FILE_USERS, kolomUser);
        if (Users.userCount == Constant.KOSONG)
        {
            Users.userCount = 0;
            Users.seedData();
            saveData(Constant.FILE_USERS, kolomUser, Users.userCount);
        }

        Products.productCount = loadData(Constant.FILE_PRODUCTS, kolomProduct);
        if (Products.productCount == Constant.KOSONG)
        {
            Products.productCount = 0;
            Products.seedData();
            saveData(Constant.FILE_PRODUCTS, kolomProduct, Products.productCount);
        }

        Transaction.transactionCount = loadData(Constant.FILE_TRANSACTION, kolomTransaction);
        if (Transaction.transactionCount == Constant.KOSONG)
        {
            Transaction.transactionCount = 0;
            saveData(Constant.FILE_TRANSACTION, kolomTransaction, Transaction.transactionCount);
        }

        Cart.cartCount = loadData(Constant.FILE_CART, kolomCart);
        if (Cart.cartCount == Constant.KOSONG)
        {
            Cart.cartCount = 0;
            saveData(Constant.FILE_CART, kolomCart, Cart.cartCount);
        }

    }

    public static void SaveAllData()
    {
        saveData(Constant.FILE_USERS, kolomUser, Users.userCount);
        saveData(Constant.FILE_PRODUCTS, kolomProduct, Products.productCount);
        saveData(Constant.FILE_TRANSACTION, kolomTransaction, Transaction.transactionCount);
        saveData(Constant.FILE_CART, kolomCart, Cart.cartCount);
    }


    public static string pararelToLine(Array[] kolom, int index)
    {
        string[] lineArray = new string[kolom.Length];
        for (int i = 0; i < kolom.Length; i++)
        {
            lineArray[i] = kolom[i].GetValue(index).ToString();
        }
        return string.Join(",", lineArray);
    }
    public static void lineToPararel(string line, Array[] kolom, int index)
    {
        string[] parts = line.Split(',');
        for (int i = 0; i < kolom.Length && i < parts.Length; i++)
        {
            Type elementType = kolom[i].GetType().GetElementType();

            object convertedValue = Convert.ChangeType(parts[i], elementType);

            kolom[i].SetValue(convertedValue, index);
        }

    }

    public static void saveData(string namaFile, Array[] kolom, int count)
    {
        try
        {
            if (count <= 0) return;
            string[] data = new string[count];

            for (int i = 0; i < count; i++)
            {
                data[i] = pararelToLine(kolom, i);
                Console.WriteLine(data[i]);
            }

            File.WriteAllLines(namaFile, data);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Folder tujuan tidak ditemukan: " + ex.Message);
            Console.WriteLine("Gagal Save Data " + namaFile);
            Console.ReadLine();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Tidak ada izin menulis file atau file berstatus Read-Only: " + ex.Message);
            Console.WriteLine("Gagal Save Data " + namaFile);
            Console.ReadLine();
        }
        catch (IOException ex)
        {
            Console.WriteLine("Gagal menyimpan file (file sedang digunakan atau disk penuh): " + ex.Message);
            Console.WriteLine("Gagal Save Data " + namaFile);
            Console.ReadLine();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Nama atau path file tidak valid: " + ex.Message);
            Console.WriteLine("Gagal Save Data " + namaFile);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Terjadi kesalahan saat menyimpan data: " + ex.Message);
            Console.WriteLine("Gagal Save Data " + namaFile);
            Console.ReadLine();
        }
    }

    public static int loadData(string namaFile, Array[] kolom)
    {
        try
        {
            if (!File.Exists(namaFile))
            {
                return Constant.KOSONG;
            }

            string[] lines = File.ReadAllLines(namaFile);

            int count = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                lineToPararel(lines[i], kolom, count);
                count++;
            }

            return count;
        }
        catch (IOException ex)
        {
            Console.WriteLine("Gagal membaca file: " + ex.Message);
            Console.WriteLine("Gagal Load Data " + namaFile);
            Console.ReadLine();
            return Constant.KOSONG;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Akses ke file ditolak: " + ex.Message);
            Console.WriteLine("Gagal Load Data " + namaFile);
            Console.ReadLine();
            return Constant.KOSONG;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Terjadi kesalahan: " + ex.Message);
            Console.WriteLine("Gagal Load Data " + namaFile);
            Console.ReadLine();
            return Constant.KOSONG;
        }
    }
}