using System.Transactions;
using BudgetBook.Core;
using Transaction = BudgetBook.Core.Transaction;
using System.Text.Json;

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
        try
        {
            string fileName = $"{transaction.Date:yyyy-MM}.json";

            List<Transaction> transactions;
            if (File.Exists(fileName))
            {
                string json = await File.ReadAllTextAsync(fileName);
                transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? [];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Autosave Error]: {ex.Message}");
        }
    }
}
