using System;

namespace TodoList.Data.Task
{
    public class ToDoTask
    {
        public string Label { get; set; }
        public bool IsDone { get; set; }
        public DateTime DueDate { get; set; }
        public string Project { get; set; }

        public ToDoTask(string label, bool isDone, DateTime dueDate, string project)
        {
            Label = label;
            IsDone = isDone;
            DueDate = dueDate;
            Project = project;
        }
    }
}
