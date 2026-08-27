# Bugs and Issues Log

This document records bugs and issues found during testing. Items are logged for later remediation; do not fix them at log time. Each entry includes a short analysis of where the issue likely resides and which requirements or solution documents are impacted.

## Conventions

- **Impact type:** `Missed/new requirement` means a capability was never specified or implemented. `Badly implemented requirement` means a requirement exists but its implementation is incomplete or wrong.
- **Where it resides:** Identify Backend (API/service), Frontend (web UI), or both, plus specific components, layers, or documents when useful.
- **Phase when added:** Record the phase during which the issue was identified and logged using its Phase ID from the [implementation plan](implementation-plan.md).
- **Status:** Default is **new**. Allowed values are **new**, **to do**, **priority**, **hold**, and **fixed**.
- **Fixed date:** Default is empty. When Status becomes **fixed**, set Fixed date to the current date in `YYYY-MM-DD` format.

## Entry template

Copy this complete block under [Entries](#entries), replace placeholders, and increment the bug number.

### Bug #(number)

| # | Date found | Found by | Summary | Phase when added | Status | Fixed date |
|---|------------|----------|---------|------------------|--------|------------|
| (number) | YYYY-MM-DD | (name) | One-line description | Phase ID (e.g. PHASE-004) | new | |

**Analysis**

- **Where the issue likely resides:** Backend / Frontend / both; identify components or layers.
- **Requirements/solution docs impacted:** List repository-relative document paths.
- **Impact type:** Missed/new requirement | Badly implemented requirement
- **Notes:** Optional clarification.

Record **Phase when added** as the phase during which the bug was identified and logged. New entries use **Status** `new` and an empty **Fixed date**; when resolved, set **Status** to `fixed` and **Fixed date** to the current date.

## Entries

No bugs or issues have been logged.

Add new entries below using the same format. Increment the bug number for each new entry.
