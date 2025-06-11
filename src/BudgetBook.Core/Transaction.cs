namespace BudgetBook.Core;

public enum TransactionType
{
    Income,
    Expense
}
public class Transaction
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }

    public Transaction(DateTime date, string description, decimal amount, TransactionType type)
    {
        Date = date;
        Description = description;
        Amount = amount;
        Type = type;
    }

    public void PrintTransaction(Transaction transaction)
    {
        Console.WriteLine($"{transaction.Date.ToShortDateString()} | {transaction.Description} | ");
        if (transaction.Type == TransactionType.Income)
        {
            Console.WriteLine($"+ {transaction.Amount:C}");
        }
        else
        {
            Console.WriteLine($"- {transaction.Amount:C}");
        }
    }
}
