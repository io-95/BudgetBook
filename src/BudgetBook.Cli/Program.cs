using BudgetBook.Core;
using BudgetBook.Persistence;

using var persistence = new PersistenceManager();

int choice = 4; // Default choice is 4 (Exit)

TransactionManager transactionManager = new TransactionManager(PrintMenu.PrintReportMenu);
Console.WriteLine("Welcome to the Budget Book CLI!\n");



while (true)
{
    choice = PrintMenu.PrintStartMenu();
    transactionManager.StartMenuHandler(choice);
}
