# Business Rules

**Version:** 1.0  
**Status:** Approved

## Employee Identity

| Rule ID | Rule |
|---------|------|
| **BR-1** | Email is the unique identifier for each employee. No two employees may share the same email address. |
| **BR-2** | There is no permanent delete. Deactivation is the only way to remove an employee from active use. |

## Field Validation

| Rule ID | Rule |
|---------|------|
| **BR-3** | First Name is required and must be non-empty. |
| **BR-4** | Last Name is required and must be non-empty. |
| **BR-5** | Email is required and must pass basic format validation. |
| **BR-6** | On create, email must not already exist in the employee list. |
| **BR-7** | On edit, if email is changed, the new email must not match another employee's email. |
| **BR-8** | If the new email is already used by someone else, the system shall display a clear error message. |

## Status Management

| Rule ID | Rule |
|---------|------|
| **BR-9** | New employees are created with Active status. |
| **BR-10** | Status can only be changed via Deactivate or Reactivate — not through the edit flow. |
| **BR-11** | Deactivate is available only for Active employees. |
| **BR-12** | Reactivate is available only for Inactive employees. |

## Display & Ordering

| Rule ID | Rule |
|---------|------|
| **BR-13** | All employees (Active and Inactive) are shown in a single list. |
| **BR-14** | The list is sorted alphabetically. |
| **BR-15** | Status is displayed as a label/badge on each row. |
| **BR-16** | Plain list only — no search, filter, or pagination. |
