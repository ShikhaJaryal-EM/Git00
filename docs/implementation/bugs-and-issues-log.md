# Bugs and Issues Log

This document records bugs and issues found during testing. Items are logged for later remediation; **do not fix at log time**. Each entry includes a short analysis of where the issue likely resides and which requirements/solution documents are impacted.

## Conventions

- **Impact type**: *Missed/new requirement* = capability was never specified or implemented. *Badly implemented requirement* = requirement exists but implementation is incomplete or wrong.
- **Where it resides**: Backend (API/service), Frontend (web UI), or both; plus specific docs if useful.
- **Phase when added**: The phase during which the bug was identified and logged. Use the Phase ID (e.g. PHASE-007). See `docs/implementation/implementation-plan.md` for the index and `docs/implementation/phases/` for the spec.
- **Status**: Default is **new**. Allowed values: **new**, **to do**, **priority**, **hold**, **fixed**. When status is set to **fixed**, set **Fixed date** to the current date (YYYY-MM-DD).
- **Fixed date**: Default is empty. When **Status** is set to **fixed**, set this to the current date (YYYY-MM-DD).

## Log format (per entry)

- **Status**: Default is **new**. Allowed values: **new**, **to do**, **priority**, **hold**, **fixed**. When status is set to **fixed**, set **Fixed date** to the current date (YYYY-MM-DD).
- **Fixed date**: Default is empty. When **Status** is set to **fixed**, set this to the current date (YYYY-MM-DD).

| # | Date found | Found by | Summary | Phase when added | Status | Fixed date |
|---|------------|----------|---------|------------------|--------|------------|
| (number) | YYYY-MM-DD | (name) | One-line description | Phase ID (e.g. PHASE-007) | new | |

**Analysis**

- **Where the issue likely resides**: backend / frontend / both; components or layers
- **Requirements/solution docs impacted**: list with paths
- **Impact type**: Missed/new requirement | Badly implemented requirement
- **Notes**: optional clarification

Record **Phase when added** as the phase during which the bug was identified and logged. New entries use **Status** default **new** and **Fixed date** empty; when a bug is resolved, set **Status** to **fixed** and **Fixed date** to the current date.

## Entries

Add new entries below using the same format. Increment the bug number for each new entry.
