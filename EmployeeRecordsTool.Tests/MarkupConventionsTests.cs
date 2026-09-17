namespace EmployeeRecordsTool.Tests;

public class MarkupConventionsTests
{
    private static readonly string PagesRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "EmployeeRecordsTool", "Pages"));

    [Fact]
    public void Index_HasNameStatusColumns_CreateLink_AndNoDeleteOrIdColumn()
    {
        var markup = File.ReadAllText(Path.Combine(PagesRoot, "Index.cshtml"));

        Assert.Contains("First name", markup);
        Assert.Contains("Last name", markup);
        Assert.Contains("Status", markup);
        Assert.Contains("class=\"table\"", markup);
        Assert.Contains("Create employee", markup);
        Assert.Contains("Deactivate", markup);
        Assert.Contains("Reactivate", markup);
        Assert.DoesNotContain("Toggle", markup);
        Assert.DoesNotContain("Delete", markup, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Remove", markup, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(">Id<", markup);
        Assert.DoesNotContain("cdn.", markup);
        Assert.DoesNotContain("unpkg", markup);
        Assert.DoesNotContain("jsdelivr", markup);
    }

    [Fact]
    public void Create_HasFirstNameLastNameAndStatus()
    {
        var markup = File.ReadAllText(Path.Combine(PagesRoot, "Create.cshtml"));

        Assert.Contains("First name", markup);
        Assert.Contains("Last name", markup);
        Assert.Contains("Status", markup);
        Assert.Contains("form-select", markup);
        Assert.Contains("Cancel", markup);
        Assert.Contains("Save", markup);
    }

    [Fact]
    public void Edit_HasNamesOnly_NoStatusControl()
    {
        var markup = File.ReadAllText(Path.Combine(PagesRoot, "Edit.cshtml"));

        Assert.Contains("First name", markup);
        Assert.Contains("Last name", markup);
        Assert.DoesNotContain("form-select", markup);
        Assert.DoesNotContain("asp-for=\"Status\"", markup);
        Assert.DoesNotContain("Deactivate", markup);
        Assert.DoesNotContain("Delete", markup, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Layout_UsesLocalBootstrap_NotCdn()
    {
        var markup = File.ReadAllText(Path.Combine(PagesRoot, "Shared", "_Layout.cshtml"));

        Assert.Contains("/lib/bootstrap/", markup);
        Assert.DoesNotContain("cdn.", markup);
        Assert.DoesNotContain("unpkg", markup);
        Assert.DoesNotContain("jsdelivr", markup);
    }

    [Fact]
    public void IndexHandlers_HaveNoDeleteMethod()
    {
        var names = typeof(EmployeeRecordsTool.Pages.IndexModel)
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
            .Select(method => method.Name);

        Assert.DoesNotContain(names, name => name.Contains("Delete", StringComparison.OrdinalIgnoreCase));
        Assert.Contains("OnPostDeactivate", names);
        Assert.Contains("OnPostReactivate", names);
    }
}
