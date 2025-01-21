// TaskManager.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class TaskManager
{
    private List<Task> _tasks;

    public TaskManager()
    {
        _tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        task.Id = _tasks.Count + 1;
        _tasks.Add(task);
    }

    public List<Task> GetTasks()
    {
        return _tasks;
    }

    public void UpdateTask(int taskId, Task updatedTask)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.Deadline = updatedTask.Deadline;
            task.IsCompleted = updatedTask.IsCompleted;
        }
    }

    public void DeleteTask(int taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            _tasks.Remove(task);
        }
    }
}
