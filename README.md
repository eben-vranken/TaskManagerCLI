# Task Manager CLI

A simple command-line task manager built with C#. Manage your tasks with priorities, track completion status, and persist data to JSON.

## Features

- ✅ Create, update, and delete tasks
- 🎯 Set priority levels for tasks
- ✔️ Mark tasks as complete/incomplete
- 💾 Automatic JSON file persistence
- 🎨 Color-coded console interface

## Getting Started

### Prerequisites

- .NET 6.0 or higher

### Running the Application

```bash
dotnet run
```

## Usage

The application presents a menu with the following options:

1. **List all tasks** - View all your tasks with their details
2. **Create new task** - Add a new task with name, description, and priority
3. **Toggle task completion** - Mark tasks as complete or incomplete
4. **Update task** - Modify task details
5. **Delete task** - Remove a task (with confirmation)
0. **Exit** - Close the application

## Data Storage

Tasks are automatically saved to `tasks.json` in the application directory. The file is created on first run and updated after each modification.

## Example

```
TaskManagerCLI
--------------
1. List all tasks
2. Create new task
3. Toggle task completion
4. Update task
5. Delete task
0. Exit

What would you like to do? 2

Create New Task
---------------
What is the task name? Finish project documentation
What is the task description? Write README and add code comments
What is the task priority? 1
```

## License

MIT
