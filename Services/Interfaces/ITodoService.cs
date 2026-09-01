using ToDoApp.Enums;
using ToDoApp.Models;

namespace ToDoApp.Services.Interfaces;

public interface ITodoService
{
    public Task<int> CreateTodo(string name, TodoStatus status = TodoStatus.Open);

    public Task<int> UpdateTodo(Guid id, string? name = null, TodoStatus? status = null);

    public Task<List<TodoModel>> FetchAllTodos();
}
