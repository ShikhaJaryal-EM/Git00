using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Services;

namespace EmployeeRecordsTool.Tests;

public class EmployeeServiceTests
{
    private readonly FakeEmployeeStore _store = new();
    private readonly EmployeeService _service;

    public EmployeeServiceTests()
    {
        _service = new EmployeeService(_store);
    }

    [Fact]
    public void Create_WithValidNamesAndStatus_AddsRecord()
    {
        var result = _service.Create("Jane", "Smith", EmployeeStatus.Inactive);

        Assert.True(result.IsSuccess);
        Assert.Equal("Jane", result.Employee!.FirstName);
        Assert.Equal("Smith", result.Employee.LastName);
        Assert.Equal(EmployeeStatus.Inactive, result.Employee.Status);
        Assert.False(string.IsNullOrWhiteSpace(result.Employee.Id));
        Assert.Equal(1, _store.SaveCount);
        Assert.Single(_store.Items);
    }

    [Fact]
    public void Create_AppearsInList()
    {
        _service.Create("Jane", "Smith", EmployeeStatus.Active);

        var list = _service.List();

        Assert.Single(list);
        Assert.Equal("Jane", list[0].FirstName);
        Assert.Equal("Smith", list[0].LastName);
        Assert.Equal(EmployeeStatus.Active, list[0].Status);
    }

    [Fact]
    public void Create_MissingFirstName_IsRejected()
    {
        var result = _service.Create("", "Smith", EmployeeStatus.Active);

        Assert.False(result.IsSuccess);
        Assert.Contains(EmployeeErrorMessages.FirstNameRequired, result.Errors);
        Assert.Equal(0, _store.SaveCount);
        Assert.Empty(_store.Items);
    }

    [Fact]
    public void Create_MissingLastName_IsRejected()
    {
        var result = _service.Create("Jane", "  ", EmployeeStatus.Active);

        Assert.False(result.IsSuccess);
        Assert.Contains(EmployeeErrorMessages.LastNameRequired, result.Errors);
        Assert.Equal(0, _store.SaveCount);
        Assert.Empty(_service.List());
    }

    [Fact]
    public void Create_WhitespaceOnlyName_IsRejected_AndDoesNotAddRecord()
    {
        var result = _service.Create("   ", "\t", EmployeeStatus.Active);

        Assert.False(result.IsSuccess);
        Assert.Contains(EmployeeErrorMessages.FirstNameRequired, result.Errors);
        Assert.Contains(EmployeeErrorMessages.LastNameRequired, result.Errors);
        Assert.Equal(0, _store.SaveCount);
        Assert.Empty(_service.List());
    }

    [Fact]
    public void Create_StoresTrimmedNames()
    {
        var result = _service.Create("  Jane  ", "  Smith  ", EmployeeStatus.Active);

        Assert.True(result.IsSuccess);
        Assert.Equal("Jane", result.Employee!.FirstName);
        Assert.Equal("Smith", result.Employee.LastName);
    }

    [Fact]
    public void Create_WithoutChosenStatus_DefaultsToActive()
    {
        var result = _service.Create("Jane", "Smith");

        Assert.True(result.IsSuccess);
        Assert.Equal(EmployeeStatus.Active, result.Employee!.Status);
    }

    [Fact]
    public void List_ReturnsFirstNameLastNameStatus_IncludingInactive()
    {
        _service.Create("Jane", "Smith", EmployeeStatus.Active);
        _service.Create("John", "Doe", EmployeeStatus.Inactive);

        var list = _service.List();

        Assert.Equal(2, list.Count);
        Assert.Equal("Jane", list[0].FirstName);
        Assert.Equal("Smith", list[0].LastName);
        Assert.Equal(EmployeeStatus.Active, list[0].Status);
        Assert.Equal("John", list[1].FirstName);
        Assert.Equal("Doe", list[1].LastName);
        Assert.Equal(EmployeeStatus.Inactive, list[1].Status);
    }

