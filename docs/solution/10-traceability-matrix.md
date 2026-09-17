# Traceability Matrix

## Purpose

Map each authored requirement ID to the solution section that designs it and the test that verifies it.

## Requirement references

All IDs in `docs/requirements`.

---

## Business objectives

| ID | Requirement | Solution docs | Tests |
| --- | --- | --- | --- |
| BO-1 | Capture first name, last name, status | 01, 04, 05 | MAT-01, MAT-07 |
| BO-2 | Create, edit, deactivate, reactivate locally | 01, 05, 06 | MAT-01, MAT-03, MAT-05, MAT-06, MAT-08 |
| BO-3 | List available after close and reopen | 01, 02, 04, 07 | MAT-01 step 4, MAT-03, MAT-05, MAT-06 |

## Functional requirements

| ID | Requirement | Solution docs | Tests |
| --- | --- | --- | --- |
| FR-1.1 | User can create a new employee | 05, 06 | MAT-01; unit Create |
| FR-1.2 | Create captures first name, last name, status | 04, 05 | MAT-01; unit Create |
| FR-1.3 | After create, record is visible in the list | 05, 06 | MAT-01; unit List after Create |
| FR-2.1 | User can edit an existing employee | 05, 06 | MAT-03 |
| FR-2.2 | Edit can change first name and last name | 05, 06 | MAT-03, MAT-10 |
| FR-2.3 | After edit, updated values are visible | 05, 06 | MAT-03 |
| FR-3.1 | User can deactivate an active employee | 05, 06 | MAT-05 |
| FR-3.2 | After deactivate, status is inactive | 05, 06 | MAT-05 |
| FR-4.1 | User can reactivate an inactive employee | 05, 06 | MAT-06 |
| FR-4.2 | After reactivate, status is active | 05, 06 | MAT-06 |
| FR-5.1 | User can see saved employee records | 05, 06 | MAT-07 |
| FR-5.2 | Each listed record shows first name, last name, status | 05 | MAT-07 |

## Business rules

| ID | Rule | Solution docs | Tests |
| --- | --- | --- | --- |
| BR-1.1 | First name required | 04, 06, 08 | MAT-02; unit reject create |
| BR-1.2 | Last name required | 04, 06, 08 | MAT-02; unit reject create |
| BR-1.3 | Names stored trimmed | 04, 06 | unit trim |
| BR-1.4 | Name of only spaces is not valid | 06, 08 | MAT-02; unit whitespace |
| BR-2.1 | Status is active or inactive | 04, 05 | unit status; MAT-01 |
| BR-2.2 | New employee active unless user chooses inactive | 05, 06 | unit default; MAT-01 |
| BR-2.3 | Deactivate only when active | 05, 06 | unit guard; MAT-05 |
| BR-2.4 | Reactivate only when inactive | 05, 06 | unit guard; MAT-06 |
| BR-3.1 | Create rejected if name missing or invalid | 06, 08 | MAT-02 |
| BR-3.2 | Edit rejected if name missing or invalid | 06, 08 | MAT-04 |
| BR-3.3 | User is told when create or edit is rejected | 05, 08 | MAT-02, MAT-04 |
| BR-3.4 | Rejected create or edit does not change the saved list | 06, 08 | MAT-02, MAT-04; unit |

## Non-functional requirements

| ID | Requirement | Solution docs | Tests |
| --- | --- | --- | --- |
| NFR-1.1 | Runs on a local machine | 02, 03, 07 | MAT-08 |
| NFR-1.2 | No network required | 02, 03, 07, 11 (DD-14) | MAT-08 |
| NFR-1.3 | No cloud or outside service | 02, 03, 07 | MAT-08 |
| NFR-2.1 | Present after close and reopen | 04, 07 | MAT-01, MAT-03, MAT-05, MAT-06 |
| NFR-2.2 | Persistence without network or cloud | 04, 07 — local JSON file, no database | MAT-08 plus file on disk |
| NFR-2.3 | Normal restart does not lose records | 04, 07 | same as NFR-2.1 |
| NFR-3.1 | Basic working interface | 05, 07 | MAT-01–MAT-07 |
| NFR-3.2 | Create, edit, deactivate, reactivate from UI | 05, 07 | MAT-08 |
| NFR-3.3 | Visual polish not required | 05, 07, 11 (DD-15) | Design review: default Bootstrap only |
| NFR-4.1 | Rejected save does not corrupt the list | 08 | MAT-02, MAT-04 |
| NFR-4.2 | Usable after rejected save | 08 | MAT-02, MAT-04 |
| NFR-4.3 | Usable after close and reopen | 07, 08 | MAT-01 close/reopen |

