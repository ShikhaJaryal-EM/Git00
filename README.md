# Employee Records Tool

Local tool for keeping employee first name, last name, and status on one machine. v1 is HTML + Bootstrap 5 (files in the repo) on ASP.NET Core 8 Razor Pages. Data is a local JSON file. There is no cloud host, no database, and no login.

## Run

1. Install the .NET 8 SDK.
2. From the repository root, start the host:

```
dotnet run --project EmployeeRecordsTool
```

3. Open `http://127.0.0.1:5080` in a local browser.

The process listens on loopback only (`127.0.0.1`). It does not bind `0.0.0.0` or a public URL.

Saved records are written to `%AppData%\EmployeeRecordsTool\employees.json` after each accepted create, name edit, deactivate, or reactivate.

## Build and test

```
dotnet build EmployeeRecordsTool.sln
dotnet test EmployeeRecordsTool.sln
```

## Layout

See `docs/solution/03-tech-stack.md`. The host, JSON store, employee service, and list/create/edit pages live in `EmployeeRecordsTool/`.
