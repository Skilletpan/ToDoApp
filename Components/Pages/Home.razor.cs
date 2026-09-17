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
    private TodoFilters filters = new();

    /// <summary>
    /// The list of Todo items in the database.
    /// </summary>
    private List<TodoModel> TodoItems = [];

    /// <summary>
    /// The filtered Todo items.
    /// </summary>
    private IEnumerable<TodoModel> FilteredTodoItems => filters.Filter(TodoItems);

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
        public List<TodoStatus> Status = [];

        /// <summary>
        /// Adds or removes a Status from the filter statuses.
        /// </summary>
        /// <param name="status">The status to toggle.</param>
        public void ToggleStatus(TodoStatus status)
        {
            if (Status.Contains(status)) Status.Remove(status);
            else Status.Add(status);
        }

        /// <summary>
        /// Filters a given list of Todo items according to the set filters in this object.
        /// </summary>
        /// <param name="items">The items to filter.</param>
        /// <returns>The filtered items.</returns>
        public IEnumerable<TodoModel> Filter(IEnumerable<TodoModel> items)
        {
            var search = Search;
            var status = Status;

            return items.Where(item =>
            {
                // Filter by name
                if (!item.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)) return false;

                // Filter by status
                if (status.Count > 0 && !status.Contains(item.Status)) return false;

                // Return remaining
                return true;
            });
        }

        /// <summary>
        /// Resets the filters to their empty state.
        /// </summary>
        public void Reset()
        {
            Search = string.Empty;
            Status.Clear();
        }
    }
}
