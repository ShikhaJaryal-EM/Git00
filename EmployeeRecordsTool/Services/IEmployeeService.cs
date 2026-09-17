using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Services;

public interface IEmployeeService
{
    IReadOnlyList<Employee> List();

    EmployeeOperationResult Create(string? firstName, string? lastName, EmployeeStatus? status = null);

    EmployeeOperationResult UpdateNames(string id, string? firstName, string? lastName);

    EmployeeOperationResult Deactivate(string id);

    EmployeeOperationResult Reactivate(string id);
}
