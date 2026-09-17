# Error Handling

## Purpose

Define validation errors, file errors, and save failures so rejected work does not corrupt the list and the user is told what happened (BR-3.3, BR-3.4, NFR-4.1, NFR-4.2).

## Requirement references

- BR-1.1–BR-3.4
- NFR-4.1–NFR-4.3
- AC-1.3, AC-1.4, AC-2.3, AC-2.4

---

## Principles

| Principle | Meaning | Refs |
| --- | --- | --- |
| Tell the user | Rejected create or edit shows a plain message | BR-3.3 |
| Do not write invalid data | Validation runs before `Save` | BR-3.1, BR-3.2, BR-3.4 |
| Do not corrupt the list | File and in-memory list stay unchanged on rejection | NFR-4.1 |
| Remain usable | User can correct input and try again | NFR-4.2 |
| Preserve a corrupt file | Do not overwrite unreadable JSON | NFR-4.1, DD-11 |

## Validation errors

### Create — missing or invalid name

| Aspect | Design |
| --- | --- |
| Trigger | Save with first or last name missing, blank, or only spaces (after trim) |
| Message | “First name is required.” or “Last name is required.” |
| Persist | No |
| List | Unchanged |
| Form | Stays open |

Refs: BR-1.1–BR-1.4, BR-3.1, BR-3.3, BR-3.4, AC-1.3, AC-1.4

### Edit — missing or invalid name

Same messages and behaviour as create. The previously saved name remains.

Refs: BR-3.2–BR-3.4, AC-2.3, AC-2.4

### Invalid status on create

| Aspect | Design |
| --- | --- |
| Trigger | Status not Active or Inactive (should not happen if the control only offers those two) |
| Message | “Status must be Active or Inactive.” |
| Persist | No |

Refs: BR-2.1

### Wrong status action

| Aspect | Design |
| --- | --- |
| Trigger | Deactivate when already inactive, or reactivate when already active |
| UI | Action is not shown, so this is a guard in the service only |
| Persist | No |

Refs: BR-2.3, BR-2.4

## Cancellation

Cancel on create or edit returns to the list page and writes nothing.

## File errors

### Missing file on first run (DD-11)

Create the data folder and an empty `employees.json`. Show an empty list. No error message. The user can create employees immediately. No database is created.

### Corrupt file on load (DD-11)

Do **not** overwrite or delete the file. Show: “The employee data file could not be read.” Include the file path. Do not accept creates or edits until the file is fixed or replaced. This avoids destroying data that might still be recoverable.

### Save failure (disk full, permissions)

Show: “Could not save employee data. Please try again.” Do not report the operation as successful. The tool remains usable so the user can retry (NFR-4.2).

## Message catalogue

| ID | Message | When |
| --- | --- | --- |
| ERR-01 | First name is required. | Create or edit |
| ERR-02 | Last name is required. | Create or edit |
| ERR-03 | Status must be Active or Inactive. | Create |
| ERR-04 | The employee data file could not be read. | Corrupt file |
| ERR-05 | Could not save employee data. Please try again. | Write failure |
| ERR-06 | Employee record not found. | Stale id (should not happen from the UI) |

Do not show stack traces or technical codes to the user.

## Not applicable

| Error class | Why it does not apply |
| --- | --- |
| Network errors | No internet or remote services; app uses localhost only (NFR-1.2) |
| Authentication errors | No login |
| Concurrent-edit conflicts | One user, one machine |
| Database connection errors | No database is used |
