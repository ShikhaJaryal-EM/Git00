# Employee Records Tool

Local tool for keeping employee first name, last name, and status on one machine. v1 is HTML + Bootstrap 5 (files in the repo) on ASP.NET Core 8 Razor Pages. Data will be a local JSON file in later phases. There is no cloud host, no database, and no login.

## Run

1. Install the .NET 8 SDK.
2. From the repository root, start the host:

```
dotnet run --project EmployeeRecordsTool
```

3. Open `http://127.0.0.1:5080` in a local browser.

The process listens on loopback only (`127.0.0.1`). It does not bind `0.0.0.0` or a public URL.

## Build and test

```
dotnet build EmployeeRecordsTool.sln
dotnet test EmployeeRecordsTool.sln
```

## Layout

See `docs/solution/03-tech-stack.md` for the planned project shape. PHASE-001 provides the host, local Bootstrap, empty `Models/`, `Services/`, and `Persistence/` folders, and a placeholder Index page.
