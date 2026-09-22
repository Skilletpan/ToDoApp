namespace ToDoApp.Models.DTOs;

public abstract class BaseDTO(BaseModel? source = null)
{
    /// <summary>
    /// The unique identifier of the Model item.
    /// </summary>
    public Guid? ID { get; } = source?.ID;

    public override string ToString()
    {
        var output = GetType().FullName + ": ";
        output += string.Join(", ", GetType().GetProperties().Select(p => $"{p.Name}={p.GetValue(this)}"));

        return output;
    }
}
