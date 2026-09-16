# Non-Functional Requirements

## Local Operation

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-1.1 | The tool runs on a local machine                                         |
| NFR-1.2 | The tool does not require a network connection                           |
| NFR-1.3 | The tool does not require a cloud or other outside service               |

## Persistence

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-2.1 | Saved records are still present after a normal close and reopen          |
| NFR-2.2 | Persistence works without a network or cloud service                     |
| NFR-2.3 | A normal restart does not lose saved records                             |

This section states how well data stays saved. It does not list the fields that are stored.

## User Interface

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-3.1 | A basic working interface is required                                    |
| NFR-3.2 | The user can complete create, edit, deactivate, and reactivate from the UI |
| NFR-3.3 | Visual polish is not required                                            |

## Reliability

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-4.1 | A rejected create or edit does not corrupt the saved list                |
| NFR-4.2 | The tool remains usable after a rejected save                            |
| NFR-4.3 | The tool remains usable after a normal close and reopen                  |
