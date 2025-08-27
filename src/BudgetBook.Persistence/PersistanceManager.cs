using System.Transactions;
using BudgetBook.Core;
using Transaction = BudgetBook.Core.Transaction;
using System.Text.Json;

namespace BudgetBook.Persistence;

public class PersistenceManager : IDisposable, IAsyncDisposable
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

        AddRunningTask(task);

        task.ContinueWith(t => RemoveRunningTask(task));
    }

    private void AddRunningTask(Task task)
    {
        lock (_lock)
        {
            _runningTasks.Add(task);
        }
    }

    private void RemoveRunningTask(Task task)
    {
        lock (_lock)
        {
            _runningTasks.Remove(task);
        }
    }

    private string CreateBasePath()
    {
        string basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "BudgetBook"
            );
        return basePath;
    }

    private async Task SaveTransactionAsync(Transaction transaction)
    {
        try
        {
            string basePath = CreateBasePath();
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

    public async Task LoadTransactionsAsync(int year, int month)
    {
        try
        {
            string basePath = CreateBasePath();
            Directory.CreateDirectory(basePath);

            string fileName = Path.Combine(basePath, $"{year:D4}-{month:D2}.json");

            List<Transaction> transactions = [];
            if (File.Exists(fileName))
            {
                string json = await File.ReadAllTextAsync(fileName);
                transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? [];

                foreach (Transaction tx in transactions)
                {
                    TransactionStore.Instance.AddTransactionSilently(tx);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Loading Error]: {ex.Message}");
        }
    }

    /// <summary>
    /// Is waiting for all running write processes.
    /// Should be called on App-Shutdown.
    /// </summary>
    public async Task FlushAsync()
    {
        Task[] tasksCopy;
        lock (_lock)
        {
            tasksCopy = _runningTasks.ToArray();
        }
        await Task.WhenAll(tasksCopy);
    }

    public void Dispose()
    {
        TransactionStore.Instance.TransactionAdded -= OnTransactionAdded;
    }

    public async ValueTask DisposeAsync()
    {
        await FlushAsync();

        Dispose();
    }
}
