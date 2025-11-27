using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker;

public class TaskApp
{
    private readonly TaskBoard _board = new();

    public void Run()
    {
        while (true)
        {
            try{
                Console.WriteLine("1. Add Task\n2. View Tasks\n3. Change Status\n4. Filter\n5. Delete Task\n0. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTask(); break;
                    case "2": ViewTasks(); break;
                    case "3": ChangeStatus(); break;
                    case "4": Filter(); break;
                    case "5": Delete(); break;
                    case "0": return;
                    default: Console.WriteLine("Unknown option."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void AddTask()
    {
        try{
            Console.Write("Title: ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
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
        catch(Exception ex)
        {
            Console.WriteLine($"Failed to add task: {ex.Message}");
        }
    }

    private void ViewTasks()
    {
        try{
            var tasks = _board.GetTasks();
            if (!tasks.Any()) Console.WriteLine("No Tasks");
            else tasks.ForEach(t => Console.WriteLine($"{t.Id} - {t}"));
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Failed to view tasks: {ex.Message}");
        }   
    }

    private void ChangeStatus()
    {
        try
        {
            Console.WriteLine("Enter task ID: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var task = _board.GetTaskById(id);
                if (task == null) Console.WriteLine("Task not found.");
                else
                {
                    Console.WriteLine("Set new status (ToDo, InProgress, Done):");
                    if (Enum.TryParse<TaskStatus>(Console.ReadLine(), true, out var newStatus))
                    {
                        task.Status = newStatus;
                        Console.WriteLine("Status updated");
                    }
                    else Console.WriteLine("Invalid status.");
                }
            }
            else Console.WriteLine("Invalid ID");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to change status: {ex.Message}");
        }
    }

    private void Filter()
    {
        try
        {
            Console.Write("Status(ToDo, InProgress, Done): ");
            if (Enum.TryParse<TaskStatus>(Console.ReadLine(), true, out var status))
            {
                var tasks = _board.GetTasksByStatus(status);
                tasks.ForEach(t => Console.WriteLine($"{t.Id} - {t}"));
            }
            else Console.WriteLine("Invalid Status.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to filter tasks: {ex.Message}");
        }
    }

    private void Delete()
    {
        try
        {
            Console.Write("Enter task id to delete: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                if (_board.RemoveTask(id)) Console.WriteLine("Deleted.");
                else Console.WriteLine("Task not found.");
            }
            else Console.WriteLine("Invalid ID.");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Failed to delete task: {ex.Message}");
        }
    }
}