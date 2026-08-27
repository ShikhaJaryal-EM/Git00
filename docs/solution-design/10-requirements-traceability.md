# 10 — Requirements Traceability

## Objectives and functional behavior

| IDs | Design realization | Primary verification |
|---|---|---|
| BO-1, FR-1.5, UJ-1 | One complete server-sorted roster in [frontend](02-frontend-design.md) and [API](04-api-design.md) | Roster component and API integration tests |
| BO-2, FR-1.1, FR-1.2, UJ-2, UJ-3 | Create/edit forms and use cases; edit preserves status | Frontend journeys and backend use-case tests |
| BO-3, FR-1.3, FR-1.4, FR-1.6, UJ-4, UJ-5 | Status badge plus contextual deactivate/reactivate actions | UI action tests and transition integration tests |
| BO-4 | Normalized unique email in application and database | Duplicate create/edit tests |
| BO-5, NFR-1.1 | Local-only [Compose topology](08-local-containerization.md) | Compose smoke test |
| BO-6, NFR-1.2 | PostgreSQL named volume and migrations | Restart persistence acceptance test |

## Business rules

| IDs | Design realization | Primary verification |
|---|---|---|
| BR-1, BR-6, BR-7, BR-8 | Lowercase normalized email primary key; 409 conflict and clear field feedback | Domain/persistence/API/UI conflict tests |
| BR-2 | No API, repository, UI, or SQL delete path | Contract/static review; deactivation journey |
| BR-3, BR-4, BR-5 | Layered nonblank name and basic email validation | Form, controller, domain, constraint tests |
| BR-9, BR-10 | Domain create defaults Active; edit DTO has no status | Domain and API tests |
| BR-11, BR-12 | Guarded domain transitions and status-specific buttons | Domain transition and UI tests |
| BR-13, BR-14, BR-15 | One ordered response; one table with status badges | Persistence query and component tests |
| BR-16 | No list query parameters or search/filter/page controls | OpenAPI and UI tests |

## Data, access, and constraints

| IDs | Design realization | Primary verification |
|---|---|---|
| DR-Employee | Employee schema contains required editable names/email and controlled status | Migration and mapping tests |
| DR-Persistence, DR-Lifecycle | Local PostgreSQL volume; status changes retain rows | Restart and lifecycle tests |
| DR-Sort | Last name, first name, email ordering (`DEC-08`) | Ordered repository test |
| PAC-1, PAC-2, PAC-3 | No login, role model, or permission middleware | Architecture/API review |
| NFR-1.3, A-1, A-2 | Single local Compose project; no distributed coordination | Compose documentation review |
| NFR-1.4, C-1 | Functional browser UI with accessible native controls | Component tests/manual check |
| C-2 | Email is API path identifier and database primary key | Contract and schema tests |
| C-3 | No permanent delete | Same controls as BR-2 |

## Requirement-to-test completion

Implementation is complete only when each row's verification exists and passes. Any changed requirement must update this matrix, its linked design, OpenAPI where applicable, and automated tests. Open interpretation of alphabetical order is tracked as `OI-01`; all other approved behaviors have a confirmed design path.
