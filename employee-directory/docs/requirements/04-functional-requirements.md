# Functional Requirements

**Version:** 1.0  
**Status:** Approved

Functional requirements describe **what the system must do**. Validation rules and business logic are defined in [Business Rules](05-business-rules.md).

---

## FR-1.1 Create Employee

The system shall allow the user to create a new employee by providing First Name, Last Name, and Email.

- On success, the employee is added with **Active** status.

## FR-1.2 Edit Employee

The system shall allow the user to edit an employee's First Name, Last Name, and Email.

- Status is **not** editable through the edit flow.

## FR-1.3 Deactivate Employee

The system shall allow the user to deactivate an **Active** employee.

- Deactivation sets status to **Inactive**.

## FR-1.4 Reactivate Employee

The system shall allow the user to reactivate an **Inactive** employee.

- Reactivation sets status to **Active**.

## FR-1.5 List Employees

The system shall display all employees (Active and Inactive) in a single list.

- Each row displays: First Name, Last Name, Email, and a status badge/label (`Active` / `Inactive`).
- Plain list only — no search, filter, or pagination required.

## FR-1.6 Row Actions

The system shall provide context-appropriate actions per employee row:

| Employee Status | Available Actions |
|-----------------|-------------------|
| Active | Edit, Deactivate |
| Inactive | Edit, Reactivate |
