using System.Text.Json;

namespace TaskManagerCLI;

public class TaskManager
{
    private List<TaskItem> _tasks = new List<TaskItem>();
    private readonly string _filePath = "tasks.json";

    public TaskManager()
    {
        LoadTasks();
    }

    private void LoadTasks()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            else
            {
                _tasks = new List<TaskItem>();
                SaveTasks();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks.json: {ex.Message}");
            _tasks = new List<TaskItem>();
        }
    }

    public void SaveTasks()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_tasks, options);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks: {ex.Message}");
        }
    }
    
    public IReadOnlyList<TaskItem> Tasks => _tasks.AsReadOnly();

    public void AddTask(TaskItem task)
    {
        _tasks.Add(task);
        SaveTasks();
    }

    public void RemoveTask(TaskItem task)
    {
        _tasks.Remove(task);
        SaveTasks();
    }

    public void ToggleTaskCompletion(TaskItem task)
    {
        task.IsCompleted = !task.IsCompleted;
        SaveTasks();
    }
    
    public TaskItem GetTask(int index) =>  _tasks[index];
    
    public List<string> UpdateTask(TaskItem task, string? name = null, string? description = null, int? priority = null)
    {
        List<string> updateList = new List<string>();

        if (!string.IsNullOrWhiteSpace(name) && task.TaskName != name)
        {
            task.TaskName = name;
            updateList.Add("name");
        }

        if (!string.IsNullOrWhiteSpace(description) && task.TaskDescription != description)
        {
            task.TaskDescription = description;
            updateList.Add("description");
        }

        if (priority.HasValue && task.TaskPriority != priority.Value)
        {
            task.TaskPriority = priority.Value;
            updateList.Add("priority");
        }
        
        SaveTasks();
        
        return updateList;
    }
}