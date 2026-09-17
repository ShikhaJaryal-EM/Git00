# Implementation Traceability — Employee Records Tool

## Purpose

Map each authored requirement ID to the implementation phase that delivers it and the test that verifies it. Requirement text is unchanged from `docs/requirements`. Solution mapping remains in `docs/solution/10-traceability-matrix.md`; this file adds **phase** coverage for the implementation package.

## Coverage rules

- Every FR appears in at least one non-foundational phase.
- An FR may appear in more than one phase when delivery is incremental (service then UI, then persistence proof).
- Foundational phases (PHASE-001, PHASE-002) have no FR mapping; they enable NFRs and the store.

---

## Business objectives

| ID | Requirement | Phases | Tests |
| --- | --- | --- | --- |
| BO-1 | Capture first name, last name, status | PHASE-003, PHASE-005, PHASE-006 | Create/List unit; MAT-01, MAT-07 |
| BO-2 | Create, edit, deactivate, reactivate locally | PHASE-003, PHASE-004, PHASE-006, PHASE-007 | Service unit; MAT-01, MAT-03, MAT-05, MAT-06, MAT-08 |
| BO-3 | List available after close and reopen | PHASE-002, PHASE-008 | JsonFileStore round-trip; MAT-01 step 4, MAT-03, MAT-05, MAT-06 |

---

## Functional requirements

| ID | Requirement | Phases | Tests |
| --- | --- | --- | --- |
| FR-1.1 | The user can create a new employee record | PHASE-003, PHASE-006 | unit Create; MAT-01 |
| FR-1.2 | Create captures first name, last name, and status | PHASE-003, PHASE-006 | unit Create; MAT-01 |
| FR-1.3 | After create, the new record is visible in the list | PHASE-003, PHASE-006, PHASE-008 | unit List after Create; MAT-01 |
| FR-2.1 | The user can edit an existing employee | PHASE-004, PHASE-006 | unit UpdateNames; MAT-03 |
| FR-2.2 | Edit can change first name and last name | PHASE-004, PHASE-006 | unit UpdateNames; MAT-03, MAT-10 |
| FR-2.3 | After edit, the updated values are visible in the list | PHASE-004, PHASE-006, PHASE-008 | unit List after UpdateNames; MAT-03 |
| FR-3.1 | The user can deactivate an active employee | PHASE-004, PHASE-007 | unit Deactivate; MAT-05 |
| FR-3.2 | After deactivate, the employee's status is inactive | PHASE-004, PHASE-007, PHASE-008 | unit Deactivate; MAT-05 |
| FR-4.1 | The user can reactivate an inactive employee | PHASE-004, PHASE-007 | unit Reactivate; MAT-06 |
| FR-4.2 | After reactivate, the employee's status is active | PHASE-004, PHASE-007, PHASE-008 | unit Reactivate; MAT-06 |
| FR-5.1 | The user can see the saved employee records | PHASE-003, PHASE-005, PHASE-008 | unit List; MAT-07 |
| FR-5.2 | Each listed record shows first name, last name, and status | PHASE-003, PHASE-005 | unit List; MAT-07 |

**FR coverage:** 12 of 12 (FR-1.1–FR-5.2).

---

## Business rules

| ID | Rule | Phases | Tests |
| --- | --- | --- | --- |
| BR-1.1 | First name is required | PHASE-003, PHASE-006 | unit reject create; MAT-02 |
| BR-1.2 | Last name is required | PHASE-003, PHASE-006 | unit reject create; MAT-02 |
| BR-1.3 | Names stored trimmed | PHASE-003 | unit trim |
| BR-1.4 | A name that is only spaces is not valid | PHASE-003, PHASE-006 | unit whitespace; MAT-02 |
| BR-2.1 | Status is either active or inactive | PHASE-002, PHASE-003 | unit status; MAT-01 |
| BR-2.2 | A new employee is active unless the user chooses inactive | PHASE-003, PHASE-006 | unit default; MAT-01 |
| BR-2.3 | Deactivate is allowed only when the employee is active | PHASE-004, PHASE-007 | unit guard; MAT-05 |
| BR-2.4 | Reactivate is allowed only when the employee is inactive | PHASE-004, PHASE-007 | unit guard; MAT-06 |
| BR-3.1 | Create is rejected if first name or last name is missing or invalid | PHASE-003, PHASE-006 | MAT-02 |
| BR-3.2 | Edit is rejected if first name or last name is missing or invalid | PHASE-004, PHASE-006 | MAT-04 |
| BR-3.3 | The user is told when a create or edit is rejected | PHASE-006 | MAT-02, MAT-04 |
| BR-3.4 | A rejected create or edit does not change the saved list | PHASE-003, PHASE-004, PHASE-006, PHASE-008 | MAT-02, MAT-04; unit |

