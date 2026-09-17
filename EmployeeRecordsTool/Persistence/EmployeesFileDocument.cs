using System.Text.Json.Serialization;
using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Persistence;

/// <summary>
/// On-disk JSON wrapper: { "employees": [ ... ] } from docs/solution/04-data-design.md.
/// </summary>
internal sealed class EmployeesFileDocument
{
    [JsonPropertyName("employees")]
    [JsonRequired]
    public List<Employee> Employees { get; set; } = [];
}
