class Users {
    // GOkd
    const int MAX_USER_COUNT = 100;
    const int MAX_LOGIN_FAIL = 3;
    public static int[] userId = new int[MAX_USER_COUNT];
    public static string[] userName = new string[MAX_USER_COUNT];
    public static string[] userPassword = new string[MAX_USER_COUNT];
    public static string[] role = new string[MAX_USER_COUNT];

    public static int userCount = 0;

    public static string sessionRole = "";
    public static int sessionUserId = Constant.KOSONG;

    public static void _saveUser(int _userId, string _userName, string _password, string _role)
    {
        userId[userCount] = _userId;
        userName[userCount] = _userName;
        userPassword[userCount] = _password;
        role[userCount] = _role;
        userCount++;
    }

    public static void seedData()
    {
        _saveUser(0, "admin","admin123","admin");
        _saveUser(1, "budi","budi123","customer");
    }

    static bool IsEmptyOrNull(string data)
    {
        return data.Trim() == "" || data == null;
    }
    public static int getIndexUserByName(string name)
    {
        if (userCount > 0)
        {
            for (int i = 0; i < userCount; i++)
            {
                if (userName[i].ToLower().Trim() == name.ToLower().Trim())
                {
                    return i;
                }
            }

        }
        return Constant.KOSONG;
    }

    static string getReadLine(string title, bool clearScreen = false)
    {
        string input = "";
        bool isValid = false;
        if (clearScreen) Console.Clear();
        while (!isValid)
        {
            Console.Write(title);
            input = Console.ReadLine();
            Console.WriteLine();
            if (IsEmptyOrNull(input))
            {
                Console.WriteLine("Data tidak boleh kosong!");
                Console.ReadLine();
            } else
            {
                isValid = true;
            }
        }
        return input;
    }

    public static void Login()
    {
        sessionRole = "";
        int foundIndex = Constant.KOSONG;
        int loginFail = 0;
        while (sessionRole == "")
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");
            string userName = getReadLine("Masukkan User Name : ");
            string password = getReadLine("Masukkan Password : ");
            foundIndex = getIndexUserByName(userName);
    
            if (foundIndex >= 0 && userPassword[foundIndex] == password)
            {
                break;
            }
            else
            {
                Console.WriteLine(foundIndex);
                Console.WriteLine(DataFile.pararelToLine(DataFile.kolomUser, 2));
                Console.WriteLine("User atau Password salah!");
                loginFail++;
                Console.ReadLine();
            }
            if (loginFail >= MAX_LOGIN_FAIL)
            {
                Program.MenuAwal();
            }
        }
        sessionRole = role[foundIndex];
        sessionUserId = userId[foundIndex];
        Program.MenuUtama();
    }

    public static void Register()
    {
        Console.Clear();
        Console.WriteLine("=== Register User ===");
        string userName;
        while (true)
        {
            userName = getReadLine("Masukkan User Name : ");
            if (getIndexUserByName(userName) < 0) break;
            Console.WriteLine("User Name sudah terdaftar! Silakan coba lagi.");
            Console.ReadLine();
        }
        string password = getReadLine("Masukkan Password : ");
        while (true)
        {
            string role = getReadLine("Pilih Role (A: admin, C: customer) : ").Trim().ToLower();
            
            if (role == "a" || role == "c")
            {
                string roleString = role == "a" ? "admin" : "customer";
                _saveUser(userCount, userName, password, roleString);
                break;
            }
            Console.WriteLine("Role Tidak tersedia ! Silakan coba lagi.");
            Console.ReadLine();
        }
    }
}