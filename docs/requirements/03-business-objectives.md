# Business Objectives

## Objectives

| ID  | Objective                                                                 |
| --- | ------------------------------------------------------------------------- |
| BO-1 | Capture each employee's first name, last name, and active or inactive status |
| BO-2 | Let a user create, edit, deactivate, and reactivate records locally      |
| BO-3 | Keep the employee list available after the tool is closed and opened again |

## Intended Outcomes

- A user can record a new employee with first name, last name, and status
- A user can correct an existing employee's name
- A user can mark an employee inactive
- A user can mark an inactive employee active again
- The same employee list is present in the next local session

## Success Measures

| Measure                    | Pass when                                              |
| -------------------------- | ------------------------------------------------------ |
| Create                     | A new record can be created and then found in the list |
| Edit                       | A change to a name is visible afterward                |
| Deactivate                 | An active employee can be marked inactive              |
| Reactivate                 | An inactive employee can be marked active              |
| Persist                    | Saved records are still present after close and reopen |
| Local use                  | The tool works with no internet or cloud service       |

## Priority

| Order | Item                                              | Priority |
| ----- | ------------------------------------------------- | -------- |
| 1     | Create an employee and keep it after a restart    | Must     |
| 2     | Edit an existing employee                         | Must     |
| 3     | Deactivate and reactivate an employee             | Must     |
| 4     | Basic working interface                           | Must     |
| 5     | Visual polish                                     | Not now  |
