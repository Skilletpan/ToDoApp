using ToDoApp.Models;
using ToDoApp.Models.DTOs;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services;

public class TodoService(IConfiguration configuration, ILogger<TodoService> logger) : BaseModelService<TodoModel>(configuration, logger), ITodoService
{
    public async Task<int> SaveTodo(TodoDTO dto)
    {
        // Validate DTO
        if (!IsValidDTO(dto)) return 0;

        // Build SQL statement and execute command
        var sql = dto.ID == null
            ? "INSERT INTO Todos (id, name, status, created) VALUES (UUID(), @Name, @Status, NOW());"
            : "UPDATE Todos SET name = @Name, status = @Status, updated = NOW() WHERE id = @ID;";

        return await Execute(sql, dto);
    }

    public async Task<int> DeleteTodo(Guid id)
    {
        // Build SQL statement and execute command
        const string sql = "DELETE FROM Todos WHERE id = @ID;";
        return await Execute(sql, new { ID = id });
    }

    public async Task<TodoModel?> FindTodoById(Guid id)
    {
        // Build SQL statement and run query
        const string sql = "SELECT * FROM Todos WHERE id = @ID;";
        return await QuerySingle(sql, new { ID = id });
    }

    public async Task<List<TodoModel>> FetchTodos()
    {
        // Build SQL statement and run query
        const string sql = "SELECT * FROM Todos ORDER BY created DESC;";
        return await Query(sql);
    }
}
