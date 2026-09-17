# PHASE-002 — Employee Model and JSON File Store

**Spec file**: `docs/implementation/phases/PHASE-002.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-002  
**Phase Name**: Employee Model and JSON File Store  
**FR Mapping**: None (foundational)

#### Goal

Employee records can be loaded from and saved to a local JSON file with id, first name, last name, and status, without using a database.

#### Dependencies

PHASE-001 Local Host and Project Foundation ([PHASE-001.md](PHASE-001.md))

#### Plan

1. Create `Models/Employee.cs` with `Id` (GUID string), `FirstName`, `LastName`, and `Status` (`Active` or `Inactive` only).
2. Create `Persistence/IEmployeeStore.cs` with `Load` and `Save`.
3. Create `Persistence/JsonFileStore.cs` using `System.Text.Json`, camelCase, indented JSON, schema `{ "employees": [ ... ] }` per `docs/solution/04-data-design.md`.
4. Default production path: `%AppData%\EmployeeRecordsTool\employees.json` (DD-02). Allow a constructor/path override for tests.
5. Implement Load: create folder if missing; missing file → empty list and create empty file; corrupt file → do not overwrite; surface a readable error (DD-11, ERR-04).
6. Implement Save: rewrite the full file from the in-memory list (DD-05). On write failure, do not report success (ERR-05).
7. Register `IEmployeeStore` in DI with the production path.
8. Add xUnit tests using a temp folder: missing file loads empty; Save then Load round-trip; corrupt file does not overwrite.

#### Outcome

- `Employee` model matches v1 fields plus hidden id
- `JsonFileStore` reads and writes `employees.json`
- Missing file yields an empty list
- Corrupt file is preserved
- Tests use a temp directory, not `%AppData%`

#### Automated Verification (AI Gate)

1. Run `dotnet test --filter JsonFileStore`.
2. Assert missing-file test creates/returns empty `employees` array.
3. Assert round-trip test restores the same id, names, and status.
4. Assert corrupt-file test leaves file bytes unchanged and does not return a silent empty list as success.

**Expected**:

- All JsonFileStore tests pass
- File content matches the documented JSON schema
- No SQL or other store types exist

#### Human Verification (Human Gate)

1. Review the JSON schema against `docs/solution/04-data-design.md` and data requirements (first name, last name, status; hidden id).
2. Confirm there is no database server, SQLite file-as-product, or connection string.
3. Confirm duplicate names are allowed at the store level (DD-12) — no uniqueness constraint on names.

#### Success Criteria

✅ `Employee` model exists with id, first name, last name, status  
✅ JSON file store Load/Save implemented  
✅ Missing-file and corrupt-file behaviour matches DD-11  
✅ Store tests pass  
✅ No database is used  

**Previous**: [PHASE-001](PHASE-001.md) · **Next**: [PHASE-003](PHASE-003.md)
