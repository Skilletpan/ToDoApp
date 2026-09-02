using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services;

public class TodoService(IDatabaseService databaseService) : BaseModelService<TodoModel>("Todos", databaseService), ITodoService
{
    public async Task<int> CreateTodo(string name, TodoStatus status = TodoStatus.Open)
    {
        // Set Todo values
        var values = new Dictionary<string, object>
        {
            { "name", name },
            { "status", status }
        };

        // Create Todo
        return await CreateModel(values);
    }

    public async Task<int> UpdateTodo(TodoModel todo, string? name, TodoStatus? status)
    {
        // Set Todo values to update
        var values = new Dictionary<string, object>();
        if (name != null) values.Add("name", name);
        if (status != null) values.Add("status", status);

        // Update Todo
        return await UpdateModel(todo.ID, values);
    }

    public async Task<int> DeleteTodo(TodoModel todo)
    {
        return 1;
    }

    public async Task<List<TodoModel>> FetchAllTodos()
    {
        return await FetchModels();
    }
}