using BudgetBook.Core;
using BudgetBook.Persistence;

using var persistence = new PersistenceManager();

int choice = 4; // Default choice is 4 (Exit)

TransactionManager transactionManager = new TransactionManager(PrintMenu.PrintReportMenu);
Console.WriteLine("Welcome to the Budget Book CLI!\n");

bool running = true;

while (running)
{
    choice = PrintMenu.PrintStartMenu();
    running = transactionManager.StartMenuHandler(choice);
}
