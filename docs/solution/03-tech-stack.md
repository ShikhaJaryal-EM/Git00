# Tech Stack

## Purpose

Name the v1 technology stack. The UI is **simple HTML, CSS, and Bootstrap**. The backend is the option that best fits those pages **and** the local-only requirements (no cloud, no internet, no login).

## Requirement filter (must pass)

| Requirement | Stack rule |
| --- | --- |
| NFR-1.1 Runs on a local machine | App and data stay on the user’s PC |
| NFR-1.2 No network connection | Bind to `localhost` only; no CDN; no outbound calls |
| NFR-1.3 No cloud or outside service | No Azure/AWS, no remote DB, no external API |
| NFR-2.1–NFR-2.3 Persist after close/reopen | Local JSON file on disk |
| NFR-3.1 Basic working interface | List page + create page + edit page |
| NFR-3.2 Create, edit, deactivate, reactivate in the UI | Those four actions are on the pages |
| NFR-3.3 Visual polish is not required | Bootstrap for layout only; no custom theme or branding |
| Scope: no security | No identity SDK |
| Scope: no extra fields / search / delete | No extra UI libraries for those features |

---

## Selected stack (v1)

| Layer | Technology | Version | Role |
| --- | --- | --- | --- |
| **UI** | **HTML + CSS + Bootstrap** | Bootstrap 5 (local files) | Simple list and forms |
| Pages | ASP.NET Core Razor Pages | .NET 8 | Server-rendered HTML; form POST |
| Backend | **ASP.NET Core** (Kestrel) | 8 LTS | Local web host on `http://127.0.0.1` |
| Language | C# | 12 | Pages, validation, employee operations |
| Logic | `EmployeeService` | — | Create, edit names, deactivate, reactivate, list |
| Persistence | JSON file + `System.Text.Json` | built-in | Save/load employee list |
| Data location | `%AppData%\EmployeeRecordsTool\employees.json` | — | Survives close and reopen |
| Tests | xUnit | latest stable | Service and file-store tests |
| Browser | Any local browser | — | Open localhost; no internet needed |
| OS | Windows 10 / 11 | — | v1 target machine |

There is **no cloud host**, **no public URL**, and **no database server**. The browser talks only to the local ASP.NET Core process.

---

## Why this backend (ASP.NET Core 8)

The UI choice is HTML/CSS/Bootstrap, so something must **serve those pages** and **apply business rules** before writing the file. ASP.NET Core 8 is the most compatible backend because:

| Need | How ASP.NET Core 8 meets it |
| --- | --- |
| Simple HTML forms | Razor Pages return HTML; Bootstrap styles the markup |
| Same machine, no cloud | Kestrel runs locally; bind to `127.0.0.1` only |
| Works offline (NFR-1.2) | Bootstrap and CSS are copied into the project; no CDN |
| Keep existing design | `EmployeeService` + JSON file stay as designed |
| No login | No authentication middleware |
| Tests | xUnit can test the service without a browser |
| Packaging | `dotnet run` locally, or a self-contained publish |

**Razor Pages** (not a JavaScript SPA) keeps the UI as HTML forms. Create, edit, deactivate, and reactivate are ordinary POST requests. No React, Vue, or extra API layer is required.

### Backends considered

| Backend | Fit with HTML/Bootstrap | Fit with local-only NFRs | Verdict |
| --- | --- | --- | --- |
| **ASP.NET Core 8 + Razor Pages + JSON** | Native HTML views | Localhost only; no cloud; JSON on disk | **Selected** |
| Node.js + Express + JSON | Good | Localhost possible; extra runtime vs existing C# design | Not selected |
| Python Flask + JSON | Good | Localhost possible; second language | Not selected |
| WinForms (previous) | Not HTML/Bootstrap | Strong local-only | Replaced by this decision |
| ASP.NET Core + SQL Server / cloud DB | Good | Conflicts with NFR-1.3 | Rejected |
| Hosted React + REST API | Not simple HTML | Network and often cloud | Rejected |

---

## Simple UI stack

v1 pages are **HTML + Bootstrap 5**, with a small amount of CSS only if Bootstrap utilities are not enough.

