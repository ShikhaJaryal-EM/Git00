# Assumptions, Constraints & Out of Scope

**Version:** 1.0  
**Status:** Approved

## Assumptions

| ID | Assumption |
|----|------------|
| **A-1** | A single user operates the application on a single machine |
| **A-2** | Only one instance of the application runs at a time |
| **A-3** | The user has a modern web browser available |
| **A-4** | Employee volume is small (tens to low hundreds of records) |
| **A-5** | Email addresses are stable enough to serve as unique identifiers |
| **A-6** | The local machine has writable filesystem access for data persistence |
| **A-7** | No network connectivity is required to use the application |

## Constraints

| ID | Constraint |
|----|------------|
| **C-1** | Must run locally — no cloud hosting or external services |
| **C-2** | Must provide a browser-based UI |
| **C-3** | Must persist data between sessions |
| **C-4** | Must enforce single-instance operation |
| **C-5** | Must not include authentication, roles, or permissions |
| **C-6** | Email is the sole unique identifier — no separate ID field |
| **C-7** | No permanent delete — deactivation only |
| **C-8** | Tech stack and storage format are not specified in requirements |

## Out of Scope

The following are explicitly **excluded** from this project:

| Item | Notes |
|------|-------|
| Permanent delete of employee records | Deactivation preserves all records |
| Roles, permissions, or authentication | See [Permissions & Access Control](08-permissions-and-access-control.md) |
| Search, filter, or pagination | Plain alphabetical list only |
| Advanced sorting options | Alphabetical by last name is sufficient |
| Multi-user or concurrent access | Single user, single instance |
| Cloud sync, external APIs, or remote databases | Local-only operation |
| Audit trails or change history | No logging of who changed what |
| Reporting or analytics | Not required |
| Polished or production-grade visual design | Functional UI is sufficient |
| Import/export (CSV, etc.) | Not required |
| Employee fields beyond First Name, Last Name, Email, Status | Not required |
