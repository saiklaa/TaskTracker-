namespace TaskTracker.Models;

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
