using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.Models.DTOs;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Components.Pages;

public partial class Home(ITodoService todoService)
{
    /// <summary>
    /// The list of Todo items in the database.
    /// </summary>
    List<TodoModel> todoItems = [];

    /// <summary>
    /// The Todo DTO to use in the editor.
    /// </summary>
    private TodoDTO EditorDTO { get; set; } = new();

    /// <summary>
    /// Saves the Todo item in the editor and resets the form.
    /// </summary>
    async Task SubmitForm()
    {
        // Save Todo item and reset form
        await SaveTodo(EditorDTO);
        ResetForm();
    }

    /// <summary>
    /// Resets the form to its empty state.
    /// </summary>
    void ResetForm() => EditorDTO = new();

    /// <summary>
    /// Saves or updates a Todo item in the database.
    /// </summary>
    /// <param name="dto">The Todo DTO to save.</param>
    async Task SaveTodo(TodoDTO dto)
    {
        // Save Todo item and refresh list
        await todoService.SaveTodo(dto);
        todoItems = await todoService.FetchTodos();
    }

    /// <summary>
    /// Progresses a Todo item to the next status.
    /// </summary>
    /// <param name="todo">The Todo item to progress.</param>
    async Task ProgressTodo(TodoModel todo)
    {
        // Catch Todo items with "Done" status
        if (todo.Status == TodoStatus.Done) throw new Exception("Cannot progress completed Todo item!");

        // Create Todo DTO with next status
        var dto = todo.CreateDTO();
        dto.Status += 1;

        // Save Todo item
        await SaveTodo(dto);
    }

    /// <summary>
    /// Deletes a Todo item from the database.
    /// </summary>
    /// <param name="todo">The Todo item to delete.</param>
    async Task DeleteTodo(TodoModel todo)
    {
        // Delete Todo item and refresh list
        await todoService.DeleteTodo(todo.ID);
        todoItems = await todoService.FetchTodos();
    }

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    protected override async Task OnInitializedAsync() => todoItems = await todoService.FetchTodos();
}
