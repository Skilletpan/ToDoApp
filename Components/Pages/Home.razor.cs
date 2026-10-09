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
    /// The filters to filter Todo items by.
    /// </summary>
    private TodoFilters Filters = new();

    /// <summary>
    /// The sortable column headers and value references.
    /// </summary>
    private readonly Dictionary<string, Func<TodoModel, object>> ColumnHeaders = new()
    {
        { "Name", todo => todo.Name },
        { "Status", todo => todo.Status },
        { "Created", todo => todo.Created },
        { "Changed", todo => todo.Changed }
    };

    /// <summary>
    /// The list of Todo items in the database.
    /// </summary>
    private IEnumerable<TodoModel> TodoItems = [];

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

    private struct TodoFilters
    {
        public TodoFilters() { }

        /// <summary>
        /// The string to filter Todo item names buy.
        /// </summary>
        public string Search = string.Empty;

        /// <summary>
        /// The list of statuses to filter Todo items by.
        /// </summary>
        public IEnumerable<TodoStatus> Status = [TodoStatus.Open, TodoStatus.InProgress];

        /// <summary>
        /// Checks a Todo item to see if it passes the filters.
        /// </summary>
        /// <param name="item">The Todo item to check.</param>
        /// <returns></returns>
        public readonly bool CheckItem(TodoModel item)
        {
            // Filter by Name
            if (!item.Name.Contains(Search, StringComparison.InvariantCultureIgnoreCase)) return false;

            // Filter by Status
            if (Status.Any() && !Status.Contains(item.Status)) return false;

            // Filter passed
            return true;
        }

        /// <summary>
        /// Resets the filters to their empty state.
        /// </summary>
        public void ResetFilters()
        {
            Search = string.Empty;
            Status = [TodoStatus.Open, TodoStatus.InProgress];
        }
    }
}
