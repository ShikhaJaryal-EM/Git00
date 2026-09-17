using System.Text.Json;
using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Persistence;

namespace EmployeeRecordsTool.Tests;

public class JsonFileStoreTests : IDisposable
{
    private readonly string _directory;
    private readonly string _filePath;

    public JsonFileStoreTests()
    {
        _directory = Path.Combine(Path.GetTempPath(), "EmployeeRecordsToolTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_directory);
        _filePath = Path.Combine(_directory, "employees.json");
    }

    [Fact]
    public void Load_MissingFile_ReturnsEmptyList_AndCreatesEmptyEmployeesArray()
    {
        var store = new JsonFileStore(_filePath);

        var loaded = store.Load();

        Assert.Empty(loaded);
        Assert.True(File.Exists(_filePath));
        using var document = JsonDocument.Parse(File.ReadAllText(_filePath));
        Assert.True(document.RootElement.TryGetProperty("employees", out var employees));
        Assert.Equal(JsonValueKind.Array, employees.ValueKind);
        Assert.Equal(0, employees.GetArrayLength());
    }

    [Fact]
    public void SaveThenLoad_RoundTrip_RestoresIdNamesAndStatus()
    {
        var store = new JsonFileStore(_filePath);
        var original = new List<Employee>
        {
            new()
            {
                Id = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                FirstName = "Jane",
                LastName = "Smith",
                Status = EmployeeStatus.Active
            },
            new()
            {
                Id = "b2c3d4e5-f6a7-8901-bcde-f12345678901",
                FirstName = "Jane",
                LastName = "Smith",
                Status = EmployeeStatus.Inactive
            }
        };

        store.Save(original);
        var loaded = store.Load();

        Assert.Equal(2, loaded.Count);
        Assert.Equal(original[0].Id, loaded[0].Id);
        Assert.Equal(original[0].FirstName, loaded[0].FirstName);
        Assert.Equal(original[0].LastName, loaded[0].LastName);
        Assert.Equal(original[0].Status, loaded[0].Status);
        Assert.Equal(original[1].Id, loaded[1].Id);
        Assert.Equal(original[1].FirstName, loaded[1].FirstName);
        Assert.Equal(original[1].LastName, loaded[1].LastName);
        Assert.Equal(original[1].Status, loaded[1].Status);

        var json = File.ReadAllText(_filePath);
        Assert.Contains("\"employees\"", json, StringComparison.Ordinal);
        Assert.Contains("\"id\"", json, StringComparison.Ordinal);
        Assert.Contains("\"firstName\"", json, StringComparison.Ordinal);
        Assert.Contains("\"lastName\"", json, StringComparison.Ordinal);
        Assert.Contains("\"status\"", json, StringComparison.Ordinal);
        Assert.Contains("\"Active\"", json, StringComparison.Ordinal);
        Assert.Contains("\"Inactive\"", json, StringComparison.Ordinal);
        Assert.DoesNotContain("email", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("phone", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Load_CorruptFile_LeavesBytesUnchanged_AndDoesNotSucceedAsEmptyList()
    {
        var originalBytes = """{ "this": "is-not-valid-employee-data" """u8.ToArray();
        File.WriteAllBytes(_filePath, originalBytes);

        var store = new JsonFileStore(_filePath);

        var exception = Assert.Throws<EmployeeStoreException>(() => store.Load());

        Assert.Contains(StoreErrorMessages.CorruptFile, exception.Message, StringComparison.Ordinal);
        Assert.Contains(_filePath, exception.Message, StringComparison.Ordinal);
        Assert.Equal(originalBytes, File.ReadAllBytes(_filePath));
        Assert.NotEmpty(exception.Message);
    }

    [Fact]
    public void Save_WhenWriteFails_DoesNotReportSuccess()
    {
        Directory.CreateDirectory(_filePath);
        var store = new JsonFileStore(_filePath);

        var exception = Assert.Throws<EmployeeStoreException>(() =>
            store.Save(
            [
                new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Status = EmployeeStatus.Active
                }
            ]));

        Assert.Equal(StoreErrorMessages.SaveFailed, exception.Message);
    }

    [Fact]
    public void GetDefaultFilePath_IsUnderAppDataEmployeeRecordsTool()
    {
        var path = JsonFileStore.GetDefaultFilePath();
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        Assert.StartsWith(appData, path, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("EmployeeRecordsTool", path, StringComparison.Ordinal);
        Assert.EndsWith("employees.json", path, StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, recursive: true);
        }
    }
}
