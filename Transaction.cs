class Transaction {
    
    const int MAX_TRX_COUNT = 100;
    public static string[] date = new string[MAX_TRX_COUNT];
    public static int[] transactionId = new int[MAX_TRX_COUNT];
    public static int[] userId = new int[MAX_TRX_COUNT];
    public static int[] productId = new int[MAX_TRX_COUNT];
    public static int[] quantity = new int[MAX_TRX_COUNT];
    public static double[] discount = new double[MAX_TRX_COUNT];
    public static double[] cost = new double[MAX_TRX_COUNT];
    public static double[] retail = new double[MAX_TRX_COUNT];
    public static double[] totalPrice = new double[MAX_TRX_COUNT];

    public static int transactionCount = 0;


    public static void addTransaction(int trxId, int trxUserId, int trxProductId, int trxQty, double trxDiscount, double trxCost, double trxRetail, double trxTotal)
    {
        DateTime now = DateTime.Now;
        date[transactionCount] = now.ToString();
        transactionId[transactionCount] = trxId;
        userId[transactionCount] = trxUserId;
        productId[transactionCount] = trxProductId;
        quantity[transactionCount] = trxQty;
        discount[transactionCount] = trxDiscount;
        cost[transactionCount] = trxCost;
        retail[transactionCount] = trxRetail;
        totalPrice[transactionCount] = trxTotal;
        transactionCount++;
    }

    public static int getIndexTrxById(int trxId)
    {
        if (transactionCount > 0)
        {
            for (int i = 0; i < transactionCount; i++)
            {
                if (transactionId[i] == trxId)
                {
                    return i;
                }
            }

        }
        return -1;
    }
}