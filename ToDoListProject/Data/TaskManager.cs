using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoList.Data.Task;
using TodoList.Interfaces;

namespace TodoList.Data
{
    public class TaskManager : ITaskManager
    {
        private List<ToDoTask> tasks = new List<ToDoTask>();

        // Display Unsorted
        
        public void DisplayTasksUnsorted(IUserInterface ui)
        {
            ui.ClearScreen();
            ui.PrintHeader("Unsorted Task List");
            foreach (var task in tasks)
            {
                string status = task.IsDone ? "Completed" : "Pending";
                ui.DisplayMessage($"{task.Label} - {status} - Due: {task.DueDate.ToString("d")} - Project: {task.Project}", task.IsDone ? ConsoleColor.Green : ConsoleColor.Red);
            }
            ui.PrintFooter();
            ui.WaitForAnyKey();
        }

        // Display tasks sorted 
       
        public void DisplaySortedTasks(IUserInterface ui, string sortBy)
        {
            IEnumerable<ToDoTask> sortedTasks = SortTasks(sortBy);
            ui.ClearScreen();
            ui.PrintHeader("Sorted Task List");
            foreach (var task in sortedTasks)
            {
                string status = task.IsDone ? "Completed" : "Pending";
                ui.DisplayMessage($"{task.Label} - {status} - Due: {task.DueDate.ToString("d")} - Project: {task.Project}", task.IsDone ? ConsoleColor.Green : ConsoleColor.Red);
            }
            ui.PrintFooter();
            ui.WaitForAnyKey();
        }

        // Helper method to sort tasks based on the provided sort key

        public IEnumerable<ToDoTask> SortTasks(string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "date":
                    return tasks.OrderBy(t => t.DueDate).ThenBy(t => t.Project);
                case "project":
                    return tasks.OrderBy(t => t.Project).ThenBy(t => t.DueDate);
                default:
                    return tasks; // Return unsorted if no valid sort option is provided
            }
        }

        // Load tasks from a file

        public void LoadTasks(string fileName)
        {
            tasks.Clear();
            List<string> loadedData = new List<string>();
            if (!LoadFile(fileName, loadedData))
            {
                Console.WriteLine("Failed to load tasks from file.");
                return;
            }
            AddTasks(loadedData);
        }

        private bool LoadFile(string filename, List<string> outData)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        outData.Add(line);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read file: {ex.Message}");
                return false;
            }
        }

        // Add just on task

        public void AddTask(string label, DateTime dueDate, string project)
        {
            tasks.Add(new ToDoTask(label, false, dueDate, project));
        }

        // Add multiple tasks from loaded data

        public void AddTasks(List<string> taskLines)
        {
            for (int i = 0; i < taskLines.Count; i += 4)
            {
                if (taskLines.Count > i + 3)  // Check for enough lines to form a complete task
                {
                    string label = taskLines[i];
                    bool isDone = taskLines[i + 1] == "1";
                    DateTime dueDate = DateTime.Parse(taskLines[i + 2]);
                    string project = taskLines[i + 3];
                    tasks.Add(new ToDoTask(label, isDone, dueDate, project));
                }
            }
        }

        // Save tasks to file

        public bool SaveTasks(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (ToDoTask task in tasks)
                    {
                        writer.WriteLine(task.Label);
                        writer.WriteLine(task.IsDone ? "1" : "0");
                        writer.WriteLine(task.DueDate.ToString("yyyy-MM-dd"));
                        writer.WriteLine(task.Project);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save file: {ex.Message}");
                return false;
            }
        }


        public bool GetTask(int index, out ToDoTask? task)
        {
            task = index >= 0 && index < tasks.Count ? tasks[index] : null;
            return task != null;
        }

        public bool RemoveTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks.RemoveAt(index);
                return true;
            }
            return false;
        }

        public int NumberOfTasks() => tasks.Count;

        public int NumberOfDoneTasks() => tasks.Count(t => t.IsDone);

        public IEnumerable<ToDoTask> GetTaskList() => tasks;

        public string? GetTaskNameAtIndex(int index)
        {
            return index >= 0 && index < tasks.Count ? tasks[index].Label : null;
        }

        public string? GetTaskDueDateAtIndex(int index)
        {
            return index >= 0 && index < tasks.Count ? tasks[index].DueDate.ToString("d") : null;
        }

        public string? GetTaskProjectAtIndex(int index)
        {
            return index >= 0 && index < tasks.Count ? tasks[index].Project : null;
        }

        public bool UpdateTask(int index, string newName, DateTime dueDate, string project )
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].Label = newName;
                tasks[index].DueDate = dueDate;
                tasks[index].Project = project;
                return true;
            }
            return false;
        }

        public bool ToggleTaskIsDone(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].IsDone = !tasks[index].IsDone;
                return true;
            }
            return false;
        }

        public void PrintCompletedTasks()
        {
            foreach (var task in tasks.Where(t => t.IsDone))
            {
                Console.WriteLine($"{task.Label} - Completed");
            }
        }

        public bool GetTaskIsDoneStatus(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                return tasks[index].IsDone;
            }
            return false;
        }
    }
}
