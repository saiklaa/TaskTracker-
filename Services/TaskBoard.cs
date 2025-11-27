using TaskTracker.Models;

namespace TaskTracker.Services;

public class TaskBoard
{
    private readonly List<TaskItem> _tasks = new();
        
    public void AddTask(TaskItem task) => _tasks.Add(task);
    public List<TaskItem> GetTasks() => _tasks.ToList();
    public List<TaskItem> GetTasksByStatus(TaskStatus status) => _tasks.Where(t => t.Status == status).ToList();
    public TaskItem? GetTaskById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);
    public bool RemoveTask(Guid id) => _tasks.RemoveAll(t => t.Id == id) > 0;
}