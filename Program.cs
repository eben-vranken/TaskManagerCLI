namespace TaskManagerCLI;

class Program
{
    // Menu options
    private static readonly Dictionary<int, string> MenuOptions = new Dictionary<int, string>()
    {
        {1, "List all tasks"},
        {2, "Create new task"},
        {3, "Toggle task completion"},
        {4, "Update task"},
        {5, "Delete task"},
        {0, "Exit"}
    };
    
    private static TaskManager _taskManager = new TaskManager();
    
    static void Main(string[] args)
    {
        
        string userInput = "";
        while (userInput != "0")
        {
            PrintMenu();
            userInput = GetValidUserInput("What would you like to do?", ConsoleColor.Green, MenuOptions.Keys.Select(k => k.ToString()).ToArray());
            
            Console.Clear();
            
            HandleUserInput(userInput);
        }
    }

    static void HandleUserInput(string userInput)
    {
        switch (userInput)
        {
            case "1":
                ListAllTasks();
                break;
            case "2":
                CreateNewTask();
                break;
            case "3":
                ToggleTaskCompletion();
                break;
            case "4":
                UpdateTask();
                break;
            case "5":
                DeleteTask();
                break;
            case "0":
                PrintLineColor("Exiting TaskManagerCLI", ConsoleColor.Red);
                break;
            default:
                PrintLineColor("Unexpected input!", ConsoleColor.Red);
                break;
        }
    }
    
    /// <summary>
    /// This function shows all _tasks.
    /// </summary>
    static void ListAllTasks()
    {
        PrintLineColorWithUnderline("All tasks", ConsoleColor.Yellow);
        
        if (_taskManager.Tasks.Count == 0)
        {
            PrintLineColor("No tasks found!", ConsoleColor.Red);
        }
        else
        {
            for (int i = 0; i < _taskManager.Tasks.Count; i++)
            {
                PrintLineColor($"Task {i+1}", ConsoleColor.Magenta);
                
                PrintColor($"Title: ", ConsoleColor.Red);
                PrintLineColor(_taskManager.Tasks[i].TaskName, ConsoleColor.Green);
                
                PrintColor("Description: ", ConsoleColor.Red);
                PrintLineColor(_taskManager.Tasks[i].TaskDescription, ConsoleColor.Green);
                
                PrintColor("Priority: ", ConsoleColor.Red);
                PrintLineColor(_taskManager.Tasks[i].TaskPriority.ToString(), ConsoleColor.Green);

                if (_taskManager.Tasks[i].IsCompleted)
                {
                    PrintColor("✅ Completed", ConsoleColor.Green);
                }
                else
                {
                    PrintColor("❌ Not Completed", ConsoleColor.Red);
                }

            }
        }
        
        Console.WriteLine();
    }
    
    /// <summary>
    /// This function creates a new task.
    /// </summary>
    static void CreateNewTask()
    {
        PrintLineColorWithUnderline("Create New Task", ConsoleColor.Yellow);

        string taskName = "";
        
        // Cannot 
        while (taskName.Length == 0)
        {
            taskName = GetUserInput("What is the task name?", ConsoleColor.Green);

            if (taskName.Length > 0) break;
            
            PrintLineColor("Task name required!", ConsoleColor.Red);
        }
        
        string taskDescription = GetUserInput("What is the task description?", ConsoleColor.Green);
        taskDescription = taskDescription.Length > 0 ? taskDescription : "No description.";
        
        int? taskPriority = GetUserNumberInput("What is the task priority?", ConsoleColor.Green);
        
        // Initialize new task
        TaskItem newTask = new TaskItem(taskName, taskDescription, taskPriority.HasValue ? taskPriority.Value : 0);
        
        _taskManager.AddTask(newTask);
    }

    static void ToggleTaskCompletion()
    {
        PrintLineColorWithUnderline("Toggle Task Completion", ConsoleColor.Yellow);

        if (_taskManager.Tasks.Count == 0)
        {
            PrintLineColor("No tasks found!", ConsoleColor.Red);
            Console.WriteLine();
            return;
        }

        TaskItem task = SelectTask();
        
        _taskManager.ToggleTaskCompletion(task);
    }
    
