public static class PrintMenu
{
    private static int choice = 4; // Default choice is 4 (Exit)
    public static int PrintStartMenu()
    {
        while (true)
        {
            Console.WriteLine("[1] Add Income, [2] Add Expense, [3] Show Report, [4] Exit");
            Console.Write("Please select an option (standard input is 4): ");
            try
            {
                choice = int.Parse(Console.ReadLine() ?? "4");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            if (choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice. Input must be between 1 and 4.");
                continue;
            }
            else
            {
                return choice;
            }
        }
    }

    public static int PrintReportMenu()
    {
        while (true)
        {
            Console.WriteLine("\nBudget Report:\n");
            Console.WriteLine("[1] Today's Report, [2] This Month's Report, [3] Back to Main Menu");
            Console.Write("Please select an option (standard input is 3): ");
            try
            {
                choice = int.Parse(Console.ReadLine() ?? "3");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            if (choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice. Input must be between 1 and 3");
                continue;
            }
            else
            {
                return choice;
            }
        }
    }
}
