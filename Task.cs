namespace TaskManagerCLI;

public class TaskItem
{
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int TaskPriority { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public TaskItem() {}
    
    public TaskItem(string taskName, string taskDescription, int taskPriority)
    {
        this.TaskName = taskName;
        this.TaskDescription = taskDescription;
        this.TaskPriority = taskPriority;
        this.IsCompleted = false;
    }
}