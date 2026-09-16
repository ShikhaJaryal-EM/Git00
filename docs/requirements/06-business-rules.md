# Business Rules

## Name Rules

| ID     | Rule                                                                     |
| ------ | ------------------------------------------------------------------------ |
| BR-1.1 | First name is required                                                   |
| BR-1.2 | Last name is required                                                    |
| BR-1.3 | First name and last name are stored as entered, after leading and trailing spaces are removed |
| BR-1.4 | A name that is only spaces is not valid                                  |

## Status Rules

| ID     | Rule                                                                     |
| ------ | ------------------------------------------------------------------------ |
| BR-2.1 | Status is either active or inactive                                      |
| BR-2.2 | A new employee is active unless the user chooses inactive                |
| BR-2.3 | Deactivate is allowed only when the employee is active                   |
| BR-2.4 | Reactivate is allowed only when the employee is inactive                 |

## Validation Rules

| ID     | Rule                                                                     |
| ------ | ------------------------------------------------------------------------ |
| BR-3.1 | Create is rejected if first name or last name is missing or invalid      |
| BR-3.2 | Edit is rejected if first name or last name is missing or invalid        |
| BR-3.3 | The user is told when a create or edit is rejected                       |
| BR-3.4 | A rejected create or edit does not change the saved list                 |