**BR coverage:** 12 of 12 (BR-1.1–BR-3.4).

---

## Non-functional requirements

| ID | Requirement | Phases | Tests |
| --- | --- | --- | --- |
| NFR-1.1 | The tool runs on a local machine | PHASE-001, PHASE-008 | MAT-08 |
| NFR-1.2 | The tool does not require a network connection | PHASE-001, PHASE-008 | MAT-08 |
| NFR-1.3 | The tool does not require a cloud or other outside service | PHASE-001, PHASE-008 | MAT-08 |
| NFR-2.1 | Saved records are still present after a normal close and reopen | PHASE-002, PHASE-008 | MAT-01, MAT-03, MAT-05, MAT-06; store round-trip |
| NFR-2.2 | Persistence works without a network or cloud service | PHASE-002, PHASE-008 | MAT-08 plus file on disk |
| NFR-2.3 | A normal restart does not lose saved records | PHASE-002, PHASE-008 | same as NFR-2.1 |
| NFR-3.1 | A basic working interface is required | PHASE-005, PHASE-006, PHASE-007 | MAT-01–MAT-07 |
| NFR-3.2 | The user can complete create, edit, deactivate, and reactivate from the UI | PHASE-006, PHASE-007, PHASE-008 | MAT-08 |
| NFR-3.3 | Visual polish is not required | PHASE-001, PHASE-005, PHASE-008 | Human review: default Bootstrap only |
| NFR-4.1 | A rejected create or edit does not corrupt the saved list | PHASE-003, PHASE-004, PHASE-006, PHASE-008 | MAT-02, MAT-04 |
| NFR-4.2 | The tool remains usable after a rejected save | PHASE-006, PHASE-008 | MAT-02, MAT-04 |
| NFR-4.3 | The tool remains usable after a normal close and reopen | PHASE-008 | MAT-01 close/reopen |

**NFR coverage:** 12 of 12 (NFR-1.1–NFR-4.3).

---

## Acceptance criteria

| ID | Criterion | Phases | Tests |
| --- | --- | --- | --- |
| AC-1.1 | A user can create an employee with first name, last name, and status | PHASE-003, PHASE-006 | MAT-01 |
| AC-1.2 | The new employee appears in the list | PHASE-003, PHASE-006 | MAT-01 |
| AC-1.3 | Create with a missing or blank name is rejected | PHASE-003, PHASE-006 | MAT-02 |
| AC-1.4 | A rejected create does not add a record | PHASE-003, PHASE-006 | MAT-02 |
| AC-2.1 | A user can change an existing employee's first or last name | PHASE-004, PHASE-006 | MAT-03 |
| AC-2.2 | The list shows the updated name | PHASE-004, PHASE-006 | MAT-03 |
| AC-2.3 | Edit with a missing or blank name is rejected | PHASE-004, PHASE-006 | MAT-04 |
| AC-2.4 | A rejected edit leaves the saved name unchanged | PHASE-004, PHASE-006 | MAT-04 |
| AC-3.1 | A user can deactivate an active employee | PHASE-004, PHASE-007 | MAT-05 |
| AC-3.2 | After deactivate, status is inactive | PHASE-004, PHASE-007 | MAT-05 |
| AC-3.3 | The employee remains in the list | PHASE-004, PHASE-007 | MAT-05 |
| AC-4.1 | A user can reactivate an inactive employee | PHASE-004, PHASE-007 | MAT-06 |
| AC-4.2 | After reactivate, status is active | PHASE-004, PHASE-007 | MAT-06 |
| AC-4.3 | The employee remains in the list | PHASE-004, PHASE-007 | MAT-06 |
| AC-5.1 | After a normal close and reopen, created records are still present | PHASE-008 | MAT-01 |
| AC-5.2 | After a normal close and reopen, edited names are still present | PHASE-008 | MAT-03 |
| AC-5.3 | After a normal close and reopen, status changes are still present | PHASE-008 | MAT-05, MAT-06 |
| AC-6.1 | The tool can be used with no network connection | PHASE-001, PHASE-008 | MAT-08 |
| AC-6.2 | The tool does not require a cloud or other outside service | PHASE-001, PHASE-008 | MAT-08 |
| AC-6.3 | Create, edit, deactivate, and reactivate can be completed in the UI | PHASE-006, PHASE-007, PHASE-008 | MAT-08 |

