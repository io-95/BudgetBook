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
    public bool StartMenuHandler(int input)
    {
        switch (input)
        {
            case 1:
                Console.WriteLine("\nAdding Income:");
                AddIncome();
                break;
            case 2:
                Console.WriteLine("\nAdding Expense:");
                AddExpense();
                break;
            case 3:
                int reportChoice = _getReportMenuChoice();
                ReportMenuHandler(reportChoice);
                break;
            case 4:
                Console.WriteLine("Exiting the application. Goodbye!");
                return false;
        }

        return true;
    }

    public void ReportMenuHandler(int input)
    {
        switch (input)
        {
            case 1:
                Console.WriteLine("\nToday's Report:");
                IEnumerable<Transaction> todayTransactions = TransactionStore.Instance.GetTransactionsByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                PrintReport(todayTransactions);
                break;
            case 2:
                Console.WriteLine("\nThis Month's Report:");
                IEnumerable<Transaction> thisMonthsTransactions = TransactionStore.Instance.GetTransactionsByMonth(DateTime.Now.Month, DateTime.Now.Year);
                PrintReport(thisMonthsTransactions);
                break;
            case 3:
                Console.WriteLine("\nReturning to Start Menu.\n");
                break;
        }
    }

    private void AddIncome()
    {
        Transaction transaction = CreateTransaction(TransactionType.Income);
        TransactionStore.Instance.AddTransaction(transaction);
        transaction.PrintTransaction(transaction);
        Console.WriteLine("Income added successfully.\n");
    }

    private void AddExpense()
    {
        Transaction transaction = CreateTransaction(TransactionType.Expense);
        TransactionStore.Instance.AddTransaction(transaction);
        transaction.PrintTransaction(transaction);
        Console.WriteLine("Expense added successfully.\n");
    }

    private void PrintReportHeader(int padding)
    {
        Console.WriteLine(
            "Date".PadRight(11) + "| " +
            "Description".PadRight(padding) + "| " +
            "Amount".PadLeft(15)
        );
        Console.WriteLine(new string('-', padding + 33));
    }

    private void PrintReport(IEnumerable<Transaction> transactions)
    {
        if (!transactions.Any())
        {
            Console.WriteLine("No transactions found for the selected period.");
            return;
        }

        int maxDescriptionLength = transactions.Max(t => t.Description.Length);
        PrintReportHeader(maxDescriptionLength);
        foreach (Transaction transaction in transactions)
        {
            transaction.PrintTransaction(transaction, maxDescriptionLength);
        }
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
        string? input;
        do
        {
            Console.Write("Enter the amount (without currency symbol): ");
            input = Console.ReadLine();
            if (!decimal.TryParse(input, out amount))
            {
                Console.WriteLine("Invalid amount. Don't use symbols beside a comma or dot.");
            }
            else if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0.");
            }
        } while (!decimal.TryParse(input, out amount) || amount <= 0);

        Transaction transaction = new(DateTime.Now, description, amount, type);
        return transaction;
    }
}
