using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services;

public class TodoService(IDatabaseService databaseService) : ITodoService
{
    public async Task<int> CreateTodo(string name, TodoStatus status = TodoStatus.Open)
    {
        // Build SQL statement and execute insert
        var sql = "INSERT INTO Todos (id, created, name, status) VALUES (UUID(), NOW(), @name, @status);";
        return await databaseService.Execute(sql, new { name, status });
    }

    public async Task<int> UpdateTodo(TodoModel todo, string? newName, TodoStatus? newStatus)
    {
        // Set existing values as fallbacks (prevents setting columns to null)
        newName ??= todo.Name;
        newStatus ??= todo.Status;

        // Build SQL statement and execute update
        var sql = "UPDATE Todos SET updated = NOW(), name = @name, status = @status WHERE id = @id;";
        return await databaseService.Execute(sql, new { id = todo.ID, name = newName, status = newStatus });
    }

    public async Task<int> DeleteTodo(TodoModel todo)
    {
        // Build SQL statement and execute delete
        var sql = "DELETE FROM Todos WHERE id = @id;";
        return await databaseService.Execute(sql, new { id = todo.ID });
    }

    public async Task<List<TodoModel>> FetchAllTodos()
    {
        // Build SQL statement and query items
        var sql = "SELECT * FROM Todos ORDER BY created DESC;";
        return await databaseService.QueryList<TodoModel>(sql);
    }
}