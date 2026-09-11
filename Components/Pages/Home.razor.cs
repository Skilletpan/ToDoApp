using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Components.Pages;

public partial class Home(ITodoService todoService)
{
    /// <summary>
    /// The list of Todo items in the database.
    /// </summary>
    List<TodoModel> todoItems = [];

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    protected override async Task OnInitializedAsync() => todoItems = await todoService.FetchTodos();
}
