using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Persistence;

namespace EmployeeRecordsTool.Tests;

internal sealed class FakeEmployeeStore : IEmployeeStore
{
    public List<Employee> Items { get; } = [];

    public int SaveCount { get; private set; }

    public bool ThrowOnLoad { get; set; }

    public bool ThrowOnSave { get; set; }

    public string Path { get; } = "fake-employees.json";

    public IReadOnlyList<Employee> Load()
    {
        if (ThrowOnLoad)
        {
            throw new EmployeeStoreException($"{StoreErrorMessages.CorruptFile} {Path}", Path);
        }

        return Items.Select(Clone).ToList();
    }

    public void Save(IReadOnlyList<Employee> employees)
    {
        if (ThrowOnSave)
        {
            throw new EmployeeStoreException(StoreErrorMessages.SaveFailed, Path);
        }

        SaveCount++;
        Items.Clear();
        Items.AddRange(employees.Select(Clone));
    }

    private static Employee Clone(Employee employee) => new()
    {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Status = employee.Status
    };
}
