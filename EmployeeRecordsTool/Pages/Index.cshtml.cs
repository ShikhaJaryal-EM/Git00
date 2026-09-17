using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Persistence;
using EmployeeRecordsTool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeRecordsTool.Pages;

public class IndexModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public IndexModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IReadOnlyList<Employee> Employees { get; private set; } = [];

    public string? ErrorMessage { get; private set; }

    public void OnGet()
    {
        LoadEmployees();
    }

    public IActionResult OnPostDeactivate(string id)
    {
        try
        {
            _employeeService.Deactivate(id);
        }
        catch (EmployeeStoreException ex)
        {
            ErrorMessage = ex.Message;
            LoadEmployees();
            return Page();
        }

        return RedirectToPage("/Index");
    }

    public IActionResult OnPostReactivate(string id)
    {
        try
        {
            _employeeService.Reactivate(id);
        }
        catch (EmployeeStoreException ex)
        {
            ErrorMessage = ex.Message;
            LoadEmployees();
            return Page();
        }

        return RedirectToPage("/Index");
    }

    private void LoadEmployees()
    {
        try
        {
            Employees = _employeeService.List();
        }
        catch (EmployeeStoreException ex)
        {
            ErrorMessage = ex.Message;
            Employees = [];
        }
    }
}
