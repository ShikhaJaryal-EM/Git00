# PHASE-008 — Persistence, Local-Only Operation, and Reliability

**Spec file**: `docs/implementation/phases/PHASE-008.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-008  
**Phase Name**: Persistence, Local-Only Operation, and Reliability  
**FR Mapping**:
- FR-1.3 After create, the new record is visible in the list
- FR-2.3 After edit, the updated values are visible in the list
- FR-3.2 After deactivate, the employee's status is inactive
- FR-4.2 After reactivate, the employee's status is active
- FR-5.1 The user can see the saved employee records

#### Goal

Created records, edited names, and status changes remain after a normal process stop and start; the tool works with the internet off, uses no cloud or database, remains usable after a rejected save, and does not add out-of-scope capabilities.

#### Dependencies

PHASE-007 Deactivate and Reactivate UI Actions ([PHASE-007.md](PHASE-007.md))

#### Plan

1. Confirm every accepted Create, UpdateNames, Deactivate, and Reactivate calls `JsonFileStore.Save`.
2. Confirm Index GET calls Load so a new process reads `employees.json` (NFR-2.1–NFR-2.3).
3. Confirm Bind remains `127.0.0.1`; no CDN; no outbound HTTP clients.
4. Confirm corrupt-file path shows ERR-04 and does not overwrite; save failure shows ERR-05; tool can retry (NFR-4.2).
5. Confirm rejected create/edit still does not write (NFR-4.1).
6. Run the full unit suite (`dotnet test`).
7. Prepare MAT-01 step 4, MAT-03 step 3, MAT-05 step 3, MAT-06 step 3, MAT-08 for human execution.
8. Sweep the repo: no extra fields, no login, no search/export, no delete, no custom theme.

#### Outcome

- Close and reopen (process restart) keeps creates, edits, and status
- App usable offline with local Bootstrap
- No database or outside service
- Rejected save does not corrupt the list
- Full unit suite green

#### Automated Verification (AI Gate)

1. Run `dotnet test` (entire solution).
2. Run JsonFileStore round-trip and corrupt-file tests.
3. Scan for CDN, EF/SQL packages, Identity, and extra model properties beyond id/firstName/lastName/status.
4. Confirm production store path is under `%AppData%\EmployeeRecordsTool\`.

**Expected**:

- All unit tests pass
- No forbidden packages or extra fields
- Persistence tests prove Save/Load round-trip

#### Human Verification (Human Gate)

1. Execute close/reopen persistence: MAT-01 step 4 (AC-5.1), MAT-03 step 3 (AC-5.2), MAT-05/MAT-06 step 3 (AC-5.3, NFR-4.3).
2. Execute MAT-08 with internet disconnected (AC-6.1, AC-6.2, NFR-1.1–NFR-1.3): pages load; all four actions work; no database required.
3. Re-confirm MAT-02 and MAT-04: after rejection the tool is still usable (NFR-4.2) and the file is unchanged (NFR-4.1).
4. Confirm visual polish was not added (NFR-3.3, DD-15) and scope exclusions still hold.

#### Success Criteria

✅ `dotnet test` passes  
✅ Process restart preserves creates, name edits, and status  
✅ Tool works with no internet and no cloud  
✅ No database, login, delete, extra fields, or CDN  
✅ MAT-01 through MAT-10 can be executed and are intended to pass  

**Previous**: [PHASE-007](PHASE-007.md)
