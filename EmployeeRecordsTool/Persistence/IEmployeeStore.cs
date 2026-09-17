using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Persistence;

public interface IEmployeeStore
{
    IReadOnlyList<Employee> Load();

    void Save(IReadOnlyList<Employee> employees);
}
