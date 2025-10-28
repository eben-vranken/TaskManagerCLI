namespace TaskManagerCLI;

public class TaskManager
{
    private List<TaskItem> _tasks = new List<TaskItem>();
    
    public IReadOnlyList<TaskItem> Tasks => _tasks.AsReadOnly();
    
    public void AddTask(TaskItem task) => _tasks.Add(task);
    
    public void RemoveTask(TaskItem task) => _tasks.Remove(task);
    
    public TaskItem GetTask(int index) =>  _tasks[index];

    public bool UpdateTask(TaskItem task, string? name = null, string? description = null, int? priority = null)
    {
        bool updated = false;

        if (!string.IsNullOrWhiteSpace(name) && task.TaskName != name)
        {
            task.TaskName = name;
            updated = true;
        }

        if (!string.IsNullOrWhiteSpace(description) && task.TaskDescription != description)
        {
            task.TaskDescription = description;
            updated = true;
        }

        if (priority.HasValue && task.TaskPriority != priority.Value)
        {
            task.TaskPriority = priority.Value;
            updated = true;
        }
    
        return updated;
    }
}