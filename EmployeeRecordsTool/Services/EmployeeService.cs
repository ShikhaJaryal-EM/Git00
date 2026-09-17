using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Persistence;

namespace EmployeeRecordsTool.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeStore _store;

    public EmployeeService(IEmployeeStore store)
    {
        _store = store;
    }

    public IReadOnlyList<Employee> List()
    {
        return _store.Load();
    }

    public EmployeeOperationResult Create(string? firstName, string? lastName, EmployeeStatus? status = null)
    {
        var validation = ValidateNames(firstName, lastName);
        if (validation is not null)
        {
            return validation;
        }

        var resolvedStatus = status ?? EmployeeStatus.Active;
        if (!IsAllowedStatus(resolvedStatus))
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.StatusInvalid);
        }

        var employees = _store.Load().ToList();
        var employee = new Employee
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = firstName!.Trim(),
            LastName = lastName!.Trim(),
            Status = resolvedStatus
        };
        employees.Add(employee);
        _store.Save(employees);
        return EmployeeOperationResult.Success(employee);
    }

    public EmployeeOperationResult UpdateNames(string id, string? firstName, string? lastName)
    {
        var validation = ValidateNames(firstName, lastName);
        if (validation is not null)
        {
            return validation;
        }

        var employees = _store.Load().ToList();
        var employee = FindById(employees, id);
        if (employee is null)
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.NotFound);
        }

        employee.FirstName = firstName!.Trim();
        employee.LastName = lastName!.Trim();
        _store.Save(employees);
        return EmployeeOperationResult.Success(employee);
    }

    public EmployeeOperationResult Deactivate(string id)
    {
        var employees = _store.Load().ToList();
        var employee = FindById(employees, id);
        if (employee is null)
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.NotFound);
        }

        if (employee.Status != EmployeeStatus.Active)
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.StatusInvalid);
        }

        employee.Status = EmployeeStatus.Inactive;
        _store.Save(employees);
        return EmployeeOperationResult.Success(employee);
    }

    public EmployeeOperationResult Reactivate(string id)
    {
        var employees = _store.Load().ToList();
        var employee = FindById(employees, id);
        if (employee is null)
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.NotFound);
        }

        if (employee.Status != EmployeeStatus.Inactive)
        {
            return EmployeeOperationResult.Failure(EmployeeErrorMessages.StatusInvalid);
        }

        employee.Status = EmployeeStatus.Active;
        _store.Save(employees);
        return EmployeeOperationResult.Success(employee);
    }

    private static EmployeeOperationResult? ValidateNames(string? firstName, string? lastName)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Trim().Length == 0)
        {
            errors.Add(EmployeeErrorMessages.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Trim().Length == 0)
        {
            errors.Add(EmployeeErrorMessages.LastNameRequired);
        }

        return errors.Count == 0 ? null : EmployeeOperationResult.Failure(errors.ToArray());
    }

    private static bool IsAllowedStatus(EmployeeStatus status) =>
        status is EmployeeStatus.Active or EmployeeStatus.Inactive;

    private static Employee? FindById(IEnumerable<Employee> employees, string id) =>
        employees.FirstOrDefault(employee => employee.Id == id);
}
