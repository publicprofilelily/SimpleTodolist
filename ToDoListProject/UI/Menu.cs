using System;
using TodoList.Interfaces;

namespace TodoList.UI
{
    public abstract class Menu
    {
        protected ITaskManager TaskManager { get; private set; }
        protected IUserInterface UI { get; private set; }

        protected Menu(ITaskManager taskManager, IUserInterface ui)
        {
            TaskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager), "TaskManager must be provided.");
            UI = ui ?? throw new ArgumentNullException(nameof(ui), "User Interface must be initialized.");
        }

        // Display the menu and handle the user input within the menu
        
        public abstract void Display();

        protected string PromptInput(string prompt)
        {
            return UI.PromptInput(prompt);
        }

        protected void WaitForAnyKey(string message = "Press any key to continue")
        {
            UI.WaitForAnyKey(message);
        }
    }
}
