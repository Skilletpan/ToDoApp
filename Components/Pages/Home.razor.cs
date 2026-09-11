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
    /// Resets the form to its empty state.
    /// </summary>
    void ResetForm() => EditorDTO = new();

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    protected override async Task OnInitializedAsync() => todoItems = await todoService.FetchTodos();
}
