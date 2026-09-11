using ToDoApp.Models;
using ToDoApp.Models.DTOs;

namespace ToDoApp.Services.Interfaces;

public interface ITodoService
{
    /// <summary>
    /// Creates a new Todo item in the database. If the <c>dto</c> has an ID, the database will run an <c>UPDATE</c> command, otherwise it will run an <c>INSERT</c> command.
    /// </summary>
    /// <param name="dto">A DTO holding the values to set for the Todo item.</param>
    /// <returns>The amount of rows affected by the operation. Should return <c>1</c> if the operation succeeded.</returns>
    public Task<int> SaveTodo(TodoDTO dto);

    /// <summary>
    /// Deletes a Todo item from the database.
    /// </summary>
    /// <param name="id">The unique ID of the Todo item to delete.</param>
    /// <returns>The amount of rows affected by the operation. Should return <c>1</c> if the operation succeeded.</returns>
    public Task<int> DeleteTodo(Guid id);

    /// <summary>
    /// Looks for a Todo item with the given ID in the database.
    /// </summary>
    /// <param name="id">The unique ID of the Todo item to look up.</param>
    /// <returns>The found Todo item or <c>null</c> if not found.</returns>
    public Task<TodoModel?> FindTodoById(Guid id);

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    /// <returns>A list of all Todo items.</returns>
    public Task<List<TodoModel>> FetchTodos();
}
