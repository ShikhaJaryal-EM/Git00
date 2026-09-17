# Implementation Plan — Employee Records Tool

**Status**: Ready for execution  
**Shape**: [CANONICAL-PHASE-SHAPE.md](CANONICAL-PHASE-SHAPE.md)  
**Sources**: `docs/requirements` (truth), `docs/solution` (design)

Do not add login, roles, cloud, CDN, extra employee fields, search, export, custom branding, a database, or permanent delete.

This file is the **phase index only**. Open one spec file at a time under `phases/`.

## Keep in sync

When you change a phase ID, name, or FR mapping, update **all** of:

1. This index
2. `phases/PHASE-00N.md` Phase Identity
3. `CANONICAL-PHASE-SHAPE.md` applied Phase Index
4. `traceability.md` Phase → FR summary (and FR rows if mapping changed)
5. `phase-status.md` name column

---

## Phase Index

| Phase ID | Phase Name | FR Mapping | Spec |
| --- | --- | --- | --- |
| PHASE-001 | Local Host and Project Foundation | None (foundational) | [phases/PHASE-001.md](phases/PHASE-001.md) |
| PHASE-002 | Employee Model and JSON File Store | None (foundational) | [phases/PHASE-002.md](phases/PHASE-002.md) |
| PHASE-003 | Employee Service — Create and List | FR-1.1 The user can create a new employee record; FR-1.2 Create captures first name, last name, and status; FR-1.3 After create, the new record is visible in the list; FR-5.1 The user can see the saved employee records; FR-5.2 Each listed record shows first name, last name, and status | [phases/PHASE-003.md](phases/PHASE-003.md) |
| PHASE-004 | Employee Service — Edit, Deactivate, Reactivate | FR-2.1 The user can edit an existing employee; FR-2.2 Edit can change first name and last name; FR-2.3 After edit, the updated values are visible in the list; FR-3.1 The user can deactivate an active employee; FR-3.2 After deactivate, the employee's status is inactive; FR-4.1 The user can reactivate an inactive employee; FR-4.2 After reactivate, the employee's status is active | [phases/PHASE-004.md](phases/PHASE-004.md) |
| PHASE-005 | List Page UI | FR-5.1 The user can see the saved employee records; FR-5.2 Each listed record shows first name, last name, and status | [phases/PHASE-005.md](phases/PHASE-005.md) |
| PHASE-006 | Create and Edit Pages UI | FR-1.1 The user can create a new employee record; FR-1.2 Create captures first name, last name, and status; FR-1.3 After create, the new record is visible in the list; FR-2.1 The user can edit an existing employee; FR-2.2 Edit can change first name and last name; FR-2.3 After edit, the updated values are visible in the list | [phases/PHASE-006.md](phases/PHASE-006.md) |
| PHASE-007 | Deactivate and Reactivate UI Actions | FR-3.1 The user can deactivate an active employee; FR-3.2 After deactivate, the employee's status is inactive; FR-4.1 The user can reactivate an inactive employee; FR-4.2 After reactivate, the employee's status is active | [phases/PHASE-007.md](phases/PHASE-007.md) |
| PHASE-008 | Persistence, Local-Only Operation, and Reliability | FR-1.3 After create, the new record is visible in the list; FR-2.3 After edit, the updated values are visible in the list; FR-3.2 After deactivate, the employee's status is inactive; FR-4.2 After reactivate, the employee's status is active; FR-5.1 The user can see the saved employee records | [phases/PHASE-008.md](phases/PHASE-008.md) |

**Bug-fix phases**: none yet. Add `PHASE-BUG-nnn` here and as `phases/PHASE-BUG-nnn.md` only after approval (`Student Resource Files/PHASE-EXECUTION-GUIDE.md`).

**Dependency graph**

```
PHASE-001
    └── PHASE-002
            └── PHASE-003
                    └── PHASE-004
                            └── PHASE-005
                                    └── PHASE-006
                                            └── PHASE-007
                                                    └── PHASE-008
```

PHASE-008 depends on PHASE-007 and therefore on PHASE-001 through PHASE-007.

## Package files

| File | Role |
| --- | --- |
| [CANONICAL-PHASE-SHAPE.md](CANONICAL-PHASE-SHAPE.md) | Mandatory phase structure |
| [phases/PHASE-001.md](phases/PHASE-001.md) … [PHASE-008.md](phases/PHASE-008.md) | Detailed specs (one phase per file) |
| [traceability.md](traceability.md) | Requirement → phase → test |
| [phase-status.md](phase-status.md) | Execution tracking |
| [bugs-and-issues-log.md](bugs-and-issues-log.md) | Defects (do not fix at log time) |

## Out of scope (do not implement in any phase)

- Extra employee fields (email, phone, title, photo)
- Login, roles, permissions
- Cloud, CDN, external APIs, hosted URLs
- Database servers (SQL Server, SQLite as a product dependency, and similar)
- Search, filters, export, reporting
- Permanent delete
- Custom visual design polish (branding, themes)

## Execution

Follow `Student Resource Files/PHASE-EXECUTION-GUIDE.md`. Read **only** the current `phases/PHASE-00N.md` for that chunk. Educate, wait for go/no-go, branch `feature/{phase-id-short-name}`, implement, AI gate, human gate, ask about bugs, then wait for approval before commit.
