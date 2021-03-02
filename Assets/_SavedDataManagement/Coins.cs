public static class Coins {

    private static int coins;

    public static int Get() { return coins; }

    public static void Add(int amt)
    {
        coins += amt;
    }

    public static void Set(int amt)
    {
        coins = amt;
    }

    public static bool Debit(int amt)
    {
        if (amt < coins)
        {
            coins -= amt;
            AnalyticsController.LogSpendCurrency();
            return true;
        }
        else { return false; }
    }
}