    /// <summary>
    /// Update a given task
    /// </summary>
    static void UpdateTask()
    {
        PrintLineColorWithUnderline("Update Task", ConsoleColor.Yellow);

        if (_taskManager.Tasks.Count == 0)
        {
            PrintLineColor("No tasks found!", ConsoleColor.Red);
            Console.WriteLine();
            return;
        }

        TaskItem task = SelectTask();
        
        string newName = GetUserInput("Give a new name (Blank to keep the same)", ConsoleColor.Green);
        string newDescription = GetUserInput("Give a new description (Blank to keep the same)", ConsoleColor.Green);
        int? newPriority = GetUserNumberInput("Give a new priority (Blank to keep the same)", ConsoleColor.Green);

        List<string> updateList = _taskManager.UpdateTask(task, newName, newDescription, newPriority);

        if (updateList.Count > 0)
        {
            foreach (var update in updateList)
            {
                PrintLineColor($"Updated {update}.", ConsoleColor.Yellow);
            }
        }
        else
        {
            PrintLineColor("Nothing updated", ConsoleColor.Yellow);
        }
        
        Console.WriteLine();
    }

    static void DeleteTask()
    {
        PrintLineColorWithUnderline("Delete Task", ConsoleColor.Yellow);

        if (_taskManager.Tasks.Count == 0)
        {
            PrintLineColor("No tasks found!", ConsoleColor.Red);
            Console.WriteLine();
            return;
        }
        
        TaskItem task = SelectTask();

        string userConfirmation = GetValidUserInput("Are you sure you want to delete this task? (Yes/No)",
            ConsoleColor.Green, new string[] {"Yes", "No" });

    if (userConfirmation == "Yes")
        {
            _taskManager.RemoveTask(task);
        }
        
        PrintLineColor(userConfirmation == "Yes" ? "Task deleted" : "Nothing deleted", ConsoleColor.Yellow);
        Console.WriteLine();
    }

    static TaskItem SelectTask()
    {
        ListAllTasks();
        
        string[] possibleSelections = Enumerable.Range(1, _taskManager.Tasks.Count).Select(n => n.ToString()).ToArray();
        
        int userSelection = int.Parse(GetValidUserInput("Which task?", ConsoleColor.Green, possibleSelections)) - 1;

        return _taskManager.Tasks[userSelection];
    }
    
    /// <summary>
    /// This function prints a line according to another ones length
    /// </summary>
    /// <param name="prompt">The prompt as to which the length should correspond.</param>
    /// <param name="lineColor">The color as to print the line in</param>
    static void PrintLineColorWithUnderline(string prompt, ConsoleColor lineColor)
    {
        PrintLineColor(prompt, lineColor);
        string line = "";

        foreach (var _ in prompt)
        {
            line += "-";
        }
        
        PrintLineColor(line, lineColor);
    }
    
    /// <summary>
    /// This function prints a full line in color and resets the console color afterward.
    /// </summary>
    /// <param name="text">The text which is to be printed</param>
    /// <param name="color">The color in which the text will be printed</param>
    static void PrintLineColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    /// <summary>
    /// This function prints something in-line, in color, and resets the console color afterward.
    /// </summary>
    /// <param name="text">The text which is to be printed</param>
    /// <param name="color">The color in which the text will be printed</param>
    static void PrintColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
    
    /// <summary>
    /// This function prints the main menu. It contains all the functionality the end-user has access to.
    /// </summary>
    static void PrintMenu()
    {
        PrintLineColorWithUnderline("TaskManagerCLI", ConsoleColor.Yellow);
        
        // Loop through menu options
        foreach (var option in MenuOptions)
        {
            PrintColor($"{option.Key.ToString()}. ", ConsoleColor.Cyan);
            PrintLineColor(option.Value,  ConsoleColor.Blue);
        }
    }

    static string GetUserInput(string prompt, ConsoleColor promptColor)
    {
        PrintColor($"{prompt} ",  promptColor);
        string userInput = Console.ReadLine()?.Trim() ?? "";

        return userInput;
    }

    static int? GetUserNumberInput(string prompt, ConsoleColor promptColor)
    {
        int result;

        while (true)
        {
            string input = GetUserInput(prompt, promptColor);
         
            if (string.IsNullOrWhiteSpace(input))
                return null;
            
            if (int.TryParse(input, out result)) break;
            
            PrintLineColor("Input was not a number", ConsoleColor.Red);
        }

        return result;
    }
    
    /// <summary>
    /// This function asks the user for an input, and keeps asking until a valid one is given.
    /// </summary>
    /// <param name="prompt">The prompt stated to the user</param>
    /// <param name="promptColor">The color as to print the prompt in</param>
    /// <param name="validInput">All inputs which are considered valid.</param>
    /// <returns></returns>
    static string GetValidUserInput(string prompt, ConsoleColor promptColor, string[] validInput)
    {
        string userInput;
        
        while (true)
        {
            userInput = GetUserInput(prompt, promptColor);

            if (validInput.Contains(userInput)) break;

            PrintLineColor("Invalid input", ConsoleColor.Red);
        }

        return userInput;
    }
}