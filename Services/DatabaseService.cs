using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services;

public class DatabaseService(IConfiguration config) : IDatabaseService
{
    /// <summary>
    /// The connection string pointing at the database to target commands at.
    /// </summary>
    private readonly string ConnectionString = config.GetConnectionString("DB_CONN") ?? throw new ConfigurationErrorsException("Database Connection String (\"DB_CONN\") is missing!");

    /// <summary>
    /// Runs an INSERT, UPDATE or DELETE command on the database.
    /// </summary>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>The amount of rows affected by the command.</returns>
    public async Task<int> Execute(string sql, object? dto = null)
    {
        // Open connection and execute command
        await using var connection = new MySqlConnection(ConnectionString);
        {
            return await connection.ExecuteAsync(sql, dto);
        }
    }

    /// <summary>
    /// Runs a SELECT command on the database.
    /// </summary>
    /// <typeparam name="T">The type of the queried object.</typeparam>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>The first found queried object or null.</returns>
    public async Task<T?> QuerySingle<T>(string sql, object? dto = null)
    {
        // Open connection and execute command
        await using var connection = new MySqlConnection(ConnectionString);
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, dto);
        }
    }

    /// <summary>
    /// Runs a SELECT command on the database.
    /// </summary>
    /// <typeparam name="T">The type of the queried objects.</typeparam>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>A list containing the queried objects.</returns>
    public async Task<List<T>> QueryList<T>(string sql, object? dto = null)
    {
        // Open connection and execute command
        await using var connection = new MySqlConnection(ConnectionString);
        {
            return (await connection.QueryAsync<T>(sql, dto)).ToList();
        }
    }
}
