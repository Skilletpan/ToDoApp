using System.ComponentModel.DataAnnotations;

using ToDoApp.Enums;

namespace ToDoApp.Models.DTOs;

public class TodoDTO(TodoModel? source = null) : BaseDTO(source)
{
    /// <summary>
    /// The new display name for the Todo item.
    /// </summary>
    [Required(ErrorMessage = "Name is required!")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3-30 characters!")]
    public string Name { get; set; } = source?.Name ?? string.Empty;

    /// <summary>
    /// The new status for the Todo item.
    /// </summary>
    [Required(ErrorMessage = "Status is required!")]
    [Range(0, 2, ErrorMessage = "Status is out of range!")]
    public TodoStatus Status { get; set; } = source?.Status ?? TodoStatus.Open;
}
