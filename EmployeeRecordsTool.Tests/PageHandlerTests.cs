using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Pages;
using EmployeeRecordsTool.Persistence;
using EmployeeRecordsTool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeRecordsTool.Tests;

public class PageHandlerTests
{
    [Fact]
    public void IndexOnGet_PassesListResultsToPageModel()
    {
        var store = new FakeEmployeeStore();
        store.Items.Add(new Employee
        {
            Id = "1",
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Inactive
        });
        var page = new IndexModel(new EmployeeService(store));

        page.OnGet();

        Assert.Single(page.Employees);
        Assert.Equal("Jane", page.Employees[0].FirstName);
        Assert.Equal("Smith", page.Employees[0].LastName);
        Assert.Equal(EmployeeStatus.Inactive, page.Employees[0].Status);
        Assert.Null(page.ErrorMessage);
    }

    [Fact]
    public void IndexOnGet_CorruptStore_ShowsErr04_AndEmptyList()
    {
        var store = new FakeEmployeeStore { ThrowOnLoad = true };
        var page = new IndexModel(new EmployeeService(store));

        page.OnGet();

        Assert.Empty(page.Employees);
        Assert.Contains(StoreErrorMessages.CorruptFile, page.ErrorMessage);
    }

    [Fact]
    public void CreateOnPost_InvalidNames_StayOnPage_AndDoNotSave()
    {
        var store = new FakeEmployeeStore();
        var page = new CreateModel(new EmployeeService(store))
        {
            FirstName = " ",
            LastName = "",
            Status = EmployeeStatus.Active
        };

        var result = page.OnPost();

        Assert.IsType<PageResult>(result);
        Assert.Contains(EmployeeErrorMessages.FirstNameRequired, page.Errors);
        Assert.Equal(0, store.SaveCount);
        Assert.Empty(store.Items);
    }

    [Fact]
    public void CreateOnPost_Valid_RedirectsToIndex_AndPersists()
    {
        var store = new FakeEmployeeStore();
        var page = new CreateModel(new EmployeeService(store))
        {
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Inactive
        };

        var result = page.OnPost();

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/Index", redirect.PageName);
        Assert.Single(store.Items);
        Assert.Equal(EmployeeStatus.Inactive, store.Items[0].Status);
    }

    [Fact]
    public void CreateOnPost_SaveFailure_ShowsErr05_AndDoesNotReportSuccess()
    {
        var store = new FakeEmployeeStore { ThrowOnSave = true };
        var page = new CreateModel(new EmployeeService(store))
        {
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Active
        };

        var result = page.OnPost();

        Assert.IsType<PageResult>(result);
        Assert.Contains(StoreErrorMessages.SaveFailed, page.Errors);
    }

    [Fact]
    public void EditOnPost_InvalidName_DoesNotChangeStoredName()
    {
        var store = new FakeEmployeeStore();
        store.Items.Add(new Employee
        {
            Id = "1",
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Active
        });
        var page = new EditModel(new EmployeeService(store))
        {
            Id = "1",
            FirstName = "",
            LastName = "Smythe"
        };

        var result = page.OnPost();

        Assert.IsType<PageResult>(result);
        Assert.Equal("Jane", store.Items[0].FirstName);
        Assert.Equal("Smith", store.Items[0].LastName);
        Assert.Equal(EmployeeStatus.Active, store.Items[0].Status);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public void IndexOnPostDeactivate_ChangesStatus_AndKeepsRow()
    {
        var store = new FakeEmployeeStore();
        store.Items.Add(new Employee
        {
            Id = "1",
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Active
        });
        var page = new IndexModel(new EmployeeService(store));

        var result = page.OnPostDeactivate("1");

        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal(EmployeeStatus.Inactive, store.Items[0].Status);
        Assert.Single(store.Items);
    }

    [Fact]
    public void IndexOnPostReactivate_ChangesStatus_AndKeepsRow()
    {
        var store = new FakeEmployeeStore();
        store.Items.Add(new Employee
        {
            Id = "1",
            FirstName = "Jane",
            LastName = "Smith",
            Status = EmployeeStatus.Inactive
        });
        var page = new IndexModel(new EmployeeService(store));

        var result = page.OnPostReactivate("1");

        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal(EmployeeStatus.Active, store.Items[0].Status);
        Assert.Single(store.Items);
    }
}
