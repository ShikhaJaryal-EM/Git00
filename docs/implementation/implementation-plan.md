# Employee Directory Implementation Plan

This plan implements the approved requirements and solution design in seven dependency-ordered phases. Requirements remain authoritative for behavior; the solution design is authoritative for technical choices. Open items `OI-01` through `OI-04` remain pending and use only their documented defaults until the product owner confirms them.

## Phase index

1. [PHASE-001 Platform and Data Foundation](#phase-001-platform-and-data-foundation)
2. [PHASE-002 Backend List and Cross-Cutting Baseline](#phase-002-backend-list-and-cross-cutting-baseline)
3. [PHASE-003 Frontend Shell and Employee List](#phase-003-frontend-shell-and-employee-list)
4. [PHASE-004 Create Employee Vertical Slice](#phase-004-create-employee-vertical-slice)
5. [PHASE-005 Edit Employee Vertical Slice](#phase-005-edit-employee-vertical-slice)
6. [PHASE-006 Status Transitions and Row Actions](#phase-006-status-transitions-and-row-actions)
7. [PHASE-007 Local Containerization and Acceptance Closure](#phase-007-local-containerization-and-acceptance-closure)

## Scope guardrails

- Include one local, browser-based, single-user, single-instance Employee Directory.
- Include one alphabetically ordered list containing Active and Inactive employees.
- Include create, edit, deactivate, and reactivate only.
- Exclude authentication, authorization, roles, permanent delete, search, filter, pagination, cloud resources, distributed infrastructure, and multi-user/concurrent-access design.
- Treat the server-sorted roster as authoritative and retain all employee records.

## Open items carried through implementation

- **OI-01:** Confirm whether alphabetical ordering is last-name-first or first-name-first. Pending confirmation, implement `DEC-08` (last name, first name, email, case-insensitively) and isolate the ordering decision in the repository query and tests.
- **OI-02:** Select concrete local frontend and optional backend host ports. Keep ports configurable and record chosen values during PHASE-007; do not claim this item resolved without confirmation.
- **OI-03:** Confirm maximum first-name and last-name lengths. Pending confirmation, use the documented 100-Unicode-character default consistently in schema, validation, and tests.
- **OI-04:** Confirm whether deactivation requires a browser confirmation. Pending confirmation, include the documented simple confirmation and isolate its UI test; do not claim this item resolved without confirmation.

## PHASE-001 Platform and Data Foundation

#### Phase Identity

**Phase ID**: PHASE-001  
**Phase Name**: Platform and Data Foundation  
**FR Mapping**: None (foundational)

#### Goal

The backend platform can start against a migrated PostgreSQL database whose employee model and persistence boundaries enforce the approved foundational invariants.

#### Dependencies

None

#### Plan

1. Create the Java 21 Spring Boot backend build and package-by-feature clean-architecture directories for domain, application ports and services, web adapters, persistence adapters, and configuration.
2. Configure Hibernate ORM, PostgreSQL, Flyway, Jakarta Bean Validation, JUnit 5, and PostgreSQL Testcontainers without coupling domain or application classes to Spring Data or HTTP.
3. Create the `EmployeeStatus` domain type with only `ACTIVE` and `INACTIVE`.
4. Create the `Employee` domain model with email as its sole identifier, required first and last names, and no delete operation or separate identifier.
5. Implement shared normalization rules that trim names, preserve name case and internal spaces, and trim and lowercase email before lookup or persistence.
6. Create the initial Flyway migration for `employee` with the normalized email primary key, non-null name columns, nonblank checks, and an `ACTIVE`/`INACTIVE` status check.
7. Map a persistence entity to the table and implement domain-to-persistence mapping behind an application-facing repository port with no delete method.
8. Configure Hibernate for schema validation only and test profiles for PostgreSQL Testcontainers.
9. Apply the pending `OI-03` default of 100 Unicode characters to both name columns and validation while keeping the choice traceable and replaceable.

#### Outcome

- A Java 21 Spring Boot backend compiles with clean-architecture boundaries.
- A versioned migration creates exactly one employee table with the approved constraints.
- Employee email is normalized and serves as both database and domain identity.
- Hibernate validates rather than creates the schema.
- Persistence integration tests run against PostgreSQL, not an in-memory substitute.

#### Automated Verification (AI Gate)

1. Run the backend clean compile and architecture-boundary tests.  
   **Expected:** The build succeeds on Java 21 and domain/application code has no web or persistence-framework dependencies.
2. Start a PostgreSQL Testcontainer and execute all Flyway migrations from an empty database.  
   **Expected:** Flyway creates the employee table once and Hibernate schema validation succeeds.
3. Run persistence tests for required names, valid status values, normalized lowercase email identity, and case-insensitive duplicate rejection.  
   **Expected:** Valid employees persist and every database invariant rejects invalid data.
4. Inspect repository ports and generated schema for delete operations or a separate employee ID.  
   **Expected:** Neither a delete path nor a separate identifier exists.

#### Human Verification (Human Gate)

1. Review the package boundaries against the clean-architecture dependency direction in the solution design.
2. Confirm the migration matches the employee physical schema and retains the documented `OI-03` default as pending.
3. Confirm no user, role, audit, soft-delete, search, filter, pagination, or cloud-related model was introduced.

#### Success Criteria

- [ ] ✅ The Java 21 backend compiles.
- [ ] ✅ The initial Flyway migration succeeds on PostgreSQL.
- [ ] ✅ Employee constraints and normalized email uniqueness are enforced.
- [ ] ✅ Hibernate schema validation succeeds.
- [ ] ✅ The repository port contains no delete operation.
- [ ] ✅ `OI-03` remains explicitly pending with its documented default.

## PHASE-002 Backend List and Cross-Cutting Baseline

#### Phase Identity

**Phase ID**: PHASE-002  
**Phase Name**: Backend List and Cross-Cutting Baseline  
**FR Mapping**:
- FR-1.5 List Employees

#### Goal

The backend can return every Active and Inactive employee in one deterministic alphabetical roster through a documented, observable, locally accessible API.

#### Dependencies

PHASE-001 Platform and Data Foundation

#### Plan

1. Extend the repository port and PostgreSQL adapter with `findAllOrdered` using case-insensitive last name, first name, and email ordering as the pending `OI-01` default from `DEC-08`.
2. Implement the list-employees application use case to return every employee without status partitioning, query criteria, caching, or pagination.
3. Implement `GET /api/employees` with a `200` JSON array response and no list query parameters.
4. Create stable employee response mapping with `firstName`, `lastName`, `email`, and `status`.
5. Configure the canonical error schema and `@RestControllerAdvice` for safe `400`, `404`, `409`, and `500` responses used by current and later operations.
6. Configure Log4j 2 without duplicate logging bindings and add an HTTP correlation filter that accepts or generates `X-Correlation-ID`, returns it, and clears thread context.
7. Mask or one-way-derive email identifiers in logs and prevent request bodies, full emails, stack traces, and database details from entering API responses.
8. Configure local-origin-only CORS, Spring profiles, environment-based settings, and a health endpoint for liveness and database readiness.
9. Generate OpenAPI documentation for the list operation and serve Swagger UI only from the local backend.
10. Add domain, application, web-slice, persistence, full-integration, logging, correlation, and OpenAPI contract tests for the baseline.

#### Outcome

- `GET /api/employees` returns one unpaged array containing both statuses.
- The roster is sorted by the documented pending `OI-01` default.
- API errors, correlation IDs, logs, CORS, health checks, and OpenAPI share a tested baseline.
- The API exposes no authentication, authorization, delete, search, filter, or pagination behavior.

#### Automated Verification (AI Gate)

1. Run repository integration tests with mixed-case names, duplicate sort keys, and both statuses.  
   **Expected:** Results are deterministic by lowercased last name, first name, then email and include every row.
2. Call `GET /api/employees` against empty and populated PostgreSQL test data.  
   **Expected:** Responses are `200 []` or a correctly shaped, server-sorted complete roster.
3. Validate generated OpenAPI and controller mappings.  
   **Expected:** The list operation exists with no query parameters, and no DELETE or access-control operation exists.
4. Run correlation, safe-error, CORS, and logging tests.  
   **Expected:** Correlation IDs round-trip, thread context clears, local origin is allowed, and sensitive/internal details are absent.
5. Run backend compile, unit, web-slice, persistence, and full-integration suites.  
   **Expected:** All tests pass with no skipped or disabled tests.

#### Human Verification (Human Gate)

1. Review ordering behavior and confirm `OI-01` is still pending rather than silently resolved.
2. Inspect Swagger UI locally for a clear list contract and representative response.
3. Review logs and an induced error response for useful diagnostics without full email or internal details.

#### Success Criteria

- [ ] ✅ The complete roster endpoint returns `200` for empty and populated data.
- [ ] ✅ Active and Inactive employees appear in one response.
- [ ] ✅ Deterministic ordering tests pass.
- [ ] ✅ Error, logging, correlation, CORS, health, and OpenAPI tests pass.
- [ ] ✅ No list query parameters or excluded-scope endpoints exist.
- [ ] ✅ `OI-01` remains explicitly pending with the `DEC-08` default.

## PHASE-003 Frontend Shell and Employee List

#### Phase Identity

**Phase ID**: PHASE-003  
**Phase Name**: Frontend Shell and Employee List  
**FR Mapping**:
- FR-1.5 List Employees

#### Goal

The browser UI can load and display the complete server-sorted employee roster with accessible status indicators and recoverable loading, empty, and error states.

#### Dependencies

PHASE-002 Backend List and Cross-Cutting Baseline

#### Plan

1. Create the React, strict TypeScript, and Vite frontend with Vitest, React Testing Library, user-event, linting, type-checking, and production-build scripts.
2. Define the employee client model with normalized email identity and `ACTIVE` or `INACTIVE` status.
3. Implement the API client for `GET /api/employees` with typed response parsing, stable error mapping, and configurable local API base URL.
4. Create `EmployeeDirectoryPage` to own roster loading, retry behavior, and visible outcome messages.
5. Create `EmployeeTable` and `StatusBadge` to render one semantic table with first name, last name, email, and status text in addition to color.
6. Render explicit loading, empty-roster, recoverable-error, and populated-roster states while preserving server order.
7. Add labeled native controls, table headers, keyboard-accessible retry, and `aria-live` feedback.
8. Ensure the shell renders no login, role, delete, search, filter, pagination, separate inactive list, or status-edit control.
9. Mock HTTP at the network boundary and test loading, empty, error/retry, ordered rendering, both statuses, accessibility, and excluded controls.

#### Outcome

- A functional browser shell loads the roster from the backend.
- One semantic table displays every employee and a textual Active/Inactive badge.
- Loading, empty, failure, and retry behavior are observable and tested.
- The UI contains none of the excluded controls.

#### Automated Verification (AI Gate)

1. Run frontend lint and strict TypeScript checks.  
   **Expected:** Both commands complete with zero errors.
2. Run component tests for loading, empty, populated, failed, and retried requests.  
   **Expected:** Each visible state matches the API outcome and Retry performs a new request.
3. Run roster rendering tests with Active and Inactive employees in server-supplied order.  
   **Expected:** One semantic table shows all fields and readable status text without client reordering.
4. Query rendered controls for login, delete, search, filter, pagination, status editing, or separate status lists.  
   **Expected:** No excluded-scope control is present.
5. Build the frontend production bundle.  
   **Expected:** The Vite production build succeeds.

#### Human Verification (Human Gate)

1. Open the UI with empty and populated data and confirm the complete roster is understandable without visual polish.
2. Navigate loading recovery and the table using only the keyboard.
3. Confirm status is distinguishable by text and not color alone.

#### Success Criteria

- [ ] ✅ Frontend lint, type-check, tests, and production build pass.
- [ ] ✅ Loading, empty, error, retry, and populated states render correctly.
- [ ] ✅ One accessible table displays all employees and fields.
- [ ] ✅ Active and Inactive status labels are readable.
- [ ] ✅ No excluded-scope UI controls are rendered.

## PHASE-004 Create Employee Vertical Slice

#### Phase Identity

**Phase ID**: PHASE-004  
**Phase Name**: Create Employee Vertical Slice  
**FR Mapping**:
- FR-1.1 Create Employee

#### Goal

The system can create a valid employee with Active status, reject invalid or duplicate input with clear feedback, and show the employee in the canonical roster.

#### Dependencies

PHASE-003 Frontend Shell and Employee List

#### Plan

1. Add domain and application creation behavior that trims names, normalizes email, validates required fields and basic email format, rejects duplicate normalized email, and always assigns `ACTIVE`.
2. Extend the repository adapter with existence checks and transactional save behavior while retaining the database primary-key constraint as the final uniqueness defense.
3. Implement `POST /api/employees` with create request validation, `201` employee response, encoded `Location`, and canonical `400`, `409`, and `500` errors.
4. Translate both application pre-check and database uniqueness violations to `409 EMAIL_CONFLICT` with an actionable email field error.
5. Document the create operation, request, response, validation, requirement ID, and errors in generated OpenAPI.
6. Create the frontend employee form in create mode with empty fields, labels, trimming, required validation, basic email validation, and focus on the first invalid field.
7. Submit create requests through the API client, disable duplicate submission while pending, preserve entered values on failure, and map field and page errors visibly.
8. Close the form and refresh the server-sorted roster after success so the new Active employee appears in canonical order.
9. Add backend and frontend tests for valid creation, normalization, blank/invalid fields, case-insensitive duplicates, pending state, retry after failure, and Active status.

#### Outcome

- `POST /api/employees` creates a normalized Active employee and returns `201` with `Location`.
- Layered validation rejects blank names, malformed email, and duplicate normalized email.
- The create form provides accessible inline and general feedback without losing failed input.
- A successful creation refreshes the canonical roster.

#### Automated Verification (AI Gate)

1. Run domain and application tests for creation defaults, trimming, normalization, validation, and uniqueness.  
   **Expected:** Valid input creates `ACTIVE`; invalid and duplicate input is rejected deterministically.
2. Run API integration tests for `POST /api/employees`.  
   **Expected:** Valid input returns `201` with employee and `Location`; invalid input returns `400`; duplicates return `409 EMAIL_CONFLICT`.
3. Force a database uniqueness race path in a persistence-backed test.  
   **Expected:** The constraint violation is translated to the same safe `409` contract.
4. Run create-form tests for keyboard input, client validation, pending submission, server field errors, retained values, success closure, and roster refresh.  
   **Expected:** Each observable state and response matches the create journey.
5. Validate OpenAPI and run complete frontend and backend quality commands.  
   **Expected:** The create contract is documented and all checks pass without skipped tests.

#### Human Verification (Human Gate)

1. Create an employee through the browser and confirm the employee appears Active in alphabetical order.
2. Try blank, malformed, and duplicate email input and assess whether correction guidance is clear.
3. Confirm the form has no status field and no authentication, deletion, search, filter, or pagination affordance.

#### Success Criteria

- [ ] ✅ Valid create returns `201`, `Location`, and an Active employee.
- [ ] ✅ Required and basic email validation pass at every designed layer.
- [ ] ✅ Case-insensitive duplicate email returns clear `409` feedback.
- [ ] ✅ Pending state prevents duplicate submission.
- [ ] ✅ Successful create refreshes the canonical roster.
- [ ] ✅ Create-related frontend and backend tests pass.

## PHASE-005 Edit Employee Vertical Slice

#### Phase Identity

**Phase ID**: PHASE-005  
**Phase Name**: Edit Employee Vertical Slice  
**FR Mapping**:
- FR-1.2 Edit Employee

#### Goal

The system can edit either status employee's name or email while preserving status and rejecting invalid, missing, or duplicate updates with clear feedback.

#### Dependencies

PHASE-004 Create Employee Vertical Slice

#### Plan

1. Add domain detail-update behavior that changes only first name, last name, and email while preserving employee status.
2. Implement the update application use case to locate by normalized current email, validate and normalize desired values, check uniqueness only against another employee, and replace email atomically in one transaction.
3. Implement `PUT /api/employees/{email}` using a URL-encoded current email path and desired email in the body with `200`, `400`, `404`, `409`, and `500` contracts.
4. Translate not-found and both pre-check and database duplicate paths to the canonical safe errors.
5. Document edit semantics, identity replacement, status preservation, requirement ID, and all responses in generated OpenAPI.
6. Extend `EmployeeForm` with edit mode prepopulated from either an Active or Inactive row and omit any editable status control.
7. Encode email path segments in the API client and preserve form values while displaying validation, conflict, not-found, or unexpected errors.
8. Disable duplicate submission while pending and refresh the server-sorted roster after success.
9. Add domain, application, persistence, web, integration, API-client, and component tests for unchanged/changed email, encoding, normalization, status preservation, invalid input, missing employee, and duplicate email.

#### Outcome

- `PUT /api/employees/{email}` updates names and optionally the email identity atomically.
- Active and Inactive employees can both be edited without changing status.
- Invalid, missing, and duplicate edits return stable and actionable errors.
- The browser edit flow refreshes the canonical roster after success.

#### Automated Verification (AI Gate)

1. Run domain and use-case tests for editing Active and Inactive employees.  
   **Expected:** Allowed details change and the original status is preserved in every case.
2. Run persistence integration tests for unchanged email and transactional primary-key email replacement.  
   **Expected:** Exactly one updated row remains and duplicate replacement is rejected without partial changes.
3. Run API integration tests for valid, malformed, missing, and duplicate updates, including special characters in encoded email paths.  
   **Expected:** Responses are respectively `200`, `400`, `404`, and `409` with the canonical schema.
4. Run edit-form and API-client tests for prepopulation, path encoding, pending state, retained values, error feedback, success closure, and roster refresh.  
   **Expected:** The edit journey works for both statuses and never exposes status editing.
5. Validate OpenAPI and run complete frontend and backend quality commands.  
   **Expected:** The edit contract is complete and all checks pass without skipped tests.

#### Human Verification (Human Gate)

1. Edit the name and email of one Active and one Inactive employee and confirm each status remains unchanged.
2. Attempt to change an employee to another employee's email and assess the clarity of conflict feedback.
3. Confirm email identity changes do not create a second roster row and status cannot be edited in the form.

#### Success Criteria

- [ ] ✅ Active and Inactive employees can be edited.
- [ ] ✅ Edit preserves status.
- [ ] ✅ Email identity replacement is atomic and URL encoded.
- [ ] ✅ Invalid, missing, and duplicate updates return the specified errors.
- [ ] ✅ Successful edit refreshes one canonical roster row.
- [ ] ✅ Edit-related frontend and backend tests pass.

## PHASE-006 Status Transitions and Row Actions

#### Phase Identity

**Phase ID**: PHASE-006  
**Phase Name**: Status Transitions and Row Actions  
**FR Mapping**:
- FR-1.3 Deactivate Employee
- FR-1.4 Reactivate Employee
- FR-1.6 Row Actions

#### Goal

The system can perform only valid deactivate and reactivate transitions while retaining every employee and presenting status-appropriate row actions.

#### Dependencies

PHASE-005 Edit Employee Vertical Slice

#### Plan

1. Implement domain `deactivate` and `reactivate` operations that permit only `ACTIVE` to `INACTIVE` and `INACTIVE` to `ACTIVE`.
2. Implement transactional deactivate and reactivate use cases that locate employees by normalized email, retain records, and return not-found or invalid-transition errors.
3. Implement `POST /api/employees/{email}/deactivate` and `POST /api/employees/{email}/reactivate` with updated employee responses and canonical `404`, `409`, and `500` errors.
4. Document both action endpoints, transition rules, requirement IDs, and responses in generated OpenAPI while ensuring no DELETE operation exists.
5. Render Edit and Deactivate only for Active rows and Edit and Reactivate only for Inactive rows.
6. Apply the pending `OI-04` default by placing a simple browser confirmation before deactivation only, isolating the behavior for easy change after confirmation.
7. Encode action email paths, disable the affected action while pending, expose clear transition errors, and refresh the server-sorted roster after success.
8. Add tests for valid and invalid transitions, missing employees, retained rows, contextual actions, deactivation confirmation/cancel, pending protection, errors, and refreshed status.
9. Add static and contract assertions proving no controller, repository, SQL, OpenAPI, or UI delete path exists.

#### Outcome

- Explicit action endpoints perform guarded status transitions.
- Deactivated employees remain in the same complete roster.
- Each row presents exactly the actions allowed for its status.
- Deactivation uses the documented pending `OI-04` confirmation default.
- No permanent-delete path exists at any layer.

#### Automated Verification (AI Gate)

1. Run domain and use-case tests for both valid transitions and repeated/wrong-state transitions.  
   **Expected:** Valid transitions persist; invalid transitions return conflict without state change.
2. Run API integration tests for both action endpoints with existing, missing, and invalid-state employees.  
   **Expected:** Valid calls return `200`; missing calls return `404`; invalid transitions return `409`.
3. Query persistence after deactivation and reactivation.  
   **Expected:** The same employee row remains and only status changes.
4. Run row-action component tests for both statuses, pending requests, errors, confirmation acceptance/cancellation, and roster refresh.  
   **Expected:** Active rows show Edit/Deactivate, Inactive rows show Edit/Reactivate, and cancelled deactivation sends no request.
5. Scan routes, repository contracts, migrations, generated OpenAPI, and rendered controls for delete capability.  
   **Expected:** No permanent-delete path exists.
6. Run complete frontend and backend quality commands.  
   **Expected:** All lint, type, compile, test, and build checks pass without skipped tests.

#### Human Verification (Human Gate)

1. Deactivate and reactivate an employee and confirm the same row remains visible with the correct badge and actions.
2. Review action labels for both statuses and confirm they match the approved action matrix.
3. Exercise the deactivation confirmation and confirm `OI-04` remains documented as pending.
4. Confirm no permanent-delete affordance or hidden status editor is present.

#### Success Criteria

- [ ] ✅ Both valid status transitions succeed.
- [ ] ✅ Invalid transitions return `409` without changing data.
- [ ] ✅ Deactivated employees remain visible.
- [ ] ✅ Row actions match employee status exactly.
- [ ] ✅ No delete path exists at any layer.
- [ ] ✅ `OI-04` remains explicitly pending with its documented default.
- [ ] ✅ Status-related frontend and backend tests pass.

## PHASE-007 Local Containerization and Acceptance Closure

#### Phase Identity

**Phase ID**: PHASE-007  
**Phase Name**: Local Containerization and Acceptance Closure  
**FR Mapping**:
- FR-1.1 Create Employee
- FR-1.2 Edit Employee
- FR-1.3 Deactivate Employee
- FR-1.4 Reactivate Employee
- FR-1.5 List Employees
- FR-1.6 Row Actions

#### Goal

The complete Employee Directory runs locally as one Compose project, preserves its roster across normal restarts, and passes all automated and human acceptance checks.

#### Dependencies

PHASE-006 Status Transitions and Row Actions

#### Plan

1. Create a multi-stage frontend image that runs the Vite build and serves static assets from an unprivileged web server with `/api` reverse-proxied to the backend.
2. Create a multi-stage backend image that builds the Spring Boot artifact and runs it on a Java 21 JRE as a non-root user.
3. Create a Docker Compose topology for frontend, backend, and a pinned supported PostgreSQL image on a private network with a named data volume.
4. Configure database readiness, backend health, and frontend HTTP health checks with dependency ordering.
5. Bind the frontend for local browser access, keep PostgreSQL private by default, and make the optional local backend diagnostics port configurable.
6. Carry `OI-02` by selecting non-conflicting implementation defaults in the environment example and runtime documentation while explicitly leaving final port confirmation pending.
7. Add a non-secret `.env.example`, ignore real secrets, and document startup, shutdown without `-v`, local URLs, health inspection, backup, restore, and destructive volume-removal warnings.
8. Run Flyway automatically at backend startup and verify Hibernate validation before backend health becomes ready.
9. Add Compose smoke and acceptance automation for clean startup, health ordering, UI-to-API connectivity, all five employee operations, invalid inputs, duplicate feedback, unavailable dependency messaging, normal restart persistence, and image recreation persistence.
10. Run the complete traceability matrix against requirements, business rules, data/access constraints, OpenAPI, automated tests, and manual checks.
11. Review `OI-01` through `OI-04` with the product owner, recording only explicitly approved resolutions in the design and tests and otherwise carrying them forward as pending.
12. Verify that no authentication, authorization, role, delete, search, filter, pagination, cloud, telemetry, multi-user, concurrency, or distributed-system capability entered the release.

#### Outcome

- Frontend, backend, and PostgreSQL run locally in a health-ordered Compose project.
- A named PostgreSQL volume preserves employee data across normal restarts and container/image recreation.
- All five API operations and all six functional requirements pass acceptance verification.
- Local operation and recovery instructions are complete and warn before destructive volume removal.
- `OI-01` through `OI-04` are either explicitly approved and recorded or remain visibly pending.

#### Automated Verification (AI Gate)

1. Run frontend lint, type-check, tests, and production build plus backend compile, tests, migration validation, package, and OpenAPI checks.  
   **Expected:** Every command passes with no skipped or disabled tests.
2. Build all images and run `docker compose up --build` from a clean local state.  
   **Expected:** Database, backend, then frontend become healthy in order and the roster loads in the browser endpoint.
3. Execute API/UI acceptance automation for list, create, edit, deactivate, reactivate, validation, duplicate email, contextual actions, and excluded controls.  
   **Expected:** Every approved journey and error contract passes.
4. Create data, run `docker compose down`, restart, and then recreate application containers/images without removing the named volume.  
   **Expected:** The complete employee roster remains unchanged after both persistence checks.
5. Interrupt backend and database availability during controlled smoke checks.  
   **Expected:** Health checks fail accurately and the UI presents recoverable feedback without data loss or leaked internals.
6. Validate generated OpenAPI and static scope assertions.  
   **Expected:** Exactly the five designed employee operations exist, list has no query parameters, and no auth or DELETE contract exists.
7. Run the requirement-to-test traceability audit.  
   **Expected:** Every FR, mapped business rule, data invariant, access constraint, and applicable NFR has passing evidence.

#### Human Verification (Human Gate)

1. Follow the documented local startup instructions on the target workstation and complete all five user journeys in the browser.
2. Inspect keyboard use, status readability, duplicate feedback, refresh behavior, Swagger examples, and restart persistence.
3. Review the destructive reset warning, local-only bindings, and absence of all excluded-scope features.
4. Review `OI-01` through `OI-04` and confirm each is either formally resolved with an approved decision or still marked pending.

#### Success Criteria

- [ ] ✅ All frontend and backend quality gates pass.
- [ ] ✅ All three Compose services build and become healthy locally.
- [ ] ✅ All functional requirements and user journeys pass acceptance checks.
- [ ] ✅ Employee data survives restart and container/image recreation.
- [ ] ✅ OpenAPI exposes exactly the designed non-delete employee operations.
- [ ] ✅ Local operation, recovery, and destructive-action warnings are documented.
- [ ] ✅ Excluded scope remains absent.
- [ ] ✅ `OI-01` through `OI-04` have explicit, non-silent final states.
