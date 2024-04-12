using System;
using TodoList.Data;
using TodoList.Data.Task;

namespace TodoList.UI
{
    public class UserInterface : IUserInterface
    {
        public void PrintHeader(string title)
        {
            ClearScreen();
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
           
        }

        public void PrintFooter()
        {
            Console.WriteLine(new string('-', 37));
            Console.ResetColor();
        }


        public void DisplaySortedTasks(IUserInterface ui, string sortBy)
        {
            var tm = new TaskManager();
            IEnumerable<ToDoTask> sortedTasks = tm.SortTasks(sortBy);
            ui.ClearScreen();
            ui.PrintHeader("Task List");
            
            foreach (var task in sortedTasks)
            
            {
                string status = task.IsDone ? "Completed" : "Pending";
                ui.DisplayMessage($"{task.Label} - {status} - Due: {task.DueDate.ToString("d")} - Project: {task.Project}", task.IsDone ? ConsoleColor.Green : ConsoleColor.Red);
            }
            
            ui.PrintFooter();
            ui.WaitForAnyKey();
        }



        public void SetConsoleColor(ConsoleColor color)
        
        {
            Console.ForegroundColor = color;
        }

        public string PromptInput(string promptMessage)
        
        {
            Console.Write($"{promptMessage}");
            return Console.ReadLine() ?? ""; // Handle possible null values
        }

        public void DisplayMessage(string message, ConsoleColor color = ConsoleColor.White)
        
        {
            SetConsoleColor(color);
            Console.WriteLine(message);
            Console.ResetColor();
        }


        public void DisplayWelcomeMessage()
        
        {
            //Magenta for border
            
            string topBottomBorder = new string('═', 30);
            Console.ResetColor(); 

            SetConsoleColor(ConsoleColor.Magenta);
            Console.WriteLine($"╔{topBottomBorder}╗");

            // Change to Cyan for letters
         
            Console.Write("║");
            SetConsoleColor(ConsoleColor.Cyan);  
            Console.Write("          WELCOME TO          ");
            SetConsoleColor(ConsoleColor.Magenta);  // Nagenta for the border
            Console.WriteLine("║");
            Console.Write("║");

            // Change back to Cyan again for letters
            
            SetConsoleColor(ConsoleColor.Cyan);  
            Console.Write("           TO-DO-LY           ");
            SetConsoleColor(ConsoleColor.Magenta);  
            Console.WriteLine("║");

            // Back to magenta

            Console.Write("╚");
            SetConsoleColor(ConsoleColor.Magenta);
            Console.WriteLine($"{topBottomBorder}╝");

            Console.WriteLine();

            Console.ResetColor();

            Console.WriteLine("Hello! Are you wondering what you should be doing today?");
            Console.WriteLine();
        }

        // Clear screen

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void PrintEmptyLine()
        { 
            Console.WriteLine(); 
        }


        public void WaitForAnyKey(string message = "Press any key to continue")
        {
            DisplayMessage(message, ConsoleColor.Cyan);  
            Console.ReadKey(true);  // Read single key press without displaying it
            Console.ResetColor();
        }

        public void DisplayOptions(string[] options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
        }

        // Gets Name of task

        public string CollectTaskInfo()
        {
            Console.WriteLine("Enter name of your task: ");
            string label = Console.ReadLine() ?? "";  // Read the task label from the user
            Console.WriteLine("Enter the status of the task (done/not done): ");
            string status = Console.ReadLine() ?? "";
            bool isDone = status.ToLower() == "done";
            return $"{label},{isDone}";  
        }
    }
}
