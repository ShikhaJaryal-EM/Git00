# Non-Functional Requirements

**Version:** 1.0  
**Status:** Approved

Non-functional requirements describe **how the system must behave** — quality attributes rather than specific features.

---

## NFR-1.1 Local Execution

The application shall run entirely on the user's local machine. No cloud services or external resources are required for core operation.

## NFR-1.2 Persistence

Employee data shall persist between application sessions. Closing and reopening the application must not lose employee records.

## NFR-1.3 Single Instance

The application shall enforce a single running instance at a time. If a second instance is attempted, it shall be blocked or the user shall be directed to the existing instance. Multi-tab or multi-process conflict handling is not required.

## NFR-1.4 User Interface

The application shall provide a functional web-based UI accessible in a browser. Visual polish and production-grade design are not a priority.

## NFR-1.5 Security

No authentication, roles, permissions, or security model is required. See [Permissions & Access Control](08-permissions-and-access-control.md).

## NFR-1.6 Platform

The application shall run on the user's local operating system. Specific OS support is not defined — left to implementation.

## NFR-1.7 Dependencies

The application shall not require cloud services, external APIs, or remote databases for core operation.
