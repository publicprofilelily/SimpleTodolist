using TodoList.Data;
using TodoList.Interfaces;
using TodoList.UI;

namespace TodoList
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ITaskManager taskManager = new TaskManager();
            
            // Loads the file
            
            taskManager.LoadTasks("tasks.txt");
            
            IUserInterface ui = new UserInterface();
            Menu mainMenu = new MainMenu(taskManager, ui);
            
            // Starts the menu

            mainMenu.Display(); 
        }
    }
}

