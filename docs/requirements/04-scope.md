# Scope

## In Scope

| Item                         | Meaning                                                      |
| ---------------------------- | ------------------------------------------------------------ |
| Employee details             | First name, last name, and active or inactive status         |
| Create                       | Add a new employee record                                    |
| Edit                         | Change an existing employee's first or last name             |
| Deactivate                   | Mark an employee inactive                                    |
| Reactivate                   | Mark an inactive employee active                             |
| List                         | See the saved employee records                               |
| Local use                    | Run on a local machine with no cloud or outside service      |
| Persistence                  | Keep the list after the tool is closed and opened again      |
| Basic interface              | A working UI; appearance is not important                    |
| Local process                | A process on the same machine (including a local browser page) is in scope |

## Out of Scope

| Item                         | Meaning                                                      |
| ---------------------------- | ------------------------------------------------------------ |
| Extra employee fields        | Email, phone, title, photo, or other attributes              |
| Security                     | Login, roles, permissions, or access control                 |
| Cloud or network services    | Hosting, sync, external APIs, or internet-dependent services |
| Database as a required store | A database server is not required; local persistence is      |
| Multi-user sharing           | Concurrent users or a shared remote list                     |
| Reporting and search extras  | Advanced filters, exports, or analytics                      |
| Visual design polish         | Branding, custom themes, or a refined look and feel          |
| Delete                       | Permanent removal of a record is not required for v1         |
