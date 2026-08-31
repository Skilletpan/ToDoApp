namespace ToDoApp.Models;

public abstract class BaseModel(Guid id, DateTime created, DateTime? updated)
{
    /// <summary>
    /// The unique identifier of the model.
    /// </summary>
    public Guid ID { get; } = id;

    /// <summary>
    /// The date and time when the model was created.
    /// </summary>
    public DateTime Created { get; } = created;

    /// <summary>
    /// The date and time when the model was last updated.
    /// </summary>
    public DateTime? Updated { get; } = updated;
}
