namespace BudgetBook.Core;

public class TransactionManager
{
    /// <summary>
    /// Function to display the report menu and return the user's choice.
    /// Must return an integer between 0 and 3.
    /// </summary>
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
                AddIncome();
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
                // Call the method to show todays report here
                break;
            case 2:
                Console.WriteLine("This Month's Report:");
                // Call the method to show this month's report here
                break;
            case 3:
                Console.WriteLine("Returning to Start Menu.\n");
                break;
        }
    }

    private void AddIncome()
    {
        Transaction transaction = CreateTransaction(TransactionType.Income);
        TransactionStore.Instance.AddTransaction(transaction);
        Console.WriteLine("Income added successfully.");
    }

    private static Transaction CreateTransaction(TransactionType type)
    {
        string description = "";
        do
        {
            Console.Write("Enter the description (max. 200 Characters): ");
            description = Console.ReadLine() ?? string.Empty;
        } while (description.Length > 200);

        decimal amount = 0;
        do
        {
            Console.Write("Enter the amount (without currency symbol): ");
        } while (!decimal.TryParse(Console.ReadLine(), out amount));

        Transaction transaction = new(DateTime.Now, description, amount, type);
        return transaction;
    }
}
