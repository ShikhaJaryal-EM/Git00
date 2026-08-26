# Data Requirements

**Version:** 1.0  
**Status:** Approved

## Entity: Employee

| Field | Required | Editable | Constraints |
|-------|----------|----------|-------------|
| **First Name** | Yes | Yes | Non-empty |
| **Last Name** | Yes | Yes | Non-empty |
| **Email** | Yes | Yes | Unique; basic email format validation |
| **Status** | Set on create | Via activate/deactivate only | Active or Inactive |

## Persistence

Employee data must persist between sessions on the local machine.

## Sort Order

The employee list is displayed in alphabetical order.

## Data Lifecycle

| Event | Effect on Data |
|-------|----------------|
| Create | New employee record added with Active status |
| Edit | First Name, Last Name, and/or Email updated |
| Deactivate | Status changed to Inactive; record retained |
| Reactivate | Status changed to Active; record retained |
| Delete | Not supported |
