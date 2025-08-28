namespace BudgetBook.Core;

public enum TransactionType
{
    Income,
    Expense
}
public class Transaction
{
    public Guid Id { get; init; } = Guid.NewGuid();
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

    public void PrintTransaction(Transaction transaction, int padding = 0)
    {
        Console.Write($"{transaction.Date.ToShortDateString()} | {transaction.Description.PadRight(padding)}| ");
        string amountFormatted = transaction.Amount.ToString("C");
        if (transaction.Type == TransactionType.Income)
        {
            Console.WriteLine($"+ {amountFormatted, 15}");
        }
        else
        {
            Console.WriteLine($"- {amountFormatted, 15}");
        }
    }
}
