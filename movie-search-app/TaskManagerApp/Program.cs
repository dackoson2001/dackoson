using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Task Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddTask(taskManager);
                    break;
                case "2":
                    ViewTasks(taskManager);
                    break;
                case "3":
                    UpdateTask(taskManager);
                    break;
                case "4":
                    DeleteTask(taskManager);
                    break;
                case "5":
                    return;
            }
        }
    }

    static void AddTask(TaskManager taskManager)
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        Console.Write("Enter task priority (Low, Medium, High): ");
        Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);
        Console.Write("Enter deadline (yyyy-mm-dd): ");
        DateTime deadline = DateTime.Parse(Console.ReadLine());

        Task newTask = new Task
        {
            Title = title,
            Description = description,
            Priority = priority,
            Deadline = deadline
        };

        taskManager.AddTask(newTask);
        Console.WriteLine("Task added successfully!");
        Console.ReadKey();
    }

    static void ViewTasks(TaskManager taskManager)
    {
        var tasks = taskManager.GetTasks();
        Console.WriteLine("\nTasks List:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Priority: {task.Priority}, Deadline: {task.Deadline.ToShortDateString()}, Completed: {task.IsCompleted}");
        }
        Console.ReadKey();
    }

    static void UpdateTask(TaskManager taskManager)
    {
        Console.Write("Enter task ID to update: ");
        int id = int.Parse(Console.ReadLine());
        Task taskToUpdate = taskManager.GetTasks().FirstOrDefault(t => t.Id == id);

        if (taskToUpdate != null)
        {
            Console.Write("Enter new title: ");
            taskToUpdate.Title = Console.ReadLine();
            Console.Write("Enter new description: ");
            taskToUpdate.Description = Console.ReadLine();
            Console.Write("Enter new priority: ");
            taskToUpdate.Priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);
            Console.Write("Enter new deadline: ");
            taskToUpdate.Deadline = DateTime.Parse(Console.ReadLine());

            taskManager.UpdateTask(id, taskToUpdate);
            Console.WriteLine("Task updated successfully!");
        }
        else
        {
            Console.WriteLine("Task not found!");
        }
        Console.ReadKey();
    }

    static void DeleteTask(TaskManager taskManager)
    {
        Console.Write("Enter task ID to delete: ");
        int id = int.Parse(Console.ReadLine());
        taskManager.DeleteTask(id);
        Console.WriteLine("Task deleted successfully!");
        Console.ReadKey();
    }
}
