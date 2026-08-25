namespace ToDoApp.Models;

public class TodoItem(Guid id, string name, bool isDone, DateTime created, DateTime? updated)
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
    /// Whether the TodoItem is marked as done.
    /// </summary>
    public bool IsDone { get; private set; } = isDone;

    /// <summary>
    /// When the TodoItem was created.
    /// </summary>
    public DateTime Created { get; private set; } = created;

    /// <summary>
    /// When the TodoItem was last updated.
    /// </summary>
    public DateTime? Updated { get; private set; } = updated;
}
