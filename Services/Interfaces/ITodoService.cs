using ToDoApp.Enums;
using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces;

public interface ITodoService
{
    /// <summary>
    /// Adds a new Todo item to the database.
    /// </summary>
    /// <param name="name">The display name of the new Todo item.</param>
    /// <param name="status">The initial status of the new Todo item.</param>
    /// <returns>The amount of database rows affected. Should return <c>1</c> if the operation was successful.</returns>
    public Task<int> CreateTodo(string name, TodoStatus status = TodoStatus.Open);

    /// <summary>
    /// Updates a Todo item in the database.
    /// </summary>
    /// <param name="todo">The Todo item to update.</param>
    /// <param name="newName">The new display name to set for the Todo item.</param>
    /// <param name="newStatus">The new status to set for the Todo item.</param>
    /// <returns>The amount of database rows affected. Should return <c>1</c> if the operation was successful.</returns>
    public Task<int> UpdateTodo(TodoModel todo, string? newName = null, TodoStatus? newStatus = null);

    /// <summary>
    /// Deletes a Todo item from the database.
    /// </summary>
    /// <param name="todo">The Todo item to delete.</param>
    /// <returns>The amount of database rows affected. Should return <c>1</c> if the operation was successful.</returns>
    public Task<int> DeleteTodo(TodoModel todo);

    /// <summary>
    /// Fetches all Todo items from the database.
    /// </summary>
    /// <returns>A list of all Todo items.</returns>
    public Task<List<TodoModel>> FetchAllTodos();
}
