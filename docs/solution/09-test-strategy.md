# Test Strategy

## Purpose

Define automated and manual checks that prove the solution meets functional requirements, business rules, NFRs, and acceptance criteria. Requirements do not include a separate test-case catalogue, so tests are derived from FRs, BRs, ACs, and NFRs.

## Requirement references

- FR-1.1–FR-5.2
- BR-1.1–BR-3.4
- NFR-1.1–NFR-4.3
- AC-1.1–AC-6.3

---

## Approach

| Level | Scope | How |
| --- | --- | --- |
| Unit | Validation, trim, default status, deactivate/reactivate guards, no-delete | xUnit |
| Unit | Load, save, missing file, corrupt file, round-trip | xUnit with a temp folder |
| Manual | Full UI journeys and persistence after close/reopen | Human gate |
| Manual | Offline / no-cloud | Human gate |

No tests against external services. None exist.

## Unit tests — EmployeeService

| Test | Proves | Refs |
| --- | --- | --- |
| Create with valid names and status adds a record | Create works | FR-1.1, FR-1.2, AC-1.1 |
| Created record is returned in List | Visible after create | FR-1.3, AC-1.2 |
| Create without first or last name is rejected | Required names | BR-1.1, BR-1.2, BR-3.1, AC-1.3 |
| Create with whitespace-only name is rejected | Spaces-only invalid | BR-1.4 |
| Rejected create does not add a record | List unchanged | BR-3.4, AC-1.4, NFR-4.1 |
| Names are stored trimmed | Trim rule | BR-1.3 |
| Create without chosen status is Active | Default | BR-2.2 |
| UpdateNames changes names only | Edit | FR-2.1, FR-2.2, AC-2.1 |
| UpdateNames with blank name is rejected; stored name unchanged | Edit reject | BR-3.2, BR-3.4, AC-2.3, AC-2.4 |
| Deactivate sets Inactive and keeps the record | Deactivate | FR-3.1, FR-3.2, AC-3.1–AC-3.3 |
| Deactivate on inactive is rejected | Guard | BR-2.3 |
| Reactivate sets Active and keeps the record | Reactivate | FR-4.1, FR-4.2, AC-4.1–AC-4.3 |
| Reactivate on active is rejected | Guard | BR-2.4 |
| List returns first name, last name, status for all rows | List | FR-5.1, FR-5.2 |
| Duplicate names are allowed | Open question resolution | DD-12 |
| Service has no delete/remove method | Out of scope | Scope |

## Unit tests — JsonFileStore

| Test | Proves | Refs |
| --- | --- | --- |
| Missing file loads as empty list | First run | DD-11 |
| Save then Load returns the same records | Persistence | NFR-2.1, NFR-2.3 |
| Corrupt file throws and does not overwrite | Reliability | NFR-4.1, DD-11 |
| Save is not called on validation failure | Indirect: service never calls store | BR-3.4 |

## Manual acceptance tests

Run against the desktop UI. Network should be off for AC-6.1 / AC-6.2.

### MAT-01 Create — success

1. Open the tool.  
2. Create an employee with first name, last name, and status.  
3. Confirm the row appears.  
4. Close and reopen; the row is still present.

Refs: AC-1.1, AC-1.2, AC-5.1

### MAT-02 Create — rejected name

1. Start create.  
2. Leave a name blank (or spaces only) and save.  
3. Confirm a message is shown and the list is unchanged.

Refs: AC-1.3, AC-1.4, BR-3.3, BR-3.4

### MAT-03 Edit — success

1. Edit first or last name and save.  
2. Confirm the list shows the new name.  
3. Close and reopen; the new name is still present.

Refs: AC-2.1, AC-2.2, AC-5.2

### MAT-04 Edit — rejected name

1. Clear a name and save.  
2. Confirm a message is shown and the saved name is unchanged.

Refs: AC-2.3, AC-2.4

### MAT-05 Deactivate

1. Deactivate an active employee.  
2. Confirm status is Inactive and the row remains.  
3. Close and reopen; status is still Inactive.

Refs: AC-3.1–AC-3.3, AC-5.3

### MAT-06 Reactivate

1. Reactivate an inactive employee.  
2. Confirm status is Active and the row remains.  
3. Close and reopen; status is still Active.

Refs: AC-4.1–AC-4.3, AC-5.3

### MAT-07 List

1. Open the tool with mixed Active and Inactive records.  
2. Confirm each row shows first name, last name, and status, including inactive rows.

Refs: FR-5.1, FR-5.2, DD-13

### MAT-08 Local operation

1. Disconnect the network.  
2. Complete create, edit, deactivate, and reactivate in the UI.

Refs: AC-6.1–AC-6.3, NFR-1.2, NFR-1.3, NFR-3.2

### MAT-09 No delete

1. Inspect the UI.  
2. Confirm there is no delete/remove action.

Refs: Scope

### MAT-10 Edit does not change status

1. Open edit on an active employee.  
2. Confirm status is not on the form.  
3. Save a name change; status stays Active.

Refs: FR-2.2

## Execution

```
dotnet test
```

Expected: all unit tests pass. Then run MAT-01 through MAT-10 on the built executable.

## Definition of done (testing)

- [ ] All unit tests pass
- [ ] MAT-01 through MAT-10 pass
- [ ] Rejected saves do not change the file
- [ ] Close/reopen keeps creates, edits, and status changes
- [ ] Tool works with no network
- [ ] No delete control in the UI
