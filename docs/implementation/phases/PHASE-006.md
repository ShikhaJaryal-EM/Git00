# PHASE-006 — Create and Edit Pages UI

**Spec file**: `docs/implementation/phases/PHASE-006.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-006  
**Phase Name**: Create and Edit Pages UI  
**FR Mapping**:
- FR-1.1 The user can create a new employee record
- FR-1.2 Create captures first name, last name, and status
- FR-1.3 After create, the new record is visible in the list
- FR-2.1 The user can edit an existing employee
- FR-2.2 Edit can change first name and last name
- FR-2.3 After edit, the updated values are visible in the list

#### Goal

The user can complete create and edit from HTML forms: create captures first name, last name, and status; edit changes names only; accepted saves return to a list that shows the new or updated row; rejected saves show a message and leave the saved list unchanged.

#### Dependencies

PHASE-005 List Page UI ([PHASE-005.md](PHASE-005.md))

#### Plan

1. Implement `Pages/Create.cshtml` / `Create.cshtml.cs`: inputs for first name and last name; `select` for status defaulting to Active (DD-08); Save POST; Cancel link to list.
2. On valid Save, call `EmployeeService.Create` and redirect to Index.
3. On invalid Save, stay on Create, show ERR-01 and/or ERR-02 (`text-danger` or `alert alert-danger`), do not persist.
4. Implement `Pages/Edit.cshtml` / `Edit.cshtml.cs`: pre-fill names; **no status field**; Save calls `UpdateNames`; Cancel returns to list.
5. Add Edit link per row on Index (`a.btn btn-secondary`).
6. On unknown id, show ERR-06 and do not persist.
7. Do not add status to the edit form (MAT-10 / FR-2.2).

#### Outcome

- Create form captures first name, last name, and status
- Successful create appears on the list
- Edit form changes names only
- Successful edit appears on the list
- Rejected create/edit shows a plain message and does not change the saved list

#### Automated Verification (AI Gate)

1. Run `dotnet test` (service validation still covers BR-3.1, BR-3.2, BR-3.4).
2. Inspect Create page: three fields — first name, last name, status.
3. Inspect Edit page: first name and last name only; no status control.
4. Inspect Create/Edit handlers: validation failure does not call `Save` on the store (or service returns rejection before persist).

**Expected**:

- Create includes status; Edit does not
- Handler tests or service tests still prove rejected save does not add/change records
- Redirect to list after success is implemented

#### Human Verification (Human Gate)

1. Execute MAT-01 (create success, including names and status).
2. Execute MAT-02 (rejected create: message shown, list unchanged).
3. Execute MAT-03 (edit success) and MAT-04 (rejected edit).
4. Execute MAT-10 (edit does not change status).
5. Confirm messages are plain language (“First name is required.”) not stack traces (BR-3.3).

#### Success Criteria

✅ Create form implements FR-1.1–FR-1.3  
✅ Edit form implements FR-2.1–FR-2.3  
✅ Rejected create/edit tells the user and does not change the list  
✅ Edit has no status field  
✅ Cancel writes nothing  

**Previous**: [PHASE-005](PHASE-005.md) · **Next**: [PHASE-007](PHASE-007.md)
