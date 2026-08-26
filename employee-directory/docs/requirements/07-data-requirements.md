# Data Requirements

**Version:** 1.0  
**Status:** Approved

## Entity: Employee

| Field | Type | Required | Editable | Constraints |
|-------|------|----------|----------|-------------|
| **First Name** | Text | Yes | Yes | Non-empty |
| **Last Name** | Text | Yes | Yes | Non-empty |
| **Email** | Text | Yes | Yes | Unique; basic email format validation |
| **Status** | Enum | Set on create | Via activate/deactivate only | `Active` or `Inactive` |

### Notes

- **Email** serves as the unique identifier for each employee record.
- **Status** defaults to `Active` when an employee is created.
- There is no separate ID field; email is the natural key.

## Data Volume

- Expected volume is small (tens to low hundreds of employees).
- No pagination, search, or indexing requirements beyond alphabetical sort.

## Persistence

| Requirement | Detail |
|-------------|--------|
| **Storage location** | Local filesystem on the user's machine |
| **Durability** | Data must survive application restarts |
| **Format** | Not specified — left to implementation |
| **Backup / sync** | Not required |

## Sort Order

When displaying the employee list:

1. Last Name (ascending, alphabetical)
2. First Name (ascending, alphabetical)
3. Email (ascending, alphabetical — tiebreaker)

## Data Lifecycle

| Event | Effect on Data |
|-------|----------------|
| Create | New employee record added with Active status |
| Edit | First Name, Last Name, and/or Email updated in place |
| Deactivate | Status changed to Inactive; record retained |
| Reactivate | Status changed to Active; record retained |
| Delete | **Not supported** — records are never permanently removed |
