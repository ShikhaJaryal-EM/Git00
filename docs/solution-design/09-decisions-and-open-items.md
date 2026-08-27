# 09 — Decisions and Open Items

## Confirmed decisions

| ID | Decision | Rationale / source |
|---|---|---|
| DEC-01 | React + TypeScript + Vite frontend; Vitest and React Testing Library | Confirmed constraint |
| DEC-02 | Java 21 Spring Boot REST backend with clean architecture and JUnit | Confirmed constraint |
| DEC-03 | Hibernate ORM with PostgreSQL and Flyway migrations | Confirmed stack; durable local persistence (`NFR-1.2`) |
| DEC-04 | Log4j 2 logging and Springdoc OpenAPI/Swagger | Confirmed constraint |
| DEC-05 | Dockerize frontend/backend; run with PostgreSQL via local Compose | Confirmed constraint; `NFR-1.1` |
| DEC-06 | Email is normalized lowercase and is the database/API identity | `BR-1`, `C-2`; case-insensitive uniqueness |
| DEC-07 | Use explicit deactivate/reactivate action endpoints; provide no DELETE | `BR-2`, `FR-1.3`, `FR-1.4` |
| DEC-08 | Sort by last name, then first name, then email, case-insensitively | Deterministic interpretation of `BR-14` |
| DEC-09 | Invalid status transition returns 409 | Standard REST conflict semantics |
| DEC-10 | Mutation responses return the updated employee; create also returns Location | Simplifies canonical UI refresh |
| DEC-11 | No auth/access checks; bind deployment for local use | `PAC-1`–`PAC-3`, `NFR-1.1` |
| DEC-12 | Server-side roster is authoritative; no cache or pagination | `BR-13`, `BR-16`; local single-user scale |

## Open items

These choices are genuinely unspecified and do not block core implementation:

| ID | TBD / completion prompt | Default pending confirmation | Impact |
|---|---|---|---|
| OI-01 | Confirm whether “alphabetical” means last-name-first or first-name-first. | Implement DEC-08. | Repository ordering and UI tests only |
| OI-02 | Select the concrete local frontend and optional backend host ports, avoiding conflicts on the target workstation. | Record chosen ports in Compose README/environment example. | Local launch URL |
| OI-03 | Confirm acceptable maximum lengths for first and last names. | Use 100 Unicode characters each, matching schema. | Validation/schema |
| OI-04 | Confirm whether deactivate requires a browser confirmation prompt. | Include a simple confirmation because the action changes status. | One UI interaction/test |

## Completion rule

When an open item is resolved, add or amend a numbered decision, update affected design/tests, and mark the item resolved with date and owner. Do not invent additional product features while resolving these items.

## Rejected alternatives

- In-memory/file storage: weaker transactional uniqueness and persistence than confirmed PostgreSQL.
- Hard or soft delete flags: deletion is prohibited; status already represents lifecycle.
- Separate active/inactive pages or server filters: conflicts with the single roster.
- Authentication scaffolding: unnecessary scope and contrary to the approved access model.
- Kubernetes/cloud deployment: infrastructure and cloud operation are explicitly out of scope.