## Acceptance criteria

| ID | Criterion | Solution docs | Tests |
| --- | --- | --- | --- |
| AC-1.1 | Create with first name, last name, status | 05, 06 | MAT-01 |
| AC-1.2 | New employee appears in the list | 05, 06 | MAT-01 |
| AC-1.3 | Create with missing or blank name rejected | 06, 08 | MAT-02 |
| AC-1.4 | Rejected create does not add a record | 06, 08 | MAT-02 |
| AC-2.1 | Change first or last name | 05, 06 | MAT-03 |
| AC-2.2 | List shows updated name | 05, 06 | MAT-03 |
| AC-2.3 | Edit with missing or blank name rejected | 06, 08 | MAT-04 |
| AC-2.4 | Rejected edit leaves saved name unchanged | 06, 08 | MAT-04 |
| AC-3.1 | Deactivate an active employee | 05, 06 | MAT-05 |
| AC-3.2 | Status is inactive | 05, 06 | MAT-05 |
| AC-3.3 | Employee remains in the list | 05, 06 | MAT-05 |
| AC-4.1 | Reactivate an inactive employee | 05, 06 | MAT-06 |
| AC-4.2 | Status is active | 05, 06 | MAT-06 |
| AC-4.3 | Employee remains in the list | 05, 06 | MAT-06 |
| AC-5.1 | Created records persist | 04, 07 | MAT-01 |
| AC-5.2 | Edited names persist | 04, 07 | MAT-03 |
| AC-5.3 | Status changes persist | 04, 07 | MAT-05, MAT-06 |
| AC-6.1 | Usable with no network | 07 | MAT-08 |
| AC-6.2 | No cloud or outside service | 07 | MAT-08 |
| AC-6.3 | All four actions completable in the UI | 05 | MAT-08 |

## User journeys

| Journey | Solution docs | Tests |
| --- | --- | --- |
| Create employee | 06 | MAT-01, MAT-02 |
| Edit employee | 06 | MAT-03, MAT-04 |
| Deactivate employee | 06 | MAT-05 |
| Reactivate employee | 06 | MAT-06 |

## Open questions (from requirements)

| Question | Resolution | Decision | Docs |
| --- | --- | --- | --- |
| Must first name plus last name be unique? | No | DD-12 | requirements `10`, solution `11` |
| Should the list show inactive employees by default? | Yes, always | DD-13 | requirements `10`, `05` note, solution `11` |
| Preferred list order? | Insertion order; no sort UI | DD-06 | requirements `10`, solution `11` |
| Status editable on create only, or also during edit? | Create can set status; later changes use deactivate/reactivate | DD-08 plus FR-2.2 | requirements `10`, `05` note, solution `11` |

## Coverage

| Category | Authored items | Mapped in this package |
| --- | --- | --- |
| Business objectives | BO-1–BO-3 | 3 |
| Functional requirements | FR-1.1–FR-5.2 | 12 |
| Business rules | BR-1.1–BR-3.4 | 12 |
| Non-functional requirements | NFR-1.1–NFR-4.3 | 12 |
| Acceptance criteria | AC-1.1–AC-6.3 | 20 |
| Open questions | 4 | 4 resolved |

Every authored FR, BR, NFR, and AC is mapped. Persistence is a **JSON file**, not a database. The HTML/CSS/Bootstrap UI and localhost host are implementation choices (DD-03, DD-14, DD-15), not extra product features.
