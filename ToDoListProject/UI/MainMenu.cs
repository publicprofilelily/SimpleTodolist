using TodoList.Interfaces;
using System.Globalization;

namespace TodoList.UI
{
    public class MainMenu : Menu
    {
        public MainMenu(ITaskManager taskManager, IUserInterface ui) : base(taskManager, ui) { }

        public override void Display()
        {
            bool running = true;
            while (running)
            {
                UI.ClearScreen();
                UI.DisplayWelcomeMessage();

                DisplayTaskSummary(); 

                UI.DisplayOptions(new string[] {
                    "1. Show task list",
                    "2. Add a new task!",
                    "3. Edit Task-Name, Due-date and Project-name",
                    "4. Toggle task completion (done/not done)",
                    "5. Save and exit when you are done"
                });

                UI.PrintEmptyLine();
                
                string choice = UI.PromptInput("Please enter a number matching your preffered course of action: ");
                
                running = ProcessInput(choice);
            }
        }

        private bool ProcessInput(string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplaySortedTasks();
                    break;
                
                case "2":
                    AddNewTask();
                    break;
                
                case "3":
                    EditTasks();
                    break;
                
                case "4":
                    ToggleTaskCompletion();
                    break;
                
                case "5":
                    TaskManager.SaveTasks("tasks.txt"); 
                    return false;
                
                default:
                    UI.DisplayMessage("Invalid option, please try again.");
                    break;
            }
            UI.WaitForAnyKey();
            return true;
        }

        public void DisplayTaskSummary()
        {
            int totalTasks = TaskManager.NumberOfTasks();
            int doneTasks = TaskManager.NumberOfDoneTasks();

            UI.DisplayMessage($"At the moment you have {totalTasks} tasks left, and {doneTasks} task or tasks are");
            UI.DisplayMessage("Completed", ConsoleColor.Green);
            UI.PrintEmptyLine();
            UI.DisplayMessage("What more do you want to know or do?", ConsoleColor.Magenta);
            UI.PrintEmptyLine();

        }

        // Displays tasks sorted after date or project depending on choice

        private void DisplaySortedTasks()
        {
            bool validInput = false;
            while (!validInput)
            {
                UI.DisplayMessage("Sort tasks by: 1. Date, 2. Project, or 3. Display unsorted.");
                string sortChoice = UI.PromptInput("Choose an option (or type 'exit' to go back):");
                switch (sortChoice)
                {
                    case "1":
                        TaskManager.DisplaySortedTasks(UI, "date");
                        validInput = true;
                        break;
                    
                    case "2":
                        TaskManager.DisplaySortedTasks(UI, "project");
                        validInput = true;
                        break;
                    
                    case "3":
                        TaskManager.DisplayTasksUnsorted(UI);
                        validInput = true;
                        break;
                    
                    case "exit":
                        return; 
                    
                    default:
                        UI.DisplayMessage("Invalid option, please try again.", ConsoleColor.Red);
                        break;
                }
            }
        }

        // Add a new task to list
        
        private void AddNewTask()
        {
            string label = UI.PromptInput("Enter name of task:");
            string dateString = UI.PromptInput("Enter due date (yyyy-MM-dd):");
            DateTime dueDate;
            
            while (!DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
            {
                UI.DisplayMessage("Invalid date format, please use yyyy-MM-dd format.", ConsoleColor.Red);
                dateString = UI.PromptInput("Enter due date (yyyy-MM-dd):");
            }
            
            string project = UI.PromptInput("Enter project name:");
            TaskManager.AddTask(label, dueDate, project);
        }

        private void EditTasks()
        {
            Menu editMenu = new EditMenu(TaskManager, UI);
            editMenu.Display();  
        }

        private void ToggleTaskCompletion()
        {
            string indexInput = UI.PromptInput("Enter task index to toggle completion:");
            
            if (int.TryParse(indexInput, out int toggleIndex) && toggleIndex > 0 && toggleIndex <= TaskManager.NumberOfTasks())
            {
                TaskManager.ToggleTaskIsDone(toggleIndex - 1);  // Adjusts for a zero-based index
            }
            
            else
            {
                UI.DisplayMessage("Invalid task index.", ConsoleColor.Red);
            }
        }
    }
}
