namespace BudgetBook.Core;

public class TransactionStore
{
    private readonly List<Transaction> _transactions;

    public TransactionStore()
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

    public IEnumerable<Transaction> GetTransactionsByMonth(int month, int year)
    {
        return _transactions.Where(t => t.Date.Month == month && t.Date.Year == year);
    }
}
