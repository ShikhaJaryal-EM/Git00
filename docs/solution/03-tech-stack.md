# Tech Stack

## Purpose

Name the v1 technology stack. The UI must stay **simple and functional**. The stack must satisfy local-only operation, local persistence, and a basic working interface. It must not introduce cloud, network services, login, or visual polish.

## Requirement filter (must pass)

| Requirement | Stack rule |
| --- | --- |
| NFR-1.1 Runs on a local machine | Desktop app on the user’s PC |
| NFR-1.2 No network connection | No HTTP client, no hosted URL, no CDN |
| NFR-1.3 No cloud or outside service | No Azure/AWS, no remote DB, no external API |
| NFR-2.1–NFR-2.3 Persist after close/reopen | Local file on disk |
| NFR-3.1 Basic working interface | List + two forms + labelled buttons |
| NFR-3.2 Create, edit, deactivate, reactivate in the UI | Those four actions are on-screen |
| NFR-3.3 Visual polish is not required | No theme, branding, or CSS framework |
| Scope: no security | No identity SDK |
| Scope: no extra fields / search / delete | No extra libraries for those features |

---

## Recommended stack (v1)

| Layer | Technology | Version | Role |
| --- | --- | --- | --- |
| Language | C# | 12 | Application and UI code |
| Runtime | .NET | 8 LTS | Local executable |
| **UI** | **Windows Forms (WinForms)** | .NET 8 | Simple list and forms |
| Logic | C# class library / project | — | Validation and employee operations |
| Persistence | JSON file + `System.Text.Json` | built-in | Save/load employee list |
| Data location | `%AppData%\EmployeeRecordsTool\employees.json` | — | Survives close and reopen |
| Tests | xUnit | latest stable | Service and file-store tests |
| IDE | Visual Studio 2022 or VS Code + .NET SDK | — | Build and run locally |
| OS | Windows 10 / 11 | — | v1 target machine |

This is one local process. There is no web server, no browser app, and no database server.

---

## Simple UI stack (what we will use)

v1 UI is **plain WinForms controls only**.

| Screen need | Control | Why it is enough |
| --- | --- | --- |
| Employee list | `ListView` (Details) | Shows first name, last name, status |
| Create / Edit | `Form` + `TextBox` | Enter names |
| Status on create | `ComboBox` (Active / Inactive) | Two values only |
| Actions | `Button` | Create, Save, Cancel, Edit, Deactivate, Reactivate |
| Rejection message | `Label` on the form | Tell the user; list stays unchanged |

**UI libraries we will not add**

| Not used | Why |
| --- | --- |
| WPF / MAUI / Electron | Extra UI complexity for a basic list |
| React, Angular, Vue, Blazor | Browser stack; polish and tooling not required |
| Bootstrap / Tailwind / Material | Visual polish is out of scope (NFR-3.3) |
| Chart / grid / search components | Search and reporting are out of scope |

The UI is successful when the user can complete create, edit, deactivate, and reactivate — not when it looks designed.

---

## Persistence stack

| Choice | Detail |
| --- | --- |
| Format | One `employees.json` file |
| Library | `System.Text.Json` (no extra NuGet for storage) |
| Write | After every accepted create, edit, deactivate, or reactivate |
| Read | On startup, before the list is shown |
| Network | None |

JSON is used instead of SQL Server, MariaDB, or cloud storage so persistence needs no outside service (NFR-2.2).

---

## Compatibility matrix

| Candidate | Local? | No network? | No cloud? | Simple UI? | Verdict |
| --- | --- | --- | --- | --- | --- |
| **.NET 8 + WinForms + JSON file** | Yes | Yes | Yes | Yes — native list and forms | **Selected** |
| Python + Tkinter + JSON file | Yes | Yes | Yes | Yes | Compatible alternative; not selected (weaker Windows packaging) |
| Node + Express + vanilla HTML + SQLite | Localhost only | Uses HTTP locally | Can be local | Simple HTML possible | Rejected — extra server, browser, and DB for no requirement gain |
| React SPA + REST API + hosted DB | No | No | No | No — extra UI layers | Rejected |
| Electron + React | Partial | Usually yes | Can be local | No — heavy UI stack | Rejected |
| Azure App Service / cloud DB | No | No | No | — | Rejected |

---

## Packages allowed in v1

| Package | Purpose |
| --- | --- |
| `System.Text.Json` | Built-in JSON read/write |
| `xunit` | Unit tests |
| `FluentAssertions` (optional) | Readable test asserts |

No other UI, HTTP, identity, or database packages.

## Packages and services forbidden in v1

- Cloud SDKs, REST clients, SignalR
- Entity Framework, SQL Server, MariaDB, MongoDB
- ASP.NET hosted APIs as the product surface
- Entra ID, OAuth, Identity, login libraries
- CSS/UI component frameworks

---

## How the user runs it

1. Install the .NET 8 SDK on the local Windows machine (or publish a self-contained `.exe` so the SDK is not required at run time).
2. Build and start the desktop app.
3. Use the list and forms. Data is written to `employees.json` on the same machine.

No internet, no account, no hosted environment.

## Planned project layout

```
EmployeeRecordsTool/
├── Program.cs
├── Forms/
│   ├── MainForm.cs
│   ├── CreateEmployeeForm.cs
│   └── EditEmployeeForm.cs
├── Models/
│   └── Employee.cs
├── Services/
│   ├── IEmployeeService.cs
│   └── EmployeeService.cs
├── Persistence/
│   ├── IEmployeeStore.cs
│   └── JsonFileStore.cs
└── EmployeeRecordsTool.csproj
```

---

## Decision

**DD-03:** v1 tech stack is **C# / .NET 8 / WinForms / local JSON**, with a **simple WinForms UI** (list, text boxes, buttons). This is the smallest stack that meets NFR-1, NFR-2, and NFR-3 as written.
