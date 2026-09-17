namespace EmployeeRecordsTool.Services;

/// <summary>
/// User-facing validation messages from docs/solution/08-error-handling.md.
/// </summary>
public static class EmployeeErrorMessages
{
    public const string FirstNameRequired = "First name is required.";
    public const string LastNameRequired = "Last name is required.";
    public const string StatusInvalid = "Status must be Active or Inactive.";
    public const string NotFound = "Employee record not found.";
}
