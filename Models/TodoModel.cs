using ToDoApp.Enums;

namespace ToDoApp.Models;

/// <summary>
/// An item in a ToDo list.
/// </summary>
/// <param name="id">The unique identifier of the Todo item.</param>
/// <param name="name">The display name of the Todo item.</param>
/// <param name="status">The status of the Todo item.</param>
/// <param name="created">The date and time when the Todo item was created.</param>
/// <param name="updated">The date and time when the Todo item was last updated.</param>
public class TodoModel(Guid id, string name, TodoStatus status, DateTime created, DateTime? updated = null) : BaseModel(id, created, updated)
{
    /// <summary>
    /// The display name of the Todo item.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The status of the Todo item.
    /// </summary>
    public TodoStatus Status { get; } = status;
}
