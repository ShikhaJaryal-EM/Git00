# PHASE-001 — Local Host and Project Foundation

**Spec file**: `docs/implementation/phases/PHASE-001.md`  
**Index**: `docs/implementation/implementation-plan.md`  
**Shape**: `docs/implementation/CANONICAL-PHASE-SHAPE.md`  
**Status**: `docs/implementation/phase-status.md`

#### Phase Identity

**Phase ID**: PHASE-001  
**Phase Name**: Local Host and Project Foundation  
**FR Mapping**: None (foundational)

#### Goal

A local ASP.NET Core 8 Razor Pages host exists on the machine, listens only on loopback, ships Bootstrap from files in the repo, and has an xUnit test project with no authentication and no database.

#### Dependencies

None

#### Plan

1. Create solution `EmployeeRecordsTool.sln` with web project `EmployeeRecordsTool` (ASP.NET Core 8 Razor Pages) and test project `EmployeeRecordsTool.Tests` (xUnit).
2. Configure Kestrel / launch settings to bind `http://127.0.0.1` only (not `0.0.0.0` or a public URL).
3. Add folders `Models/`, `Services/`, `Persistence/`, `Pages/`, `wwwroot/css/`, `wwwroot/lib/bootstrap/` as in `docs/solution/03-tech-stack.md`.
4. Copy Bootstrap 5 CSS and JS into `wwwroot/lib/bootstrap/` (no CDN link tags).
5. Create a placeholder `Pages/Index.cshtml` that renders a heading using local Bootstrap so the host can be opened in a browser.
6. Register nothing for Entity Framework, SQL, Identity, or cloud SDKs. Do not add those package references.
7. Add a smoke test that the test project builds and a trivial assertion passes (`dotnet test`).
8. Document run command in a short project README: `dotnet run` then open `http://127.0.0.1:<port>`.

#### Outcome

- `EmployeeRecordsTool.sln` builds
- App listens on `127.0.0.1`
- Local Bootstrap files exist under `wwwroot/lib/bootstrap/`
- Index page loads without internet
- No database project, connection string, or auth middleware
- `dotnet test` runs

#### Automated Verification (AI Gate)

1. Run `dotnet build` on the solution.
2. Run `dotnet test`.
3. Search the web project for CDN URLs (`cdn.`, `unpkg`, `jsdelivr`) and for EF/SQL/Identity package references.
4. Inspect `launchSettings.json` / `Program.cs` for listen URL `127.0.0.1`.

**Expected**:

- Build succeeds
- Tests pass (at least the smoke test)
- No CDN or database/auth packages
- Loopback bind is configured

#### Human Verification (Human Gate)

1. Confirm the stack matches DD-03 (HTML/CSS/Bootstrap 5 local, Razor Pages, no SPA).
2. Confirm this phase does not introduce login, cloud, or extra product features.
3. Open the placeholder page in a local browser and confirm it is a basic layout (NFR-3.3 — no custom branding).

#### Success Criteria

✅ Solution and both projects exist and build  
✅ Host binds to `127.0.0.1` only  
✅ Bootstrap is local, not CDN  
✅ No database and no authentication  
✅ `dotnet test` succeeds  

**Next**: [PHASE-002](PHASE-002.md)
