# Behaviour and Flows

## Purpose

Map each requirements journey to UI events, service calls, persistence, and rejection paths.

## Requirement references

- `08-user-journeys.md`
- FR-1.1–FR-5.2
- BR-1.1–BR-3.4
- AC-1.1–AC-5.3

---

## Flow map

| Journey | Trigger | Service | Persist | UI result |
| --- | --- | --- | --- | --- |
| Create | Create → Save | `Create` | `Save` on success | New row in the list |
| Edit | Edit → Save | `UpdateNames` | `Save` on success | Names updated |
| Deactivate | Deactivate on active row | `Deactivate` | `Save` | Status Inactive; row remains |
| Reactivate | Reactivate on inactive row | `Reactivate` | `Save` | Status Active; row remains |
| List | Open the list page in the local browser | `List` | `Load` | First name, last name, status |

## Create employee

### Happy path

| Step | Actor | Action | Result | Refs |
| --- | --- | --- | --- | --- |
| 1 | Maintainer | Starts the local app and opens the list page | Saved records are shown | Journey, FR-5.1 |
| 2 | Maintainer | Opens Create | Create fields available | FR-1.1 |
| 3 | Maintainer | Enters first name, last name, status | Values ready to save | FR-1.2 |
| 4 | Maintainer | Saves | Record created; appears in the list | FR-1.3, AC-1.1, AC-1.2 |

Status defaults to Active if the user does not change it (BR-2.2, DD-08).

### Rejected save

| Condition | Result | Refs |
| --- | --- | --- |
| First or last name missing, blank, or only spaces | Save rejected; user is told; list unchanged | BR-1.4, BR-3.1, BR-3.3, BR-3.4, AC-1.3, AC-1.4 |

### Cancel

Cancel returns to the list page. No record is created. List unchanged.

## Edit employee

### Happy path

| Step | Actor | Action | Result | Refs |
| --- | --- | --- | --- | --- |
| 1 | Maintainer | Chooses an existing employee | Record can be edited | FR-2.1 |
| 2 | Maintainer | Changes first and/or last name | New values ready to save | FR-2.2 |
| 3 | Maintainer | Saves | List shows the updated name | FR-2.3, AC-2.1, AC-2.2 |

Status is not changed in this flow (FR-2.2).

### Rejected save

| Condition | Result | Refs |
| --- | --- | --- |
| First or last name missing, blank, or only spaces | Save rejected; user is told; saved name unchanged | BR-3.2–BR-3.4, AC-2.3, AC-2.4 |

## Deactivate employee

| Step | Actor | Action | Result | Refs |
| --- | --- | --- | --- | --- |
| 1 | Maintainer | Chooses an active employee | Deactivate is available | BR-2.3 |
| 2 | Maintainer | Deactivates | Status is Inactive | FR-3.1, FR-3.2, AC-3.1, AC-3.2 |
| 3 | Maintainer | Views the list | Employee still present as Inactive | AC-3.3, DD-13 |

Deactivate is not offered when the employee is already inactive (BR-2.3).

## Reactivate employee

| Step | Actor | Action | Result | Refs |
| --- | --- | --- | --- | --- |
| 1 | Maintainer | Chooses an inactive employee | Reactivate is available | BR-2.4 |
| 2 | Maintainer | Reactivates | Status is Active | FR-4.1, FR-4.2, AC-4.1, AC-4.2 |
| 3 | Maintainer | Views the list | Employee still present as Active | AC-4.3 |

## Persistence across sessions

After a normal close and reopen:

| What must still be present | Refs |
| --- | --- |
| Created records | AC-5.1 |
| Edited names | AC-5.2 |
| Status changes | AC-5.3 |

Implemented by `Save` after each accepted mutation and `Load` from `employees.json` when the app is started again and the list page is opened (NFR-2.1–NFR-2.3). Close and reopen means: stop the local ASP.NET Core process, start it again, open the list in the browser.

## Service specifications

### Create(firstName, lastName, status)

```
Trim firstName and lastName
If either is empty → reject (do not persist)
If status omitted → Active
If status is not Active or Inactive → reject
Assign id
Append to list
Save()
Return the new record
```

### UpdateNames(id, firstName, lastName)

```
Trim firstName and lastName
If either is empty → reject (do not persist)
If id not found → reject
Update names only; leave status
Save()
```

### Deactivate(id)

```
If record is not Active → reject
Set status Inactive
Save()
```

### Reactivate(id)

```
If record is not Inactive → reject
Set status Active
Save()
```

### List()

```
Return all records in insertion order, including Inactive
```

## Cross-cutting rules

| Behaviour | Rule | Refs |
| --- | --- | --- |
| Rejected save | In-memory list and file stay as they were | BR-3.4, NFR-4.1, NFR-4.2 |
| Tool usable after rejection | User can correct and try again | NFR-4.2 |
| Tool usable after reopen | List loads from the JSON file after process restart | NFR-4.3 |
| Duplicate names | Allowed | DD-12 |
