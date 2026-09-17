# Stakeholders and Personas

## Stakeholders

| Stakeholder     | What they care about                                  | In v1 |
| --------------- | ----------------------------------------------------- | ----- |
| Tool user       | Keep employee names and status up to date, locally    | Yes   |
| Decision maker  | Small local v1: create, edit, deactivate, reactivate  | Yes   |
| Security owner  | Roles and access control                              | No    |
| Cloud owner     | External or cloud services                            | No    |

## Personas

**In v1: Employee list maintainer**

The person who keeps the local employee list up to date.

| Need        | Meaning                                     |
| ----------- | ------------------------------------------- |
| Create      | Add a new employee                          |
| Edit        | Correct a first or last name                |
| Deactivate  | Mark an employee inactive                   |
| Reactivate  | Mark an employee active again               |
| Persist     | See the same list after closing the tool    |

Closing the tool means stopping it and opening it again in a later local session.

**Not in v1**

| Persona        | Why not                    |
| -------------- | -------------------------- |
| Viewer         | No view-only role          |
| Administrator  | No separate admin role     |
