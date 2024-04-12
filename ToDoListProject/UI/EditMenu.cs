using System;
using System.Globalization;
using TodoList.Interfaces;
using TodoList.UI;

namespace TodoList
{
    public class EditMenu : Menu
    {
        public EditMenu(ITaskManager taskManager, IUserInterface ui) : base(taskManager, ui) { }

        public override void Display()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                UI.ClearScreen();
                UI.SetConsoleColor(ConsoleColor.Magenta);
                UI.DisplayMessage("Edit tasks (q to quit)\n-------------");
                UI.SetConsoleColor(ConsoleColor.Magenta);
                UI.DisplayOptions(new string[] {
                    "1. Update",
                    "2. Mark as Done",
                    "3. Remove"
                });
                UI.SetConsoleColor(ConsoleColor.White);
                UI.DisplayMessage("-------------");

                string input = UI.PromptInput("Enter your choice: ").ToLower();
                continueRunning = ProcessInput(input);
            }
        }

        public bool ProcessInput(string input)
        {
            switch (input)
            {
                case "1":
                    EditTask(Command.Update);
                    break;
                
                case "2":
                    EditTask(Command.ToggleDone);
                    break;
                
                case "3":
                    EditTask(Command.Remove);
                    break;
                
                case "q":
                    return false; // Exit the loop and closes the menu
                
                default:
                    UI.DisplayMessage("Invalid option. Please try again.", ConsoleColor.Red);
                    break;
            }
            return true; // Displays the menu
        }

        private void EditTask(Command command)
        {
            UI.ClearScreen();
            UI.SetConsoleColor(ConsoleColor.Magenta);
            PrintIndexedTaskList();
            UI.SetConsoleColor(ConsoleColor.White);
            string input = UI.PromptInput("Enter task index or 'q' to quit:");
            if (input.ToLower() == "q")
                return;

            
            if (int.TryParse(input, out int index) && IsValidIndex(--index))
            {
                // Adjusts the index so it is zero-based
                
                switch (command)
                {
                    case Command.Update:
                        UpdateTask(index);
                        break;
                    case Command.ToggleDone:
                        TaskManager.ToggleTaskIsDone(index);
                        UI.DisplayMessage("Task status toggled.", ConsoleColor.Green);
                        break;
                    case Command.Remove:
                        TaskManager.RemoveTask(index);
                        UI.DisplayMessage("Task removed successfully.", ConsoleColor.Green);
                        break;
                }
            }
            else
            {
                UI.DisplayMessage("Invalid task index.", ConsoleColor.Red);
            }
        }

        private bool IsValidIndex(int index)
        {
            // Checks if index is within the bounds of list

            return index >= 0 && index < TaskManager.NumberOfTasks();
        }

        private void UpdateTask(int index)
        {
            string currentName = TaskManager.GetTaskNameAtIndex(index);
            string dueDate = TaskManager.GetTaskDueDateAtIndex(index);
            string project = TaskManager.GetTaskProjectAtIndex(index);

            if (currentName == null)
            {
                UI.DisplayMessage("Task not found.", ConsoleColor.Red);
                return;
            }

            string newName = UI.PromptInput($"Enter a new due Date for \"{currentName}\": ");
            string newDueDateString = UI.PromptInput($"Enter a new due Date for \"{newName}\"(yyyy-MM-dd): ");
            DateTime newDueDate;
            while (!DateTime.TryParseExact(newDueDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDueDate))
            {
                UI.DisplayMessage("Invalid date format, please use yyyy-MM-dd format.", ConsoleColor.Red);
                newDueDateString = UI.PromptInput("Enter due date (yyyy-MM-dd): ");
            }
            
            string newProject = UI.PromptInput($"Enter a new project for \"{newName}\": ");
            if (!string.IsNullOrEmpty(newName) && TaskManager.UpdateTask(index, newName,newDueDate,newProject))
            {
                UI.DisplayMessage("Task Updated successfully.", ConsoleColor.Green);
            }
            else
            {
                UI.DisplayMessage("Failed to update task. Name cannot be empty.", ConsoleColor.Red);
            }

        }

        protected void PrintIndexedTaskList()
        {
            int index = 1; 
            foreach (var task in TaskManager.GetTaskList())
            {
                UI.DisplayMessage($"{task.Label} - {(task.IsDone ? "Completed" : "Pending")} - Due: {task.DueDate.ToString("d")} - Project: {task.Project}");             
                index++;
            }
        }
    }
}
