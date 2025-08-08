namespace BudgetBook.Core;

public sealed class TransactionStore
{
    private static readonly Lazy<TransactionStore> _instance = new(() => new TransactionStore());

    public static TransactionStore Instance => _instance.Value;

    private readonly List<Transaction> _transactions;

    private TransactionStore()
    {
        _transactions = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public IEnumerable<Transaction> GetTransactionsByDay(int day, int month, int year)
    {
        return _transactions.Where(t => t.Date.Day == day && t.Date.Month == month && t.Date.Year == year);
    }
    public IEnumerable<Transaction> GetTransactionsByMonth(int month, int year)
    {
        return _transactions.Where(t => t.Date.Month == month && t.Date.Year == year);
    }
}
