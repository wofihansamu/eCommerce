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
        if (loadData(Constant.FILE_USERS, kolomUser) == Constant.KOSONG)
        {
            Users.seedData();
            saveData(Constant.FILE_USERS, kolomUser, Users.userCount);
        }
        if (loadData(Constant.FILE_PRODUCTS, kolomProduct) == Constant.KOSONG)
        {
            Products.seedData();
            saveData(Constant.FILE_PRODUCTS, kolomProduct, Products.productCount);
        }
        if (loadData(Constant.FILE_TRANSACTION, kolomTransaction) == Constant.KOSONG)
        {
            saveData(Constant.FILE_TRANSACTION, kolomTransaction, Transaction.transactionCount);
        }
        if (loadData(Constant.FILE_CART, kolomCart) == Constant.KOSONG)
        {
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
        if (count <= 0) return;
        string[] data = new string[count];

        for (int i = 0; i < count; i++)
        {
            data[i] = pararelToLine(kolom, i);
            Console.WriteLine(data[i]);
        }

        File.WriteAllLines(namaFile, data);
    }

    public static int loadData(string namaFile, Array[] kolom)
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
}