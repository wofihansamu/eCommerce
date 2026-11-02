class Users {
    // GOkd
    public static int[] userId = new int[100];
    public static string[] userName = new string[100];
    public static string[] userPassword = new string[100];
    public static string[] role = new string[100];

    public static int userCount = 0;

    public static string sessionRole = "";

    public static void _saveUser(int _userId, string _userName, string _password, string _role)
    {
        userId[userCount] = _userId;
        userName[userCount] = _userName;
        userPassword[userCount] = _password;
        role[userCount] = _role;
        userCount++;
    }
    public static void Login()
    {
        Console.Clear();
        Console.WriteLine("=== Login ===");
        Console.Write("Masukkan User ID : ");
        string? userId = Console.ReadLine();
        Console.Write("Masukkan Password : ");
        string? password = Console.ReadLine();
        Console.ReadLine();
    }

    public static void Register()
    {
        Console.Clear();
        Console.WriteLine("=== Register User ===");
        Console.Write("Masukkan User ID : ");
        string? userId = Console.ReadLine();
        Console.Write("Masukkan Password : ");
        string? password = Console.ReadLine();
        Console.Write("Pilih Role (A: admin, C: customer) : ");
        string? role = Console.ReadLine();
        Console.ReadLine();

    }
    
    public static void daftarUser()
    {
        
    }
}