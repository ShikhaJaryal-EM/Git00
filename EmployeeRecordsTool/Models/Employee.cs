namespace EmployeeRecordsTool.Models;

/// <summary>
/// v1 employee record. Id is a hidden system identifier (DD-04).
/// Only first name, last name, and status are user-facing fields.
/// </summary>
public sealed class Employee
{
    public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public EmployeeStatus Status { get; set; }
}
