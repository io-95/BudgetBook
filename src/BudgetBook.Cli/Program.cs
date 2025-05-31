int choice;
choice = PrintStartMenu();

int PrintStartMenu()
{
    Console.WriteLine("Welcome to the Budget Book CLI!");
    Console.WriteLine("[1] Add Income, [2] Add Expense, [3] Show Report, [4] Exit");
    Console.Write("Please select an optio (standard input is 4): ");
    int choice = int.Parse(Console.ReadLine() ?? "4");

    return choice;
}

int PrintReportMenu()
{
    Console.WriteLine("Budget Report:");
    Console.WriteLine("[1] Today's Report, [2] This Month's Report, [3] Back to Main Menu");
    Console.Write("Please select an optio (standard input is 3): ");
    int choice = int.Parse(Console.ReadLine() ?? "3");

    return choice;
}