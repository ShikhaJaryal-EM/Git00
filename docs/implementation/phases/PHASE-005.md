# PHASE-005 — List Page UI

**Spec file**: `docs/implementation/phases/PHASE-005.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-005  
**Phase Name**: List Page UI  
**FR Mapping**:
- FR-5.1 The user can see the saved employee records
- FR-5.2 Each listed record shows first name, last name, and status

#### Goal

The main list page shows every saved employee in a Bootstrap table with first name, last name, and status, including inactive rows, loaded from the JSON store.

#### Dependencies

PHASE-004 Employee Service — Edit, Deactivate, Reactivate ([PHASE-004.md](PHASE-004.md))

#### Plan

1. Implement `Pages/Index.cshtml` and `Index.cshtml.cs` to call `EmployeeService.List()` on GET.
2. Render `table.table` with columns First name, Last name, Status (`docs/solution/05-ui-design.md`).
3. Show Active and Inactive rows (DD-13). Use insertion order (DD-06). Do not add search, filter, sort, or delete controls.
4. Do not display the system id.
5. Add a Create employee link/button (`a.btn`) pointing to `/Create` (page may be a stub until PHASE-006).
6. Leave placeholders for Edit / Deactivate / Reactivate if needed so PHASE-006/007 can fill them; do not wire status POSTs yet if handlers are absent — prefer empty Actions column until PHASE-007, with Create link present.
7. Handle Load failure with ERR-04 message; do not overwrite a corrupt file.
8. Add a unit or page-model test that List results are passed to the page model (optional); rely on `dotnet test` remaining green.

#### Outcome

- Index page displays first name, last name, and status for all saved records
- Empty list is a valid first-run state
- No delete control
- Id is not shown

#### Automated Verification (AI Gate)

1. Run `dotnet test`.
2. Render or inspect `Index.cshtml` for column headers First name, Last name, Status.
3. Assert markup has no delete/remove button and no id column.
4. Assert `_Layout` / Index reference `/lib/bootstrap/` not a CDN.

**Expected**:

- Tests still pass
- List markup matches FR-5.2
- No delete and no CDN

#### Human Verification (Human Gate)

1. With sample JSON (or after creating via a temporary test file), open the list and confirm mixed Active/Inactive rows all appear (MAT-07).
2. Confirm the page is a basic Bootstrap table (NFR-3.1, NFR-3.3).
3. Confirm no login screen is required to see the list.

#### Success Criteria

✅ List page loads from `EmployeeService.List()`  
✅ Columns are first name, last name, status  
✅ Inactive rows are visible  
✅ No delete, search, sort, or id column  
✅ Bootstrap is local  

**Previous**: [PHASE-004](PHASE-004.md) · **Next**: [PHASE-006](PHASE-006.md)
