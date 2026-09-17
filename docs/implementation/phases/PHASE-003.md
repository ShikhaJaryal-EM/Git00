# PHASE-003 — Employee Service — Create and List

**Spec file**: `docs/implementation/phases/PHASE-003.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-003  
**Phase Name**: Employee Service — Create and List  
**FR Mapping**:
- FR-1.1 The user can create a new employee record
- FR-1.2 Create captures first name, last name, and status
- FR-1.3 After create, the new record is visible in the list
- FR-5.1 The user can see the saved employee records
- FR-5.2 Each listed record shows first name, last name, and status

#### Goal

The application layer can create an employee with first name, last name, and status, reject invalid names without writing, and list every saved record including those fields.

#### Dependencies

PHASE-002 Employee Model and JSON File Store ([PHASE-002.md](PHASE-002.md))

#### Plan

1. Create `Services/IEmployeeService.cs` and `Services/EmployeeService.cs` with `Create` and `List`.
2. Implement `Create(firstName, lastName, status)` as specified in `docs/solution/06-behaviour-and-flows.md`: trim names; reject if either is empty after trim; default omitted status to Active; reject status other than Active or Inactive; assign id; append; `Save()`.
3. Implement `List()` to return all records in insertion order, including Inactive (DD-06, DD-13).
4. Do not add a delete/remove method.
5. Register `IEmployeeService` in DI.
6. Add xUnit tests from `docs/solution/09-test-strategy.md` for Create/List: valid create; listed after create; missing first/last rejected; whitespace-only rejected; rejected create does not add; names stored trimmed; default status Active; list returns first name, last name, status; duplicate names allowed.

#### Outcome

- `EmployeeService.Create` and `List` exist
- Invalid create does not call a successful persist of a new row
- List returns all rows with first name, last name, and status
- Service tests for create/list pass

#### Automated Verification (AI Gate)

1. Run `dotnet test --filter EmployeeService`.
2. Run tests: valid create adds a record (FR-1.1, FR-1.2, AC-1.1).
3. Run tests: created record returned by List (FR-1.3, AC-1.2).
4. Run tests: missing/blank/whitespace names rejected; list unchanged (BR-1.1, BR-1.2, BR-1.4, BR-3.1, BR-3.4, AC-1.3, AC-1.4).
5. Run tests: trim (BR-1.3); default Active (BR-2.2); List fields (FR-5.1, FR-5.2).

**Expected**:

- Create/list service tests pass
- Rejected create does not change the saved list
- Status on create is Active or Inactive only (BR-2.1)

#### Human Verification (Human Gate)

1. Review Create against FR-1.1–FR-1.3 and BR-1.1–BR-2.2, BR-3.1, BR-3.4.
2. Confirm List includes inactive rows and only the three user-visible fields (plus hidden id in the model).
3. Confirm no extra employee fields were added to the model or JSON.

#### Success Criteria

✅ Create captures first name, last name, and status  
✅ Invalid create is rejected and does not persist  
✅ List returns all saved records with first name, last name, and status  
✅ Duplicate names allowed  
✅ Create/list unit tests pass  

**Previous**: [PHASE-002](PHASE-002.md) · **Next**: [PHASE-004](PHASE-004.md)
