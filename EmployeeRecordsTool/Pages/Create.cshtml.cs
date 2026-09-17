using EmployeeRecordsTool.Models;
using EmployeeRecordsTool.Persistence;
using EmployeeRecordsTool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeRecordsTool.Pages;

public class CreateModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public CreateModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [BindProperty]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

    public IReadOnlyList<string> Errors { get; private set; } = [];

    public void OnGet()
    {
        Status = EmployeeStatus.Active;
    }

    public IActionResult OnPost()
    {
        try
        {
            var result = _employeeService.Create(FirstName, LastName, Status);
            if (!result.IsSuccess)
            {
                Errors = result.Errors;
                return Page();
            }

            return RedirectToPage("/Index");
        }
        catch (EmployeeStoreException ex)
        {
            Errors = [ex.Message];
            return Page();
        }
    }
}
