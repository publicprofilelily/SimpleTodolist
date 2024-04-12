using System;
using System.Collections.Generic;
using TodoList.Data.Task;
using TodoList.UI;

namespace TodoList.Interfaces
{
    public interface ITaskManager
    {
        void LoadTasks(string fileName);
        bool SaveTasks(string fileName);
        void AddTask(string label, DateTime dueDate, string project);
        void AddTasks(List<string> taskLines);
        bool RemoveTask(int index);
        bool UpdateTask(int index, string newName, DateTime dueDate, string project);
        bool ToggleTaskIsDone(int index);
        int NumberOfTasks();
        int NumberOfDoneTasks();
        IEnumerable<ToDoTask> GetTaskList();
        void PrintCompletedTasks();
        bool GetTaskIsDoneStatus(int index);
        void DisplaySortedTasks(IUserInterface ui, string sortBy);  
        void DisplayTasksUnsorted(IUserInterface ui);
        string? GetTaskNameAtIndex(int index);
        string? GetTaskDueDateAtIndex(int index);
        string? GetTaskProjectAtIndex(int index);
    }
}
