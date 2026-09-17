using System.Text.Json;
using System.Text.Json.Serialization;
using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Persistence;

public sealed class JsonFileStore : IEmployeeStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly string _filePath;

    public JsonFileStore()
        : this(GetDefaultFilePath())
    {
    }

    public JsonFileStore(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    public static string GetDefaultFilePath()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "EmployeeRecordsTool");
        return Path.Combine(folder, "employees.json");
    }

    public IReadOnlyList<Employee> Load()
    {
        EnsureDirectory();

        if (!File.Exists(_filePath))
        {
            WriteDocument(new EmployeesFileDocument());
            return [];
        }

        string json;
        try
        {
            json = File.ReadAllText(_filePath);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw CreateCorruptException(ex);
        }

        EmployeesFileDocument? document;
        try
        {
            document = JsonSerializer.Deserialize<EmployeesFileDocument>(json, JsonOptions);
        }
        catch (JsonException ex)
        {
            throw CreateCorruptException(ex);
        }

        if (document?.Employees is null)
        {
            throw CreateCorruptException();
        }

        return document.Employees.ToList();
    }

    public void Save(IReadOnlyList<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        EnsureDirectory();

        var document = new EmployeesFileDocument
        {
            Employees = employees.ToList()
        };

        try
        {
            WriteDocument(document);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new EmployeeStoreException(
                StoreErrorMessages.SaveFailed,
                _filePath,
                ex);
        }
    }

    private void EnsureDirectory()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (string.IsNullOrEmpty(directory))
        {
            return;
        }

        Directory.CreateDirectory(directory);
    }

    private void WriteDocument(EmployeesFileDocument document)
    {
        var json = JsonSerializer.Serialize(document, JsonOptions);
        var tempPath = _filePath + ".tmp";
        File.WriteAllText(tempPath, json);
        File.Move(tempPath, _filePath, overwrite: true);
    }

    private EmployeeStoreException CreateCorruptException(Exception? inner = null)
    {
        var message = $"{StoreErrorMessages.CorruptFile} {_filePath}";
        return new EmployeeStoreException(message, _filePath, inner);
    }
}
