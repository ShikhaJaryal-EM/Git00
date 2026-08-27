# Employee Directory Solution Design

**Version:** 1.0  
**Status:** Implementation-ready baseline

This package translates the [approved requirements](../requirements/01-executive-summary-and-context.md) into an implementation design for a local, single-user Employee Directory.

## Design index

| # | Document | Purpose |
|---|---|---|
| 00 | [Document control](00-document-control.md) | Ownership, status, and change process |
| 01 | [Architecture overview](01-architecture-overview.md) | Context, containers, and runtime flow |
| 02 | [Frontend design](02-frontend-design.md) | React UI structure and behavior |
| 03 | [Backend design](03-backend-design.md) | Spring Boot clean architecture |
| 04 | [API design](04-api-design.md) | REST resources, contracts, and errors |
| 05 | [Data design](05-data-design.md) | PostgreSQL model and persistence rules |
| 06 | [Cross-cutting concerns](06-cross-cutting-concerns.md) | Validation, logging, and error handling |
| 07 | [Testing strategy](07-testing-strategy.md) | Automated test layers and coverage |
| 08 | [Local containerization](08-local-containerization.md) | Docker Compose topology and operation |
| 09 | [Decisions and open items](09-decisions-and-open-items.md) | Confirmed decisions and genuine TBDs |
| 10 | [Requirements traceability](10-requirements-traceability.md) | Requirement-to-design verification map |

## Confirmed scope

- React, TypeScript, Vite, Vitest, and React Testing Library.
- Java 21 Spring Boot REST API, clean architecture, Hibernate ORM, PostgreSQL, JUnit, Log4j 2, and OpenAPI.
- Local Docker Compose runtime with separately containerized frontend and backend plus PostgreSQL.
- One user and one running instance; no authentication, authorization, cloud infrastructure, delete, search, filter, or pagination.
- One alphabetically ordered roster containing Active and Inactive employees.

## Requirement identifiers

Design statements reference approved IDs (`BO-*`, `FR-*`, `BR-*`, `UJ-*`, `PAC-*`, `NFR-*`, `A-*`, `C-*`). Data requirements without source IDs are referenced as `DR-Employee`, `DR-Persistence`, `DR-Sort`, and `DR-Lifecycle` within this package.
