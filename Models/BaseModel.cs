namespace ToDoApp.Models;

/// <summary>
/// The basic scaffolding of a Model item.
/// </summary>
/// <param name="id">The unique identifier of the Model item.</param>
/// <param name="created">The date and time when the Model item was created.</param>
/// <param name="updated">The date and time when the Model item was last updated.</param>
public abstract class BaseModel(Guid id, DateTime created, DateTime? updated = null)
{
    /// <summary>
    /// The unique identifier of the Model item.
    /// </summary>
    public Guid ID { get; } = id;

    /// <summary>
    /// The date and time when the Model item was created.
    /// </summary>
    public DateTime Created { get; } = created;

    /// <summary>
    /// The date and time when the Model item was last updated.
    /// </summary>
    public DateTime? Updated { get; } = updated;
}
