using ToDoApp.Enums;

namespace ToDoApp.Models;

public class TodoModel(Guid id, string name, TodoStatus status, DateTime created, DateTime? updated) : BaseModel(id, created, updated)
{
    /// <summary>
    /// The display name of the TodoItem.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The status of the TodoItem.
    /// </summary>
    public TodoStatus Status { get; } = status;
}
