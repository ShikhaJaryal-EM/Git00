# User Journeys

## Create Employee

| Step | Actor                      | Action                                      | Result                                      |
| ---- | -------------------------- | ------------------------------------------- | ------------------------------------------- |
| 1    | Employee list maintainer   | Opens the tool                              | Existing saved records are shown            |
| 2    | Employee list maintainer   | Starts create                               | Create fields are available                 |
| 3    | Employee list maintainer   | Enters first name, last name, and status    | Values are ready to save                    |
| 4    | Employee list maintainer   | Saves the record                            | The new employee appears in the list        |
| 5    | Employee list maintainer   | Enters a blank name and tries to save       | Save is rejected and the list is unchanged  |

## Edit Employee

| Step | Actor                      | Action                                      | Result                                      |
| ---- | -------------------------- | ------------------------------------------- | ------------------------------------------- |
| 1    | Employee list maintainer   | Chooses an existing employee                | That record can be edited                   |
| 2    | Employee list maintainer   | Changes first name or last name             | New values are ready to save                |
| 3    | Employee list maintainer   | Saves the change                            | The list shows the updated name             |
| 4    | Employee list maintainer   | Clears a name and tries to save             | Save is rejected and the name is unchanged  |

## Deactivate Employee

| Step | Actor                      | Action                                      | Result                                      |
| ---- | -------------------------- | ------------------------------------------- | ------------------------------------------- |
| 1    | Employee list maintainer   | Chooses an active employee                  | Deactivate is available                     |
| 2    | Employee list maintainer   | Deactivates the employee                    | Status is inactive                          |
| 3    | Employee list maintainer   | Views the list                              | The employee is still present as inactive   |

## Reactivate Employee

| Step | Actor                      | Action                                      | Result                                      |
| ---- | -------------------------- | ------------------------------------------- | ------------------------------------------- |
| 1    | Employee list maintainer   | Chooses an inactive employee                | Reactivate is available                     |
| 2    | Employee list maintainer   | Reactivates the employee                    | Status is active                            |
| 3    | Employee list maintainer   | Views the list                              | The employee is still present as active     |
