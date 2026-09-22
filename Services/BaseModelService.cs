using Dapper;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

using ToDoApp.Models;
using ToDoApp.Models.DTOs;

namespace ToDoApp.Services;

public abstract class BaseModelService<T>(IConfiguration configuration, ILogger logger) where T : BaseModel
{
    /// <summary>
    /// The connection object of the database to read and write data to and from.
    /// </summary>
    private readonly MySqlConnection DbConnection = new(
        configuration.GetConnectionString("DB_CONN")
        ?? throw new ConfigurationErrorsException("Connection string DB_CONN is missing!")
    );

    /// <summary>
    /// Runs an <c>INSERT</c>, <c>UPDATE</c> or <c>DELETE</c> command on the database.
    /// </summary>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="parameters">The parameters to inject into the SQL command.</param>
    /// <returns>The amount of rows that were affected by the command.</returns>
    protected async Task<int> Execute(string sql, object? parameters = null)
    {
        await using (DbConnection)
        {
            logger.LogDebug("Query: {query} Parameters: {parameters}", sql, parameters?.ToString());

            // Open database connection and execute command
            var rows = await DbConnection.ExecuteAsync(sql, parameters);
            logger.LogDebug("{rows} rows updated", rows);

            return rows;
        }
    }

    /// <summary>
    /// Runs a <c>SELECT</c> command on the database and returns the first result.
    /// </summary>
    /// <param name="sql">The SQL query to run.</param>
    /// <param name="parameters">The parameters to inject into the SQL query.</param>
    /// <returns>The first item matching the query or <c>null</c>.</returns>
    protected async Task<T?> QuerySingle(string sql, object? parameters = null)
    {
        await using (DbConnection)
        {
            logger.LogDebug("Query: {query} Parameters: {parameters}", sql, parameters?.ToString());

            // Execute and return item or null
            var itemOrNull = await DbConnection.QueryFirstAsync<T>(sql, parameters);
            logger.LogDebug("{amount} items found.", itemOrNull == null ? 0 : 1);

            return itemOrNull;
        }
    }

    /// <summary>
    /// Runs a <c>SELECT</c> command on the database.
    /// </summary>
    /// <param name="sql">The SQL query to run.</param>
    /// <param name="parameters">The parameters to inject into the SQL query.</param>
    /// <returns>The list of items matching the query.</returns>
    protected async Task<IEnumerable<T>> Query(string sql, object? parameters = null)
    {
        await using (DbConnection)
        {
            logger.LogDebug("Query: {query} Parameters: {parameters}", sql, parameters?.ToString());

            // Execute query and return items
            var items = await DbConnection.QueryAsync<T>(sql, parameters);
            logger.LogDebug("{amount} items found.", items.Count());

            return items;
        }
    }

    /// <summary>
    /// Validates a given DTO using Data Annotations.
    /// </summary>
    /// <param name="dto">The DTO to validate.</param>
    /// <returns>Whether the DTO is valid.</returns>
    protected bool IsValidDTO(BaseDTO dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResults = new List<ValidationResult>();

        // Validate DTO using data annotations
        Validator.TryValidateObject(dto, validationContext, validationResults, validateAllProperties: true);

        // Log all errors
        foreach (var result in validationResults) logger.LogError(result.ErrorMessage);

        // Return true if no errors
        return validationResults.Count == 0;
    }
}