    [Fact]
    public void Create_DuplicateNames_AreAllowed()
    {
        _service.Create("Jane", "Smith", EmployeeStatus.Active);
        var second = _service.Create("Jane", "Smith", EmployeeStatus.Active);

        Assert.True(second.IsSuccess);
        Assert.Equal(2, _service.List().Count);
        Assert.NotEqual(_service.List()[0].Id, _service.List()[1].Id);
    }

    [Fact]
    public void UpdateNames_ChangesNamesOnly()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Active).Employee!;
        var savesBeforeEdit = _store.SaveCount;

        var result = _service.UpdateNames(created.Id, "Janet", "Smythe");

        Assert.True(result.IsSuccess);
        var listed = _service.List().Single();
        Assert.Equal("Janet", listed.FirstName);
        Assert.Equal("Smythe", listed.LastName);
        Assert.Equal(EmployeeStatus.Active, listed.Status);
        Assert.Equal(created.Id, listed.Id);
        Assert.Equal(savesBeforeEdit + 1, _store.SaveCount);
    }

    [Fact]
    public void UpdateNames_BlankName_IsRejected_StoredNameUnchanged()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Active).Employee!;
        var savesAfterCreate = _store.SaveCount;

        var result = _service.UpdateNames(created.Id, "", "Smythe");

        Assert.False(result.IsSuccess);
        Assert.Contains(EmployeeErrorMessages.FirstNameRequired, result.Errors);
        Assert.Equal(savesAfterCreate, _store.SaveCount);
        var listed = _service.List().Single();
        Assert.Equal("Jane", listed.FirstName);
        Assert.Equal("Smith", listed.LastName);
    }

    [Fact]
    public void UpdateNames_UnknownId_IsRejected()
    {
        var result = _service.UpdateNames("missing", "Jane", "Smith");

        Assert.False(result.IsSuccess);
        Assert.Contains(EmployeeErrorMessages.NotFound, result.Errors);
        Assert.Equal(0, _store.SaveCount);
    }

    [Fact]
    public void Deactivate_SetsInactive_AndKeepsRecord()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Active).Employee!;

        var result = _service.Deactivate(created.Id);

        Assert.True(result.IsSuccess);
        var listed = _service.List().Single();
        Assert.Equal(EmployeeStatus.Inactive, listed.Status);
        Assert.Equal(created.Id, listed.Id);
        Assert.Equal("Jane", listed.FirstName);
    }

    [Fact]
    public void Deactivate_WhenInactive_IsRejected()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Inactive).Employee!;
        var savesAfterCreate = _store.SaveCount;

        var result = _service.Deactivate(created.Id);

        Assert.False(result.IsSuccess);
        Assert.Equal(savesAfterCreate, _store.SaveCount);
        Assert.Equal(EmployeeStatus.Inactive, _service.List().Single().Status);
    }

    [Fact]
    public void Reactivate_SetsActive_AndKeepsRecord()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Inactive).Employee!;

        var result = _service.Reactivate(created.Id);

        Assert.True(result.IsSuccess);
        var listed = _service.List().Single();
        Assert.Equal(EmployeeStatus.Active, listed.Status);
        Assert.Equal(created.Id, listed.Id);
    }

    [Fact]
    public void Reactivate_WhenActive_IsRejected()
    {
        var created = _service.Create("Jane", "Smith", EmployeeStatus.Active).Employee!;
        var savesAfterCreate = _store.SaveCount;

        var result = _service.Reactivate(created.Id);

        Assert.False(result.IsSuccess);
        Assert.Equal(savesAfterCreate, _store.SaveCount);
        Assert.Equal(EmployeeStatus.Active, _service.List().Single().Status);
    }

    [Fact]
    public void EmployeeService_HasNoDeleteOrRemoveMethod()
    {
        var names = typeof(EmployeeService)
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
            .Select(method => method.Name)
            .ToList();

        Assert.DoesNotContain(names, name => name.Contains("Delete", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(names, name => name.Contains("Remove", StringComparison.OrdinalIgnoreCase));
    }
}