| Screen need | HTML / Bootstrap | Why it is enough |
| --- | --- | --- |
| Employee list | `table.table` | First name, last name, status |
| Create / Edit | `<form>` + `form-control` | Enter names |
| Status on create | `<select class="form-select">` | Active or Inactive |
| Actions | `btn btn-primary` / `btn btn-secondary` | Create, Save, Cancel, Edit, Deactivate, Reactivate |
| Rejection message | `alert alert-danger` or field `text-danger` | Tell the user; list unchanged |

**Bootstrap is local, not from a CDN.** Files live under `wwwroot/lib/bootstrap/` so the tool still works with the network off (NFR-1.2, AC-6.1).

**UI libraries we will not add**

| Not used | Why |
| --- | --- |
| React, Angular, Vue, Blazor WASM | SPA complexity; not simple HTML |
| Tailwind / Material / custom design system | Extra polish beyond Bootstrap layout |
| Chart / grid / search plugins | Search and reporting are out of scope |

NFR-3.3 still applies: Bootstrap is for a readable layout, not branding or visual polish.

---

## How local-only is preserved

| Risk | Rule |
| --- | --- |
| Accidental internet use | No CDN, no telemetry, no external APIs |
| Accidental LAN sharing | Listen on `127.0.0.1` only (not `0.0.0.0`) |
| Accidental cloud | No Azure, no connection strings to remote stores |
| Accidental database server | JSON file only |

Loopback HTTP (`http://127.0.0.1`) is how the browser loads local HTML. It is **not** an outside network service (NFR-1.3).

---

## Persistence stack

There is **no database**. SQL Server, SQLite, MariaDB, and similar stores are not used.

| Choice | Detail |
| --- | --- |
| Format | One `employees.json` file |
| Library | `System.Text.Json` |
| Write | After every accepted create, edit, deactivate, or reactivate |
| Read | When the list page (or a mutation) needs data |
| Network | None |

---

## Packages allowed in v1

| Package | Purpose |
| --- | --- |
| ASP.NET Core (shared framework) | Host and Razor Pages |
| `System.Text.Json` | JSON read/write |
| Bootstrap 5 (static files in repo) | Simple layout |
| `xunit` | Unit tests |
| `FluentAssertions` (optional) | Readable test asserts |

## Packages and services forbidden in v1

- Cloud SDKs, REST clients to remote systems, SignalR
- Entity Framework, SQL Server, MariaDB, MongoDB
- Entra ID, OAuth, Identity, login libraries
- Bootstrap or other assets loaded from a CDN
- SPA frameworks

---

## How the user runs it

1. Install the .NET 8 SDK on the local Windows machine.
2. Run the app (`dotnet run`). It listens on `http://127.0.0.1:<port>` only.
3. Open that address in a local browser.
4. Use the list and forms. Data is written to `employees.json` on the same machine.

No internet, no account, no hosted environment, no database. Stop the process and start it again to reopen; the JSON file still has the list (NFR-2.1, NFR-4.3).

## Planned project layout

```
EmployeeRecordsTool/
├── Program.cs
├── Pages/
│   ├── Index.cshtml              # employee list
│   ├── Index.cshtml.cs
│   ├── Create.cshtml
│   ├── Create.cshtml.cs
│   ├── Edit.cshtml
│   └── Edit.cshtml.cs
├── Models/
│   └── Employee.cs
├── Services/
│   ├── IEmployeeService.cs
│   └── EmployeeService.cs
├── Persistence/
│   ├── IEmployeeStore.cs
│   └── JsonFileStore.cs
├── wwwroot/
│   ├── css/
│   │   └── site.css              # optional, minimal
│   └── lib/
│       └── bootstrap/            # local Bootstrap 5
└── EmployeeRecordsTool.csproj
```

Deactivate and reactivate are POST handlers from the list page (no extra pages).

---

## Decision

**DD-03:** v1 tech stack is **HTML + CSS + Bootstrap 5 (local)** for the UI, **ASP.NET Core 8 Razor Pages** as the backend, and a **local JSON file** for persistence. ASP.NET Core is the backend that best matches this UI and the local-only requirements.
