using ToDoApp.Enums;

namespace ToDoApp.Models;

public class TodoItem(Guid id, string name, TodoStatus status, DateTime created, DateTime? updated)
{
    /// <summary>
    /// The unique identifier of the TodoItem.
    /// </summary>
    public Guid ID { get; private set; } = id;

    /// <summary>
    /// The display name of the TodoItem.
    /// </summary>
    public string Name { get; private set; } = name;

    /// <summary>
    /// The status of the TodoItem.
    /// </summary>
    public TodoStatus Status { get; private set; } = status;

    /// <summary>
    /// When the TodoItem was created.
    /// </summary>
    public DateTime Created { get; private set; } = created;

    /// <summary>
    /// When the TodoItem was last updated.
    /// </summary>
    public DateTime? Updated { get; private set; } = updated;
}
