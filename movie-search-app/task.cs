// Task.cs
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High
}
