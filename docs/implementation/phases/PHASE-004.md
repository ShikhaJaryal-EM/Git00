# PHASE-004 — Employee Service — Edit, Deactivate, Reactivate

**Spec file**: `docs/implementation/phases/PHASE-004.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-004  
**Phase Name**: Employee Service — Edit, Deactivate, Reactivate  
**FR Mapping**:
- FR-2.1 The user can edit an existing employee
- FR-2.2 Edit can change first name and last name
- FR-2.3 After edit, the updated values are visible in the list
- FR-3.1 The user can deactivate an active employee
- FR-3.2 After deactivate, the employee's status is inactive
- FR-4.1 The user can reactivate an inactive employee
- FR-4.2 After reactivate, the employee's status is active

#### Goal

The application layer can change names without changing status, deactivate only active employees, and reactivate only inactive employees, persisting only accepted changes.

#### Dependencies

PHASE-003 Employee Service — Create and List ([PHASE-003.md](PHASE-003.md))

#### Plan

1. Add `UpdateNames(id, firstName, lastName)`: trim; reject empty names; reject unknown id; update names only; `Save()` (`docs/solution/06-behaviour-and-flows.md`).
2. Add `Deactivate(id)`: reject if not Active; set Inactive; `Save()`.
3. Add `Reactivate(id)`: reject if not Inactive; set Active; `Save()`.
4. Keep List/Create behaviour unchanged.
5. Add xUnit tests: UpdateNames changes names only; blank name rejected and stored name unchanged; Deactivate sets Inactive and keeps the record; Deactivate on inactive rejected; Reactivate sets Active and keeps the record; Reactivate on active rejected; no delete method.

#### Outcome

- `UpdateNames`, `Deactivate`, and `Reactivate` exist on `EmployeeService`
- Edit never writes a status change
- Status guards match BR-2.3 and BR-2.4
- Rejected edit does not change the saved list

#### Automated Verification (AI Gate)

1. Run `dotnet test --filter EmployeeService`.
2. Run UpdateNames tests (FR-2.1, FR-2.2, FR-2.3, AC-2.1–AC-2.4, BR-3.2, BR-3.4).
3. Run Deactivate tests (FR-3.1, FR-3.2, AC-3.1–AC-3.3, BR-2.3).
4. Run Reactivate tests (FR-4.1, FR-4.2, AC-4.1–AC-4.3, BR-2.4).
5. Assert the service type has no public delete/remove method.

**Expected**:

- All new service tests pass
- After UpdateNames, List shows new names and the same status
- After Deactivate/Reactivate, the record remains in List with the new status

#### Human Verification (Human Gate)

1. Confirm edit is names-only (FR-2.2) and does not duplicate deactivate/reactivate.
2. Confirm deactivate/reactivate are separate operations, not a generic toggle (DD-09 at service level).
3. Confirm rejected edit leaves the file/list unchanged (BR-3.4, NFR-4.1).

#### Success Criteria

✅ UpdateNames changes first and last name only  
✅ Deactivate sets Inactive and keeps the record  
✅ Reactivate sets Active and keeps the record  
✅ Status guards reject the wrong action  
✅ Service tests pass  

**Previous**: [PHASE-003](PHASE-003.md) · **Next**: [PHASE-005](PHASE-005.md)
