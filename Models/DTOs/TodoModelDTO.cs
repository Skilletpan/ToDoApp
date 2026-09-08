using ToDoApp.Enums;

namespace ToDoApp.Models.DTOs;

public class TodoModelDTO(TodoModel? source = null) : BaseModelDTO(source)
{
    /// <summary>
    /// The new display name for the Todo item.
    /// </summary>
    public string? Name { get; set; } = source?.Name;

    /// <summary>
    /// The new status for the Todo item.
    /// </summary>
    public TodoStatus? Status { get; set; } = source?.Status ?? TodoStatus.Open;
}
