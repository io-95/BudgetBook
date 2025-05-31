using System.Diagnostics;

int choice = 4; // Default choice is 4 (Exit)

Console.WriteLine("Welcome to the Budget Book CLI!");

while (true)
{
    choice = PrintStartMenu();
    StartMenuHandler(choice);
}

int PrintStartMenu()
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
        return choice;
    }
}

int PrintReportMenu()
{
    while (true)
    {
        Console.WriteLine("Budget Report:");
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
        return choice;
    }   
}

void StartMenuHandler(int input) 
{
    switch (input)
    {
        case 1:
            Console.WriteLine("Adding Income:");
            // Call the method to add income here
            break;
        case 2:
            Console.WriteLine("Adding Expense:");
            // Call the method to add expense here
            break;
        case 3:
            int reportChoice = PrintReportMenu();
            ReportMenuHandler(reportChoice);
            break;
        case 4:
            Console.WriteLine("Exiting the application. Goodbye!");
            Environment.Exit(0);
            break;
    }
}

void ReportMenuHandler(int input)
{
    switch (input)
    {
        case 1:
            Console.WriteLine("Today's Report:");
            // Call the method to show today's report here
            break;
        case 2:
            Console.WriteLine("This Month's Report:");
            // Call the method to show this month's report here
            break;
        case 3:
            Console.WriteLine("Returning to Main Menu.");
            break;
    }
}
