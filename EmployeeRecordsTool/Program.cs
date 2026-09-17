using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Local-only host: loopback HTTP, not 0.0.0.0 or a public URL (NFR-1.1–NFR-1.3, DD-14).
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Parse("127.0.0.1"), 5080);
});

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
