using System.Transactions;
using BudgetBook.Core;
using Transaction = BudgetBook.Core.Transaction;
using System.Text.Json;

namespace BudgetBook.Persistence;

public class PersistenceManager : IDisposable
{
    public PersistenceManager()
    {
        TransactionStore.Instance.TransactionAdded += OnTransactionAdded;
    }

    private readonly List<Task> _runningTasks = [];
    private readonly object _lock = new();
    private void OnTransactionAdded(Object? sender, Transaction transaction)
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
            string basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "BudgetBook"
            );
            Directory.CreateDirectory(basePath);

            string fileName = Path.Combine(basePath, $"{transaction.Date:yyyy-MM}.json");

            List<Transaction> transactions = [];
            if (File.Exists(fileName))
            {
                string json = await File.ReadAllTextAsync(fileName);
                transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? [];
            }

            transactions.Add(transaction);

            string newJson = JsonSerializer.Serialize(
                transactions,
                new JsonSerializerOptions { WriteIndented = true }
            );

            await File.WriteAllTextAsync(fileName, newJson);

            Console.WriteLine($"\n[Autosave] Transaction saved to {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Autosave Error]: {ex.Message}");
        }
    }

    public void Dispose()
    {
        TransactionStore.Instance.TransactionAdded -= OnTransactionAdded;
    }
}