**AC coverage:** 20 of 20 (AC-1.1–AC-6.3).

---

## User journeys

| Journey | Phases | Tests |
| --- | --- | --- |
| Create employee | PHASE-003, PHASE-006, PHASE-008 | MAT-01, MAT-02 |
| Edit employee | PHASE-004, PHASE-006, PHASE-008 | MAT-03, MAT-04 |
| Deactivate employee | PHASE-004, PHASE-007, PHASE-008 | MAT-05 |
| Reactivate employee | PHASE-004, PHASE-007, PHASE-008 | MAT-06 |

---

## Phase → FR summary

Must match `implementation-plan.md` and each `phases/PHASE-00N.md` Phase Identity.

| Phase ID | FR Mapping | Spec |
| --- | --- | --- |
| PHASE-001 | None (foundational) | [phases/PHASE-001.md](phases/PHASE-001.md) |
| PHASE-002 | None (foundational) | [phases/PHASE-002.md](phases/PHASE-002.md) |
| PHASE-003 | FR-1.1, FR-1.2, FR-1.3, FR-5.1, FR-5.2 | [phases/PHASE-003.md](phases/PHASE-003.md) |
| PHASE-004 | FR-2.1, FR-2.2, FR-2.3, FR-3.1, FR-3.2, FR-4.1, FR-4.2 | [phases/PHASE-004.md](phases/PHASE-004.md) |
| PHASE-005 | FR-5.1, FR-5.2 | [phases/PHASE-005.md](phases/PHASE-005.md) |
| PHASE-006 | FR-1.1, FR-1.2, FR-1.3, FR-2.1, FR-2.2, FR-2.3 | [phases/PHASE-006.md](phases/PHASE-006.md) |
| PHASE-007 | FR-3.1, FR-3.2, FR-4.1, FR-4.2 | [phases/PHASE-007.md](phases/PHASE-007.md) |
| PHASE-008 | FR-1.3, FR-2.3, FR-3.2, FR-4.2, FR-5.1 | [phases/PHASE-008.md](phases/PHASE-008.md) |

---

## Design decisions affecting implementation

| ID | Decision | Phases that must honour it |
| --- | --- | --- |
| DD-01 | Three layers in one local ASP.NET Core process | PHASE-001–PHASE-007 |
| DD-02 | JSON file under `%AppData%\EmployeeRecordsTool\` | PHASE-002, PHASE-008 |
| DD-03 | HTML/CSS/Bootstrap 5 local; Razor Pages; JSON | PHASE-001, PHASE-005–PHASE-007 |
| DD-04 | Hidden GUID id | PHASE-002–PHASE-007 |
| DD-05 | Full-file rewrite | PHASE-002 |
| DD-06 | Insertion order; no sort UI | PHASE-003, PHASE-005 |
| DD-07 | Visible Edit / Deactivate / Reactivate | PHASE-006, PHASE-007 |
| DD-08 | Create status default Active | PHASE-003, PHASE-006 |
| DD-09 | Separate Deactivate and Reactivate labels | PHASE-007 |
| DD-11 | Missing file empty; corrupt file kept | PHASE-002, PHASE-008 |
| DD-12 | Names not unique | PHASE-002, PHASE-003 |
| DD-13 | Inactive always listed | PHASE-003, PHASE-005, PHASE-007 |
| DD-14 | `127.0.0.1` allowed; no internet/CDN/cloud | PHASE-001, PHASE-008 |
| DD-15 | Default Bootstrap only | PHASE-001, PHASE-005, PHASE-008 |

---

## Gaps

None. Every authored FR, BR, NFR, and AC is mapped to at least one phase and a test from `docs/solution/09-test-strategy.md`.
