using ToDoApp.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services;

public abstract class BaseModelService<T>(string table, IDatabaseService databaseService) where T : BaseModel
{
    private readonly string ModelTable = table;

    protected async Task<int> CreateModel(Dictionary<string, object>? setValues = null)
    {
        // Set default values
        var defaultValues = new Dictionary<string, string>
        {
            { "id", "UUID()" },
            { "created", "NOW()" }
        };

        // Build full model values and parameters
        var (values, parameters) = BuildValuesAndParameters(defaultValues, setValues);

        // Build SQL statement
        var sql = @$"
            INSERT INTO {ModelTable}
            ({string.Join(", ", values.Keys)})
            VALUES ({string.Join(", ", values.Values)});
        ";
        Console.WriteLine(sql);

        // Execute command
        return await databaseService.Execute(sql, parameters);
    }

    protected async Task<int> UpdateModel(Guid id, Dictionary<string, object> setValues)
    {
        // Set default values
        var defaultValues = new Dictionary<string, string>
        {
            { "updated", "NOW()" }
        };

        // Set default parameters
        var defaultParameters = new Dictionary<string, object>
        {
            { "id", id }
        };

        // Build full model values and parameters
        var (values, parameters) = BuildValuesAndParameters(defaultValues, setValues, defaultParameters);

        // Build SQL statement
        var sql = @$"
            UPDATE {ModelTable}
            SET {string.Join(", ", values.Select(v => $"{v.Key} = {v.Value}"))}
            WHERE id = @id;
        ";
        Console.WriteLine(sql);

        // Execute command
        return await databaseService.Execute(sql, parameters);
    }

    protected async Task<T?> FetchModel(Dictionary<string, object>? whereValues = null, string? order = "created DESC")
    {
        // Build full model values and parameters
        var (values, parameters) = BuildValuesAndParameters(modelValues: whereValues);

        // Build SQL statement
        var sql = "";
        if (values.Count == 0)
        {
            sql = @$"
                SELECT * FROM {ModelTable}
                ORDER BY {order};
            ";
        }
        else
        {
            sql = @$"
                SELECT * FROM {ModelTable}
                WHERE {string.Join(", ", values.Select(v => $"{v.Key} = {v.Value}"))}
                ORDER BY {order};
            ";
        }
        Console.WriteLine(sql);

        return await databaseService.QuerySingle<T>(sql, parameters);
    }

    protected async Task<List<T>> FetchModels(Dictionary<string, object>? whereValues = null, string? order = "created DESC")
    {
        // Build full model values and parameters
        var (values, parameters) = BuildValuesAndParameters(modelValues: whereValues);

        // Build SQL statement
        var sql = "";
        if (values.Count == 0)
        {
            sql = @$"
                SELECT * FROM {ModelTable}
                ORDER BY {order};
            ";
        }
        else
        {
            sql = @$"
                SELECT * FROM {ModelTable}
                WHERE {string.Join(", ", values.Select(v => $"{v.Key} = {v.Value}"))}
                ORDER BY {order};
            ";
        }
        Console.WriteLine(sql);

        return await databaseService.QueryList<T>(sql, parameters);
    }

    private static (Dictionary<string, string>, Dictionary<string, object>) BuildValuesAndParameters(
        Dictionary<string, string>? defaultValues = null,
        Dictionary<string, object>? modelValues = null,
        Dictionary<string, object>? defaultParameters = null
    )
    {
        // Create dictionaries with default values
        Dictionary<string, string> values = defaultValues?.ToDictionary() ?? [];
        Dictionary<string, object> parameters = defaultParameters?.ToDictionary() ?? [];

        // Add modelValues as parameters
        if (modelValues != null) foreach (var modelValue in modelValues)
        {
            values.Add(modelValue.Key, $"@{modelValue.Key}");
            parameters.Add(modelValue.Key, modelValue.Value);
        }

        // Return values and parameters
        return (values, parameters);
    }
}
