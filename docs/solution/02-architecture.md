# Architecture

## Purpose

Define components, layers, communication, and data flows for create, edit, deactivate, reactivate, list, and persist.

## Requirement references

- FR-1.1–FR-5.2
- BR-1.1–BR-3.4
- NFR-1.1–NFR-4.3
- User journeys in `08-user-journeys.md`

---

## Design principles

| Principle | Meaning | Refs |
| --- | --- | --- |
| Local-only | UI, logic, and storage stay on the machine | NFR-1.1–NFR-1.3 |
| Validate before persist | Invalid names never reach the file | BR-3.1–BR-3.4, NFR-4.1 |
| Persist on success | Every accepted create, edit, deactivate, or reactivate is written immediately | NFR-2.1–NFR-2.3 |
| No hard delete | Records stay in the file; status changes only | Scope |
| Status via dedicated actions | Edit changes names only | FR-2.2, BR-2.3, BR-2.4 |
| Single user | No locking or concurrent-session logic | Assumptions |

## High-level architecture

```
┌──────────────────────────────────────────────────────────┐
│              Local machine (localhost only)              │
│                                                          │
│  ┌────────────────────────────────────────────────────┐  │
│  │  Browser — HTML + CSS + Bootstrap                  │  │
│  │  List page / Create page / Edit page               │  │
│  └───────────────────────┬────────────────────────────┘  │
│                          │ HTTP to 127.0.0.1             │
│  ┌───────────────────────▼────────────────────────────┐  │
│  │              ASP.NET Core (Kestrel)                │  │
│  │  ┌──────────────────────────────────────────────┐  │  │
│  │  │ UI layer — Razor Pages                       │  │  │
│  │  └──────────────────────┬───────────────────────┘  │  │
│  │  ┌──────────────────────▼───────────────────────┐  │  │
│  │  │ Application — EmployeeService                │  │  │
│  │  └──────────────────────┬───────────────────────┘  │  │
│  │  ┌──────────────────────▼───────────────────────┐  │  │
│  │  │ Persistence — JsonFileStore                  │  │  │
│  │  └──────────────────────┬───────────────────────┘  │  │
│  └─────────────────────────┼──────────────────────────┘  │
└────────────────────────────┼─────────────────────────────┘
                             │
                    ┌────────▼────────┐
                    │ employees.json  │
                    └─────────────────┘
```

**DD-01:** Three logical layers inside one local ASP.NET Core process. The browser is a view over localhost; there is no cloud or separate database server.

## Component overview

### UI layer

| Component | Responsibility | Refs |
| --- | --- | --- |
| Main list | `Index` Razor Page — HTML table of saved records | FR-5.1, FR-5.2 |
| Create action | Link/button to Create page | FR-1.1 |
| Edit action | Link to Edit page for that record | FR-2.1 |
| Deactivate action | POST from the list; shown only when status is active | FR-3.1, BR-2.3 |
| Reactivate action | POST from the list; shown only when status is inactive | FR-4.1, BR-2.4 |
| Create form | HTML form: first name, last name, status | FR-1.2, BR-2.2 |
| Edit form | HTML form: first name and last name only | FR-2.2 |
| Rejection message | Bootstrap alert / field error on the page | BR-3.3 |

No login screen, no settings screen, no navigation beyond list and forms (NFR-3.1).

### Application layer

| Operation | Responsibility | Refs |
| --- | --- | --- |
| `Create` | Trim names; require both; set status (default Active); persist | FR-1.1–FR-1.3, BR-1.1–BR-2.2, BR-3.1 |
| `UpdateNames` | Trim names; require both; do not change status; persist | FR-2.1–FR-2.3, BR-3.2 |
| `Deactivate` | Allowed only if active; set inactive; persist | FR-3.1–FR-3.2, BR-2.3 |
| `Reactivate` | Allowed only if inactive; set active; persist | FR-4.1–FR-4.2, BR-2.4 |
| `List` | Return all saved records, active and inactive | FR-5.1–FR-5.2 |
| Validation | Reject missing, blank, or whitespace-only names before any write | BR-1.4, BR-3.1–BR-3.4 |

There is no delete operation.

### Persistence layer

| Operation | Responsibility | Refs |
| --- | --- | --- |
| `Load` | Read the local JSON file when data is needed (list page or before a mutation) | NFR-2.1, NFR-2.3 |
| `Save` | Write the full list after an accepted mutation | NFR-2.1–NFR-2.2 |
| Path | `%AppData%\EmployeeRecordsTool\employees.json` (see `03-tech-stack.md`) | NFR-1.1 |
| Database | None — no SQL engine and no DB server | NFR-1.3, DD-02 |

Missing file on first run creates an empty list. A corrupt file is not overwritten (`08-error-handling.md`).

## Deployment

| Aspect | Design | Refs |
| --- | --- | --- |
| Delivery | Local `dotnet run` or self-contained publish; open `http://127.0.0.1` | NFR-1.1 |
| Data | Local JSON file on the same machine | NFR-1.2, NFR-1.3 |
| Network | Loopback only; no internet; no CDN | NFR-1.2, AC-6.1, AC-6.2 |
| Install | Run on the local machine; no outside service | NFR-1.3 |

## Data flows

### Open tool / list (journey start)

```
User starts the local app and opens the list page in the browser
  → Razor Page handler
    → JsonFileStore.Load()
      → EmployeeService.List()
        → HTML table shows first name, last name, status
```

### Create

```
User starts create → enters names and status → saves
  → EmployeeService.Create()
      → trim and validate
      → if invalid: tell user; do not save; list unchanged
      → if valid: append record; JsonFileStore.Save()
        → new row visible in the list
```

### Edit

```
User chooses a record → changes first and/or last name → saves
  → EmployeeService.UpdateNames()
      → trim and validate
      → if invalid: tell user; saved name unchanged
      → if valid: update names only; Save()
        → list shows updated names
```

### Deactivate / reactivate

```
User chooses an active record → Deactivate
  → EmployeeService.Deactivate() → status Inactive → Save()
    → record remains in the list as inactive

User chooses an inactive record → Reactivate
  → EmployeeService.Reactivate() → status Active → Save()
    → record remains in the list as active
```
