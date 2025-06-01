namespace BudgetBook.Core;

public class TransactionManager
{
    private readonly Func<int> _getReportMenuChoice;

    public TransactionManager(Func<int> getReportMenuChoice)
    {
        _getReportMenuChoice = getReportMenuChoice;
    }
    public void StartMenuHandler(int input)
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
                int reportChoice = _getReportMenuChoice();
                ReportMenuHandler(reportChoice);
                break;
            case 4:
                Console.WriteLine("Exiting the application. Goodbye!");
                Environment.Exit(0);
                break;
        }
    }

    public void ReportMenuHandler(int input)
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
}
