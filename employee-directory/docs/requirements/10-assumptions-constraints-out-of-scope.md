# Assumptions, Constraints & Out of Scope

**Version:** 1.0  
**Status:** Approved

## Assumptions

| ID | Assumption |
|----|------------|
| **A-1** | A single user operates the application on a single machine |
| **A-2** | Only one instance of the application runs at a time |

## Constraints

| ID | Constraint |
|----|------------|
| **C-1** | Must provide a browser-based UI |
| **C-2** | Email is the unique identifier — no separate ID field |
| **C-3** | No permanent delete — deactivation only |
| **C-4** | Tech stack and storage format are not specified |

## Out of Scope

| Item | Notes |
|------|-------|
| Permanent delete of employee records | Deactivation only |
| Roles, permissions, or authentication | |
| Search, filter, or pagination | Plain list only |
| Multi-user or concurrent access | Single user, single instance |
| Cloud or other outside resources | Local operation only |
| Polished visual design | Functional UI is sufficient |
