# Data Requirements

## Employee Record

- One record represents one employee
- A record has first name, last name, and status
- A record can be created, edited, deactivated, and reactivated
- A record is not required to be permanently deleted in v1

## Fields

| Field      | Required | Values                      | Notes                          |
| ---------- | -------- | --------------------------- | ------------------------------ |
| First name | Yes      | Non-empty text              | Leading and trailing spaces removed |
| Last name  | Yes      | Non-empty text              | Leading and trailing spaces removed |
| Status     | Yes      | Active or Inactive          | Default on create is Active    |

## Persistence

- First name, last name, and status are stored for each employee
- Stored records remain after the tool is closed
- Stored records are available the next time the tool is opened
- Persistence is local; no cloud or outside store is required
- This section states what is saved, not how well it is saved
