using BudgetBook.Core;

int choice = 4; // Default choice is 4 (Exit)

Console.WriteLine("Welcome to the Budget Book CLI!");

while (true)
{
    choice = PrintStartMenu();
    StartMenuHandler(choice);
}
