# Acceptance Criteria

## Create Employee

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-1.1 | A user can create an employee with first name, last name, and status     |
| AC-1.2 | The new employee appears in the list                                     |
| AC-1.3 | Create with a missing or blank name is rejected                          |
| AC-1.4 | A rejected create does not add a record                                  |

## Edit Employee

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-2.1 | A user can change an existing employee's first or last name              |
| AC-2.2 | The list shows the updated name                                          |
| AC-2.3 | Edit with a missing or blank name is rejected                            |
| AC-2.4 | A rejected edit leaves the saved name unchanged                          |

## Deactivate Employee

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-3.1 | A user can deactivate an active employee                                 |
| AC-3.2 | After deactivate, status is inactive                                     |
| AC-3.3 | The employee remains in the list                                         |

## Reactivate Employee

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-4.1 | A user can reactivate an inactive employee                               |
| AC-4.2 | After reactivate, status is active                                       |
| AC-4.3 | The employee remains in the list                                         |

## Persistence

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-5.1 | After a normal close and reopen, created records are still present       |
| AC-5.2 | After a normal close and reopen, edited names are still present          |
| AC-5.3 | After a normal close and reopen, status changes are still present        |

## Local Operation

| ID     | Criterion                                                                |
| ------ | ------------------------------------------------------------------------ |
| AC-6.1 | The tool can be used with no network connection                          |
| AC-6.2 | The tool does not require a cloud or other outside service               |
| AC-6.3 | Create, edit, deactivate, and reactivate can be completed in the UI      |
