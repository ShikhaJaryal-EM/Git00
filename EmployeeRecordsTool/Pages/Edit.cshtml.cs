using EmployeeRecordsTool.Persistence;
using EmployeeRecordsTool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeRecordsTool.Pages;

public class EditModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public EditModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [BindProperty]
    public string Id { get; set; } = string.Empty;

    [BindProperty]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    public string LastName { get; set; } = string.Empty;

    public IReadOnlyList<string> Errors { get; private set; } = [];

    public IActionResult OnGet(string id)
    {
        try
        {
            var employee = _employeeService.List().FirstOrDefault(item => item.Id == id);
            if (employee is null)
            {
                Errors = [EmployeeErrorMessages.NotFound];
                return Page();
            }

            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            return Page();
        }
        catch (EmployeeStoreException ex)
        {
            Errors = [ex.Message];
            return Page();
        }
    }

    public IActionResult OnPost()
    {
        try
        {
            var result = _employeeService.UpdateNames(Id, FirstName, LastName);
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
