# Functional Requirements

## Create Employee

| ID     | Requirement                                                              |
| ------ | ------------------------------------------------------------------------ |
| FR-1.1 | The user can create a new employee record                                |
| FR-1.2 | Create captures first name, last name, and status                        |
| FR-1.3 | After create, the new record is visible in the list                      |

## Edit Employee

| ID     | Requirement                                                              |
| ------ | ------------------------------------------------------------------------ |
| FR-2.1 | The user can edit an existing employee                                   |
| FR-2.2 | Edit can change first name and last name                                 |
| FR-2.3 | After edit, the updated values are visible in the list                   |

Edit does not change status. Status after create is changed only by deactivate and reactivate.

## Deactivate Employee

| ID     | Requirement                                                              |
| ------ | ------------------------------------------------------------------------ |
| FR-3.1 | The user can deactivate an active employee                               |
| FR-3.2 | After deactivate, the employee's status is inactive                      |

## Reactivate Employee

| ID     | Requirement                                                              |
| ------ | ------------------------------------------------------------------------ |
| FR-4.1 | The user can reactivate an inactive employee                             |
| FR-4.2 | After reactivate, the employee's status is active                        |

## List Employees

| ID     | Requirement                                                              |
| ------ | ------------------------------------------------------------------------ |
| FR-5.1 | The user can see the saved employee records                              |
| FR-5.2 | Each listed record shows first name, last name, and status               |

The list includes both active and inactive employees. There is no requirement to hide inactive rows, search, or sort.
