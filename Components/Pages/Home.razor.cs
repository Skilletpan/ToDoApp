using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.Models.DTOs;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Components.Pages;

public partial class Home(ITodoService todoService)
{
    /// <summary>
    /// The Todo DTO to use in the editor.
    /// </summary>
    private TodoDTO EditorDTO { get; set; } = new();

    /// <summary>
    /// The list of Todo items in the database.
    /// </summary>
    private List<TodoModel> TodoItems = [];

    /// <summary>
    /// The string to filter Todo item names buy.
    /// </summary>
    private string SearchName = string.Empty;

    /// <summary>
    /// The list of Todo items after applying filters.
    /// </summary>
    private List<TodoModel> FilteredTodoItems => TodoItems.Where(todoItem =>
    {
        // Check against name search value
        if (!todoItem.Name.Contains(SearchName, StringComparison.CurrentCultureIgnoreCase)) return false;

        // Return remaining items
        return true;
    }).ToList();

    /// <summary>
    /// Saves the Todo item in the editor and resets the form.
    /// </summary>
    private async Task SubmitForm()
    {
        // Save Todo item and reset form
        await SaveTodo(EditorDTO);
        ResetForm();
    }

    /// <summary>
    /// Saves or updates a Todo item in the database.
    /// </summary>
    /// <param name="dto">The Todo DTO to save.</param>
    private async Task SaveTodo(TodoDTO dto)
    {
        // Save Todo item and refresh list
        await todoService.SaveTodo(dto);
        TodoItems = await todoService.FetchTodos();
    }

    /// <summary>
    /// Progresses a Todo item to the next status.
    /// </summary>
    /// <param name="todo">The Todo item to progress.</param>
    private async Task ProgressTodo(TodoModel todo)
    {
        // Catch Todo items with "Done" status
        if (todo.Status == TodoStatus.Done) throw new Exception("Cannot progress completed Todo item!");

        // Create Todo DTO with next status
        var dto = todo.ToDTO();
        dto.Status += 1;

        // Save Todo item
        await SaveTodo(dto);
    }

    /// <summary>
    /// Deletes a Todo item from the database.
    /// </summary>
    /// <param name="todo">The Todo item to delete.</param>
    private async Task DeleteTodo(TodoModel todo)
    {
        // Delete Todo item and refresh list
        await todoService.DeleteTodo(todo.ID);
        TodoItems = await todoService.FetchTodos();
    }

    /// <summary>
    /// Resets the form to its empty state.
    /// </summary>
    private void ResetForm() => EditorDTO = new();

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    protected override async Task OnInitializedAsync() => TodoItems = await todoService.FetchTodos();
}
