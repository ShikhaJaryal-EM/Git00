# 07 — Testing Strategy

## Objectives

Automated tests verify business invariants at the cheapest layer and key user journeys end to end. Every functional requirement and business rule has test coverage identified in [traceability](10-requirements-traceability.md).

## Frontend tests

Vitest, React Testing Library, and user-event test observable behavior rather than component internals:

- API client serialization and error mapping.
- Roster loading, empty/error states, deterministic rows, badges, and contextual actions.
- Create/edit form required and email validation.
- Successful create/edit/status transitions and refreshed roster.
- 409 duplicate feedback, 404/500 handling, pending-state protection.
- No delete, status-edit, search, filter, or pagination controls.

Mock HTTP at the network boundary (for example with MSW) so components exercise the real API client.

## Backend tests

JUnit 5 test layers:

| Layer | Scope |
|---|---|
| Domain unit | creation defaults, detail changes, valid/invalid status transitions |
| Application unit | use cases with mocked repository ports, uniqueness, not-found behavior, ordering delegation |
| Web slice | JSON contracts, Bean Validation, exception-to-status mapping |
| Persistence integration | Hibernate mappings, normalized unique email, ordered query, retained inactive rows |
| Full integration | controller through PostgreSQL and generated OpenAPI |

Persistence tests use a PostgreSQL Testcontainer rather than an in-memory substitute so constraints and ordering match production.

## Journey/acceptance coverage

1. Start with an empty database and receive `200 []`.
2. Create employees and verify Active status and alphabetical placement (`UJ-2`).
3. Reject blank/invalid fields and duplicate email with clear errors.
4. Edit names/email while preserving status (`UJ-3`).
5. Deactivate and verify the employee remains visible (`UJ-4`).
6. Reactivate and verify actions/status change (`UJ-5`).
7. Restart services and verify records remain (`NFR-1.2`).

## Quality gates

- Frontend lint, type-check, unit/component tests, and production build pass.
- Backend compile, unit/integration tests, migration validation, and package pass on Java 21.
- Docker images build and Compose smoke test passes.
- OpenAPI contains all five operations and declared 200/201, 400, 404, 409, and 500 responses as applicable.
- New behavior must carry a requirement/decision reference and tests.

## Manual exploratory checks

Before release, inspect keyboard-only form use, readable status indicators, browser refresh, container restart persistence, clear duplicate feedback, and Swagger examples. Visual polish and high-load testing are not release gates (`NFR-1.4`, `A-1`).
