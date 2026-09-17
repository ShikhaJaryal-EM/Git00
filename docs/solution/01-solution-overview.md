# Solution Overview

## Purpose

Describe the overall solution: what is being built, who it serves, which objectives it satisfies, and how a local design delivers the required capabilities.

## Requirement references

- `01-executive-summary-and-context.md`
- `02-stakeholders-and-personas.md`
- `03-business-objectives.md` (BO-1, BO-2, BO-3)
- `04-scope.md`
- FR-1.1–FR-5.2
- NFR-1.1–NFR-4.3

---

## Problem

There is no dedicated local place to keep employee names and status. Teams cannot reliably add people, correct names, mark people inactive, or reuse the same list after the tool is closed.

## Solution summary

v1 is a **local tool** used by one person on one machine. A local ASP.NET Core process serves **HTML, CSS, and Bootstrap** pages in the browser. Employee records are stored in a **local JSON file** — not a database. When the user opens the list page, records load from that file. The user can create records, edit names, deactivate, and reactivate. Each successful change is written to the file immediately. After the app is stopped and started again, the same list is still there.

```
┌───────────────────────────────────────────────┐
│                 Local machine                 │
│                                               │
│  ┌─────────────┐    ┌─────────────────────┐  │
│  │  Browser    │───►│  ASP.NET Core       │  │
│  │  HTML/CSS/  │◄───│  Razor Pages +      │  │
│  │  Bootstrap  │    │  EmployeeService    │  │
│  └─────────────┘    └──────────┬──────────┘  │
│                                │              │
│                       ┌────────▼────────┐     │
│                       │ employees.json  │     │
│                       │ (local file)    │     │
│                       └─────────────────┘     │
└───────────────────────────────────────────────┘

Localhost only. No cloud. No external service. Bootstrap files are local (no CDN).
```

**DD-01:** UI pages, business rules, and file persistence run in one local ASP.NET Core process. The browser only talks to `127.0.0.1`. That matches local-only operation (NFR-1.1–NFR-1.3).

## Business objectives mapping

| ID | Objective | How the solution satisfies it |
| --- | --- | --- |
| BO-1 | Capture first name, last name, and status | Create form and list columns; stored on each record |
| BO-2 | Create, edit, deactivate, and reactivate locally | Main list plus create/edit forms and status actions (FR-1.1–FR-4.2) |
| BO-3 | Keep the list after close and reopen | Write the local JSON file on every successful change; load it after process restart (NFR-2.1–NFR-2.3) |

## Users

| Attribute | Design |
| --- | --- |
| Who | Employee list maintainer (the only v1 persona) |
| What they do | Create, edit names, deactivate, reactivate, and see the list persist |
| Access | Full access to all v1 actions; no login |
| Environment | One person, one local machine |

Viewer and administrator personas are not in v1. There is no view-only mode and no separate admin mode.

## In scope

| Capability | Delivery | Refs |
| --- | --- | --- |
| Create employee | Create form: first name, last name, status | FR-1.1–FR-1.3 |
| Edit names | Edit form: first name and last name only | FR-2.1–FR-2.3 |
| Deactivate | Action available only for an active employee | FR-3.1–FR-3.2, BR-2.3 |
| Reactivate | Action available only for an inactive employee | FR-4.1–FR-4.2, BR-2.4 |
| List | Show first name, last name, and status | FR-5.1–FR-5.2 |
| Persistence | Local JSON file load when the list is shown; save on successful change | NFR-2.1–NFR-2.3 |
| Basic UI | Working screens for the journeys above | NFR-3.1–NFR-3.2 |

## Out of scope

The design must not introduce:

| Item | Refs |
| --- | --- |
| Extra employee fields | Scope |
| Login, roles, permissions | Scope, stakeholders |
| Cloud, network, or outside services | NFR-1.2, NFR-1.3 |
| Multi-user sharing | Assumptions |
| Search, filters, export, reporting | Scope |
| Permanent delete | Scope |
| Visual polish | NFR-3.3 |

## Success

The design is successful when:

1. FR-1.1 through FR-5.2 can be completed in the UI.
2. BR-1.1 through BR-3.4 are enforced in application logic before persist.
3. NFR-1.1 through NFR-4.3 are met (local, persistent, basic UI, rejected save does not corrupt the list).
4. AC-1.1 through AC-6.3 can be demonstrated.
5. No out-of-scope capability is added.
