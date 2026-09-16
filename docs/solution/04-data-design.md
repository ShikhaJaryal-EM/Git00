# Data Design

## Purpose

Define the employee record, stored fields, JSON schema, persistence rules, and integrity checks.

## Requirement references

- `07-data-requirements.md`
- BR-1.1–BR-3.4
- FR-1.1–FR-5.2
- NFR-2.1–NFR-2.3

---

## Employee record

One record is one employee. v1 fields are first name, last name, and status. Permanent delete is not required.

The file also stores a system `id` so edit, deactivate, and reactivate target the correct row when two people share a name (**DD-04**, **DD-12**). The id is not shown and is not editable.

### Logical model

| Field | Required | Values | User visible | User editable | Notes |
| --- | --- | --- | --- | --- | --- |
| Id | Yes | GUID string | No | No | Assigned on create |
| First name | Yes | Non-empty text after trim | Yes | Create and edit | BR-1.1, BR-1.3, BR-1.4 |
| Last name | Yes | Non-empty text after trim | Yes | Create and edit | BR-1.2, BR-1.3, BR-1.4 |
| Status | Yes | Active or Inactive | Yes | Create (choice); later via deactivate/reactivate | BR-2.1, BR-2.2 |

### Status values

| Value | Meaning |
| --- | --- |
| `Active` | Default on create unless the user chooses Inactive |
| `Inactive` | Set by deactivate; cleared by reactivate |

No other status values are stored.

## JSON schema

Path: `%AppData%\EmployeeRecordsTool\employees.json`

```json
{
  "employees": [
    {
      "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
      "firstName": "Jane",
      "lastName": "Smith",
      "status": "Active"
    },
    {
      "id": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
      "firstName": "John",
      "lastName": "Doe",
      "status": "Inactive"
    }
  ]
}
```

Serialise with indented JSON so the file is readable on disk. Property names are camelCase.

## Persistence rules

| Rule | Design | Refs |
| --- | --- | --- |
| What is saved | id, first name, last name, status | Data requirements |
| When saved | After every accepted create, edit, deactivate, or reactivate | NFR-2.1 |
| How | Rewrite the full file from the in-memory list | DD-05 |
| Load | On startup, before the list is shown | NFR-2.3 |
| Local only | No network path and no cloud store | NFR-2.2 |
| No delete | Deactivate sets status; the record stays in the file | Scope |
| Rejected change | Validation failure does not call Save | BR-3.4, NFR-4.1 |

**DD-05:** Full-file rewrite is enough for a single-user list.

### Save

```
Save():
  1. Serialise the current in-memory list
  2. Write employees.json
  3. If the write fails, surface an error; do not report success
```

### Load

```
Load():
  1. If the folder is missing, create it
  2. If the file is missing, treat as empty list and create an empty file
  3. If the file is present, deserialise
  4. If deserialise fails, do not overwrite the file; show an error
```

## Integrity

| Rule | Enforcement | Refs |
| --- | --- | --- |
| First and last name required | Reject create/edit before Save | BR-1.1, BR-1.2, BR-3.1, BR-3.2 |
| Trim then validate | Store the trimmed value | BR-1.3 |
| Spaces-only is invalid | Same as missing | BR-1.4 |
| Status is Active or Inactive | Validate on create; deactivate/reactivate only flip those two | BR-2.1 |
| Duplicate names allowed | No uniqueness check | DD-12 |
| List includes inactive | Inactive rows remain in the file and in the list | User journeys, DD-13 |

## List order

**DD-06:** Display in insertion order (file order). No sort control. Search and sort are out of scope.

## Not stored

| Excluded | Why |
| --- | --- |
| Email, phone, title, photo | Extra fields out of scope |
| Created/updated timestamps | Not required |
| Audit history | Not required |
| User or owner | Single local user; no login |
| Deleted flag | Status is used instead of delete |
