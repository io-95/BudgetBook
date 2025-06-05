using BudgetBook.Core;

int choice = 4; // Default choice is 4 (Exit)

TransactionManager transactionManager = new TransactionManager(PrintMenu.PrintReportMenu);
Console.WriteLine("Welcome to the Budget Book CLI!");

while (true)
{
    choice = PrintMenu.PrintStartMenu();
    transactionManager.StartMenuHandler(choice);
}
