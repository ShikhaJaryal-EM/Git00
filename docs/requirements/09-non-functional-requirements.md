# Non-Functional Requirements

## Local Operation

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-1.1 | The tool runs on a local machine                                         |
| NFR-1.2 | The tool does not require a network connection                           |
| NFR-1.3 | The tool does not require a cloud or other outside service               |

**Notes**

- NFR-1.2 means no internet and no remote network. The user must be able to complete the work with the outside network off.
- A page served on the same machine (`localhost` / `127.0.0.1`) is still local. It is not a cloud or outside service (NFR-1.3).
- NFR-1.3 forbids hosted, cloud, or remote stores. A local process and a local file on the same PC are allowed.

## Persistence

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-2.1 | Saved records are still present after a normal close and reopen          |
| NFR-2.2 | Persistence works without a network or cloud service                     |
| NFR-2.3 | A normal restart does not lose saved records                             |

This section states how well data stays saved. It does not list the fields that are stored. Close and reopen means the local tool is stopped and started again; saved records must still be there. Persistence must not depend on a network or cloud service (NFR-2.2). A database server is not required.

## User Interface

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-3.1 | A basic working interface is required                                    |
| NFR-3.2 | The user can complete create, edit, deactivate, and reactivate from the UI |
| NFR-3.3 | Visual polish is not required                                            |

**Notes**

- A simple HTML page is an acceptable basic interface (NFR-3.1).
- NFR-3.3 means branding, custom themes, and a refined look are not required. A default layout so the page is readable is allowed.

## Reliability

| ID      | Requirement                                                              |
| ------- | ------------------------------------------------------------------------ |
| NFR-4.1 | A rejected create or edit does not corrupt the saved list                |
| NFR-4.2 | The tool remains usable after a rejected save                            |
| NFR-4.3 | The tool remains usable after a normal close and reopen                  |
