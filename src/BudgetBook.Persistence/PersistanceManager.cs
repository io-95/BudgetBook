using System.Transactions;
using BudgetBook.Core;

namespace BudgetBook.Persistence;

public class PersistenceManager
{
    public PersistenceManager()
    {
        TransactionStore.Instance.TransactionAdded += async (sender, Transaction) =>
        {

        };
    }
}
