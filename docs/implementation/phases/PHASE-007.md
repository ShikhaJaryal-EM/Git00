# PHASE-007 — Deactivate and Reactivate UI Actions

**Spec file**: `docs/implementation/phases/PHASE-007.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-007  
**Phase Name**: Deactivate and Reactivate UI Actions  
**FR Mapping**:
- FR-3.1 The user can deactivate an active employee
- FR-3.2 After deactivate, the employee's status is inactive
- FR-4.1 The user can reactivate an inactive employee
- FR-4.2 After reactivate, the employee's status is active

#### Goal

From the list page, the user can deactivate an active employee and reactivate an inactive employee; the row stays in the list with the updated status.

#### Dependencies

PHASE-006 Create and Edit Pages UI ([PHASE-006.md](PHASE-006.md))

#### Plan

1. On Index, for each Active row show a Deactivate button that POSTs to a page handler calling `EmployeeService.Deactivate(id)` (DD-07, DD-09).
2. For each Inactive row show a Reactivate button that POSTs to a handler calling `EmployeeService.Reactivate(id)`.
3. Do not show Deactivate on inactive rows or Reactivate on active rows (BR-2.3, BR-2.4).
4. After a successful POST, stay on or redirect to the list; the same employee remains with the new status.
5. Do not add a Toggle control, delete control, or status field on Edit.
6. Guard handlers: if the service rejects the wrong status action, do not persist; redisplay the list.

#### Outcome

- Deactivate is available only for Active rows
- Reactivate is available only for Inactive rows
- After deactivate, status is Inactive and the row remains
- After reactivate, status is Active and the row remains
- All four UI actions exist: create, edit, deactivate, reactivate (NFR-3.2)

#### Automated Verification (AI Gate)

1. Run `dotnet test` (service guards).
2. Inspect `Index.cshtml` for separate Deactivate and Reactivate labels (not “Toggle”).
3. Inspect handlers for POST `Deactivate` and `Reactivate` calling the service by id.
4. Assert no delete/remove handler exists.

**Expected**:

- Markup and handlers match DD-09
- Tests still pass
- No delete action

#### Human Verification (Human Gate)

1. Execute MAT-05 (deactivate; row remains Inactive).
2. Execute MAT-06 (reactivate; row remains Active).
3. Execute MAT-09 (no delete in the UI).
4. Confirm NFR-3.2: create, edit, deactivate, and reactivate are completable in the UI (AC-6.3).

#### Success Criteria

✅ Deactivate works for active employees  
✅ Reactivate works for inactive employees  
✅ Wrong action is not offered in the UI  
✅ Employee remains in the list after status change  
✅ No delete control  

**Previous**: [PHASE-006](PHASE-006.md) · **Next**: [PHASE-008](PHASE-008.md)
