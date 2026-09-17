namespace EmployeeRecordsTool.Persistence;

/// <summary>
/// File load or save failure. Messages match the error catalogue (ERR-04, ERR-05).
/// </summary>
public sealed class EmployeeStoreException : Exception
{
    public string FilePath { get; }

    public EmployeeStoreException(string message, string filePath, Exception? innerException = null)
        : base(message, innerException)
    {
        FilePath = filePath;
    }
}
