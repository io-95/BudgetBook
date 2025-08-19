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

    private readonly List<Task> _runningTasks = [];
    private readonly object _lock = new();
    private void OnTransactoinAdded(Object? sender, Transaction transaction)
    {
        Task task = SaveTransactionAsync(transaction);

        lock (_lock)
        {
            _runningTasks.Add(task);
        }

        task.ContinueWith(t =>
        {
            lock (_lock)
            {
                _runningTasks.Remove(task);
            }
        });
    }

    private async Task SaveTransactionAsync(Transaction transaction)
    {

    }
}
