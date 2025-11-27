using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskTracker
{
    public enum TaskStatus
    { ToDo, InProgress, Done }

    public class TaskItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Title} [{Status}]";
        }
    }

    public class TaskBoard
    {
        private readonly List<TaskItem> _tasks = new();
        public void AddTask(TaskItem task) => _tasks.Add(task);
        public List<TaskItem> GetTasks() => _tasks.ToList();
        public List<TaskItem> GetTasksByStatus(TaskStatus status) => _tasks.Where(t => t.Status == status).ToList();
        public TaskItem? GetTaskById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);
        public bool RemoveTask(Guid id) => _tasks.RemoveAll(t => t.Id == id) > 0;
    }
    
    public class TaskApp
    {
        private readonly TaskBoard _board = new();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Task\n2. View Tasks\n3. Change Status\n4. Filter\n5. Delete Task\n0. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTask(); break;
                    case "2": ViewTasks(); break;
                    case "3": ChangeStatus();break;
                    case "4": Filter(); break;
                    case "5": Delete(); break;
                    case "0": return;
                    default: Console.WriteLine("Unknown option."); break;
                }
            }
        }
        private void AddTask()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
            Console.Write("Description: ");
            var description = Console.ReadLine();
            var task = new TaskItem
            {
                Title = title.Trim(),
                Description = description?.Trim()
            };
            _board.AddTask(task);
            Console.WriteLine("Task added.");
        }
        private void ViewTasks()
        {
            var tasks = _board.GetTasks();
            if(!tasks.Any()) Console.WriteLine("No Tasks");
            else tasks.ForEach(t => Console.WriteLine($"{t.Id} - {t}"));
        }

        private void ChangeStatus()
        {
            Console.WriteLine("Enter task ID: ");
            if(Guid.TryParse(Console.ReadLine(), out var id)){
                var task = _board.GetTaskById(id);
                if(task == null) Console.WriteLine("Task not found.");
                else
                {
                    Console.WriteLine("Set new status (ToDo, InProgress, Done):");
                    if(Enum.TryParse<TaskStatus>(Console.ReadLine(),true, out var newStatus))
                    {
                        task.Status = newStatus;
                        Console.WriteLine("Status updated");
                    }
                    else Console.WriteLine("Invalid status.");
                }
            }
            else Console.WriteLine("Invalid ID");

        }

        private void Filter()
        {
            Console.Write("Status(ToDo, InProgress, Done): ");
            if (Enum.TryParse<TaskStatus>(Console.ReadLine (), true, out var status))
            {
                var tasks = _board.GetTasksByStatus(status);
                tasks.ForEach ( t => Console.WriteLine($"{t.Id} - {t}"));
            }
            else Console.WriteLine("Invalid Status.");
        }

        private void Delete()
        {
            Console.Write("Enter task id to delete: ");
            if(Guid.TryParse(Console.ReadLine(), out var id))
            {
                if (_board.RemoveTask(id)) Console.WriteLine("Deleted.");
                else Console.WriteLine("Task not found.");
            }
            else Console.WriteLine("Invalid ID.");
        }


    }
    class Program
    {
        static void Main()
        {
            var app = new TaskApp();
            app.Run();
        }
    }
}