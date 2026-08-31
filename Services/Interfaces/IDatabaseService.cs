namespace ToDoApp.Services.Interfaces;

public interface IDatabaseService
{
    /// <summary>
    /// Runs an ExecuteAsync command on the database.
    /// </summary>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>The amount of rows affected by the command.</returns>
    public Task<int> Execute(string sql, object? dto = null);

    /// <summary>
    /// Runs a QueryFirstOrDefaultAsync command on the database.
    /// </summary>
    /// <typeparam name="T">The type of the queried object.</typeparam>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>The queried object or null.</returns>
    public Task<T?> QuerySingle<T>(string sql, object? dto = null);

    /// <summary>
    /// Runs a QueryAsync command on the database.
    /// </summary>
    /// <typeparam name="T">The type of the queried objects.</typeparam>
    /// <param name="sql">The SQL command to run.</param>
    /// <param name="dto">An object holding the parameters to inject into the SQL command.</param>
    /// <returns>A list containing the queried objects.</returns>
    public Task<List<T>> QueryList<T>(string sql, object? dto = null);
}
