using System.Transactions;
using BudgetBook.Core;
using Transaction = BudgetBook.Core.Transaction;

namespace BudgetBook.Persistence;

public class PersistenceManager
{
    public PersistenceManager()
    {
        TransactionStore.Instance.TransactionAdded += OnTransactoinAdded;
    }

    private void OnTransactoinAdded(Object? sender, Transaction transaction)
    {
        Task task = SaveTransactionAsync(transaction);
    }

    private async Task SaveTransactionAsync(Transaction transaction)
    {

    }
}
