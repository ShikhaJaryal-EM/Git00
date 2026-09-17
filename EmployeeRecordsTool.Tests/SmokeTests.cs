namespace EmployeeRecordsTool.Tests;

public class SmokeTests
{
    [Fact]
    public void TestProject_Builds_AndAssertionPasses()
    {
        Assert.True(true);
    }

    [Fact]
    public void WebProject_IsReferenced()
    {
        Assert.Equal("EmployeeRecordsTool.Pages", typeof(EmployeeRecordsTool.Pages.IndexModel).Namespace);
    }
}
