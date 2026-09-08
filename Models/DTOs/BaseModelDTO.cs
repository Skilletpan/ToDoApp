namespace ToDoApp.Models.DTOs;

public abstract class BaseModelDTO(BaseModel? source = null)
{
    /// <summary>
    /// The unique identifier of the Model item.
    /// </summary>
    public Guid? ID { get; } = source?.ID;
}
