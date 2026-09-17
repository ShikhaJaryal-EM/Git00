using EmployeeRecordsTool.Models;

namespace EmployeeRecordsTool.Tests;

public class ModelShapeTests
{
    [Fact]
    public void Employee_HasOnlyIdNamesAndStatus()
    {
        var names = typeof(Employee).GetProperties().Select(property => property.Name).OrderBy(name => name).ToArray();

        Assert.Equal(["FirstName", "Id", "LastName", "Status"], names);
    }
}
