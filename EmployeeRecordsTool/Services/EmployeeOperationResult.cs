using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Services;

public sealed class EmployeeOperationResult
{
    public bool IsSuccess { get; private init; }

    public IReadOnlyList<string> Errors { get; private init; } = [];

    public Employee? Employee { get; private init; }

    public static EmployeeOperationResult Success(Employee? employee = null) =>
        new() { IsSuccess = true, Employee = employee };

    public static EmployeeOperationResult Failure(params string[] errors) =>
        new() { IsSuccess = false, Errors = errors };
}
