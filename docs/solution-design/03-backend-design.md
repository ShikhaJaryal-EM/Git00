# 03 — Backend Design

## Runtime and modules

Build a Java 21 Spring Boot REST service. Use package-by-feature with clean-architecture layers:

```text
employee/
  domain/Employee.java, EmployeeStatus.java
  application/port/in/*.java
  application/port/out/EmployeeRepository.java
  application/service/*.java
  adapter/in/web/*.java
  adapter/out/persistence/*.java
  config/*.java
```

## Domain model

`Employee` contains `firstName`, `lastName`, `email`, and `status`. Email is the identifier and there is no separate public or database ID (`BR-1`, `C-2`). `EmployeeStatus` is `ACTIVE` or `INACTIVE`.

Domain operations:

- `create(firstName, lastName, email)` sets `ACTIVE` (`BR-9`).
- `updateDetails(...)` changes only names/email and preserves status (`BR-10`).
- `deactivate()` accepts only `ACTIVE` (`BR-11`).
- `reactivate()` accepts only `INACTIVE` (`BR-12`).

## Application use cases

| Use case | Input | Rules and result |
|---|---|---|
| ListEmployees | none | Return all sorted by last name, first name, then email |
| CreateEmployee | names, email | Validate; reject existing normalized email; create Active |
| UpdateEmployee | current email, names, new email | Find current record; validate uniqueness if changed; preserve status |
| DeactivateEmployee | email | Find; require Active; persist Inactive |
| ReactivateEmployee | email | Find; require Inactive; persist Active |

Sorting resolves the otherwise ambiguous “alphabetical” rule (`BR-14`) with the conventional last-name-first ordering; this is recorded as `DEC-08` and can be changed in one repository query if the product owner chooses first-name-first.

## Repository port

The application-facing repository supports `findAllOrdered`, `findByEmail`, `existsByEmail`, `save`, and atomic email replacement during update. It exposes domain objects, not JPA entities.

## Transactions and consistency

Each mutation use case executes in one transaction. Application checks provide clear errors; the database unique constraint is the final defense against duplicate email. Translate both pre-check and constraint violation to HTTP 409 (`BR-6`–`BR-8`). There is no repository delete operation (`BR-2`).

## Mapping and dependencies

Controllers map request DTOs to use-case commands and presenters map results to response DTOs. Bean Validation handles shape constraints at the HTTP boundary; application/domain logic repeats business-critical invariants so non-HTTP callers cannot bypass them. Persistence adapters own JPA annotations and Hibernate repositories.

## Observability and documentation

Use Log4j 2 through Spring Boot's logging bridge with request correlation IDs and structured key/value context. Generate OpenAPI from controller annotations and serve Swagger UI locally. Details are in [cross-cutting concerns](06-cross-cutting-concerns.md).
