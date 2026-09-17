# Canonical SDLC Phase Shape — Reference Guide

## Purpose

This document defines the **Canonical SDLC Phase Shape** — a standardized structure for implementation plan phases that ensures:
- **Clarity**: Every phase is independently executable
- **Traceability**: Functional Requirements (FRs) are explicitly mapped
- **Verifiability**: Clear AI and human gates for completion
- **Consistency**: Uniform structure across all phases

## When to Use

Use this shape when:
- Creating a new phased implementation plan
- Refactoring an existing implementation plan
- Reviewing phase completeness
- Onboarding new team members to a project

---

## Applied to this project (Employee Records Tool)

This file lives with the implementation plan. The **index** is `implementation-plan.md`. Each phase is a **separate file** under `phases/` so work can be done in chunks. Every phase file uses the eight sections below.

**Sources:** `docs/requirements` (FR/BR/NFR/AC truth), `docs/solution` (architecture, JSON store, Razor Pages + local Bootstrap).

Keep this Phase Index identical to `implementation-plan.md` and to each file’s Phase Identity section.

### Phase Index (ID, Names, FR Mapping)

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

Track files:

- Traceability: [traceability.md](traceability.md)
- Phase status: [phase-status.md](phase-status.md)
- Bugs and issues: [bugs-and-issues-log.md](bugs-and-issues-log.md)

---

## Canonical Phase Shape (MANDATORY)

Every phase **MUST** contain the following sections, in this exact order:

### 1. Phase Identity

**Format:**
```
#### Phase Identity

**Phase ID**: PHASE-XXX (or PHASE-XXX.Y for sub-phases)
**Phase Name**: [Descriptive Name]
**FR Mapping**:
- FR-X.Y.Z [Requirement description]
- FR-X.Y.Z [Requirement description]
- ...
```

**Rules:**
- Phase IDs must be sequential and unique
- Phase Names should be descriptive (e.g., "Allocation Creation (API-only)")
- **FR Mapping is MANDATORY** — every phase must list at least one FR, unless explicitly foundational
- Foundational phases must state: `FR Mapping: None (foundational)`
- FRs must appear exactly as authored (IDs and names unchanged)
- A phase may implement partial FRs, but must list them explicitly
- An FR may appear in multiple phases if implementation is incremental

### 2. Goal

**Format:**
```
#### Goal

[Single sentence describing the system state after this phase completes]
```

**Rules:**
- Goals describe **system state**, not effort
- Use present tense: "The system can..." or "X exists..."
- Be specific and measurable
- Avoid implementation details (save for Plan section)

**Examples:**
- ✅ "The system can authenticate users, issue secure tokens, and enforce role-based access for protected backend endpoints."
- ✅ "Shared taxonomies used by Practitioners, Projects, Open Roles, and Matching are created and seeded with initial values."
- ❌ "Implement authentication" (too vague)
- ❌ "Work on taxonomies" (describes effort, not state)

### 3. Dependencies

**Format:**
```
#### Dependencies

PHASE-XXX [Phase Name]
PHASE-YYY [Phase Name]
```

**Rules:**
- List all phases that must complete before this phase can start
- Use Phase IDs (e.g., `PHASE-002`)
- If no dependencies, state: `None`
- Dependencies should reference Phase IDs, not phase numbers

### 4. Plan

**Format:**
```
#### Plan

1. [Action verb] [what] [how/where]
2. [Action verb] [what] [how/where]
3. ...
```

**Rules:**
- Plans are **instructions**, not explanations
- Use imperative mood (action verbs: create, implement, configure, add, etc.)
- Be specific enough that another AI or developer could execute blindly
- Order steps logically (dependencies first)
- Each step should be independently verifiable

**Examples:**
- ✅ "Create `user_account` table with constraints (unique `entra_oid`; case-insensitive email uniqueness)"
- ✅ "Implement Entra JWT validation"
- ✅ "Configure Spring Security OAuth2 Resource Server"
- ❌ "Set up authentication" (too vague)
- ❌ "Work on user accounts" (not an instruction)

### 5. Outcome

**Format:**
```
#### Outcome

- [Measurable deliverable 1]
- [Measurable deliverable 2]
- ...
```

**Rules:**
- Outcomes are **measurable deliverables**
- Use bullet points
- Each outcome should be verifiable
- Focus on what exists/works after the phase completes

**Examples:**
- ✅ "`user_account` table exists with proper constraints"
- ✅ "JWT validation works"
- ✅ "RBAC is enforced"
- ❌ "Authentication is done" (not measurable)

### 6. Automated Verification (AI Gate)

**Format:**
```
#### Automated Verification (AI Gate)

1. [Test/verification step 1]
2. [Test/verification step 2]
3. ...

**Expected**:
- [Expected result 1]
- [Expected result 2]
- ...
```

**Rules:**
- AI Gates verify **mechanics** (does it work?)
- List specific test commands or verification steps
- Include expected results
- Should be executable by CI/CD or automated test runner
- Use specific commands where possible (e.g., `curl`, test names, etc.)

**Examples:**
- ✅ "Run Testcontainers migration tests"
- ✅ "Integration tests for CRUD operations"
- ✅ "Test JIT provisioning creates `user_account` on first login"
- ✅ "Verify constraint violations are rejected"
- ❌ "Test authentication" (too vague)

### 7. Human Verification (Human Gate)

**Format:**
```
#### Human Verification (Human Gate)

1. [Review/verification step requiring human judgment]
2. [Review/verification step requiring human judgment]
3. ...
```

**Rules:**
- Human Gates verify **intent** (does it align with requirements?)
- Focus on things requiring human judgment (design review, requirement alignment, etc.)
- Should be review/validation steps, not execution steps
- Keep to 2-4 items maximum

**Examples:**
- ✅ "Review table structure matches data requirements"
- ✅ "Verify role mapping works correctly"
- ✅ "Confirm seed values match requirements"
- ❌ "Test the system" (this is automated)
- ❌ "Make sure it works" (not specific)

### 8. Success Criteria

**Format:**
```
#### Success Criteria

✅ [Criterion 1]
✅ [Criterion 2]
✅ ...
```

**Rules:**
- Success Criteria are **binary** (done or not done)
- Use checkboxes (✅) for visual clarity
- Each criterion should be independently verifiable
- Completion is binary — all criteria must be met
- Keep to 4-8 items maximum

**Examples:**
- ✅ "`user_account` table created"
- ✅ "JWT validation works"
- ✅ "RBAC enforced"
- ✅ "Integration tests pass"
- ❌ "Authentication is good" (not binary/verifiable)

---

## Refactoring Process

### Step 1: Identify Phase Boundaries

- Review existing phase plan
- Ensure phases are dependency-ordered
- Split phases that are too large (aim for 1-2 weeks of work)
- Merge phases that are too small (unless they represent distinct dependencies)

### Step 2: Extract FR Mappings

- For each phase, identify which Functional Requirements it implements
- List FRs explicitly (e.g., `FR3.1.1`, `FR3.1.2`)
- If phase is foundational (infrastructure, tooling), state: `FR Mapping: None (foundational)`
- **Never invent, modify, merge, or split FRs** — use them exactly as authored

### Step 3: Rewrite Each Phase

For each phase:

1. **Phase Identity**: Assign Phase ID, Name, and list FRs
2. **Goal**: Rewrite as system state (not effort)
3. **Dependencies**: List Phase IDs that must complete first
4. **Plan**: Convert deliverables/descriptions into step-by-step instructions
5. **Outcome**: List measurable deliverables
6. **Automated Verification**: Define testable verification steps with expected results
7. **Human Verification**: Define review steps requiring human judgment
8. **Success Criteria**: List binary completion criteria

### Step 4: Validate

- Every phase has all 8 sections
- FR mappings are explicit (or "None (foundational)")
- Goals describe system state
- Plans are instructions (not explanations)
- Outcomes are measurable
- Verification gates are executable
- Success criteria are binary

---

## Common Mistakes to Avoid

### ❌ Goals describe effort
- Bad: "Implement authentication system"
- Good: "The system can authenticate users and issue secure tokens"

### ❌ Plans are explanations
- Bad: "Authentication is implemented using OAuth2"
- Good: "Configure Spring Security OAuth2 Resource Server"

### ❌ FR mappings are implicit
- Bad: "This phase implements authentication"
- Good: "FR Mapping: FR1.9.1 Single Sign-On support, FR1.9.2 Microsoft Entra ID integration"

### ❌ Verification is vague
- Bad: "Test authentication"
- Good: "Integration tests for `/api/me` endpoint return user info for authenticated users"

### ❌ Success criteria are subjective
- Bad: "Authentication works well"
- Good: "✅ OAuth2 Resource Server configured, ✅ JWT validation works, ✅ `/api/me` endpoint functional"

---

## Example: Fully Compliant Phase

See `phases/PHASE-001.md` through `phases/PHASE-008.md` for full specs. Condensed example from this project:

```markdown
### Phase 1 — Local Host and Project Foundation

#### Phase Identity

**Phase ID**: PHASE-001  
**Phase Name**: Local Host and Project Foundation  
**FR Mapping**: None (foundational)

#### Goal

A local ASP.NET Core 8 Razor Pages host exists on the machine, listens only on loopback, ships Bootstrap from files in the repo, and has an xUnit test project with no authentication and no database.

#### Dependencies

None

#### Plan

1. Create solution `EmployeeRecordsTool.sln` with web and xUnit test projects
2. Bind Kestrel to `http://127.0.0.1` only
3. Copy Bootstrap 5 into `wwwroot/lib/bootstrap/` (no CDN)
4. Add placeholder Index page
5. Do not add EF, SQL, Identity, or cloud packages

#### Outcome

- Solution builds
- App listens on `127.0.0.1`
- Local Bootstrap loads
- `dotnet test` runs

#### Automated Verification (AI Gate)

1. Run `dotnet build`
2. Run `dotnet test`
3. Scan for CDN URLs and EF/SQL/Identity packages
4. Confirm listen URL is `127.0.0.1`

**Expected**:
- Build and tests succeed
- No CDN, database, or auth packages
- Loopback bind is configured

#### Human Verification (Human Gate)

1. Confirm stack matches DD-03
2. Confirm no login, cloud, or extra product features
3. Confirm placeholder UI is default Bootstrap only (NFR-3.3)

#### Success Criteria

✅ Solution builds  
✅ Host binds to `127.0.0.1` only  
✅ Bootstrap is local  
✅ No database and no authentication  
✅ `dotnet test` succeeds
```

---

## Template for New Phases

Copy this template when creating new phases:

```markdown
### Phase X — [Phase Name]

#### Phase Identity

**Phase ID**: PHASE-XXX  
**Phase Name**: [Descriptive Name]  
**FR Mapping**:
- FR-X.Y.Z [Requirement description]
- FR-X.Y.Z [Requirement description]

#### Goal

[Single sentence describing system state after completion]

#### Dependencies

PHASE-XXX [Phase Name]
PHASE-YYY [Phase Name]

#### Plan

1. [Action verb] [what] [how/where]
2. [Action verb] [what] [how/where]
3. [Action verb] [what] [how/where]

#### Outcome

- [Measurable deliverable 1]
- [Measurable deliverable 2]
- [Measurable deliverable 3]

#### Automated Verification (AI Gate)

1. [Test/verification step 1]
2. [Test/verification step 2]
3. [Test/verification step 3]

**Expected**:
- [Expected result 1]
- [Expected result 2]
- [Expected result 3]

#### Human Verification (Human Gate)

1. [Review/verification step 1]
2. [Review/verification step 2]

#### Success Criteria

✅ [Criterion 1]
✅ [Criterion 2]
✅ [Criterion 3]
✅ [Criterion 4]
```

---

## Integration with Project Management

### Phase Status Tracking

Maintain a `phase-status.md` file that tracks:
- Phase ID
- Status (Not Started, In Progress, Completed, Blocked)
- Completion date
- Blockers (if any)

### Phase Completion Checklist

Before marking a phase complete:
- [ ] All Success Criteria met
- [ ] Automated Verification passes
- [ ] Human Verification completed
- [ ] FR mappings verified
- [ ] Dependencies for next phases satisfied

---

## Questions?

- Review existing phases in `docs/implementation/phases/` for examples
- Ensure every phase follows the canonical shape exactly
- When in doubt, ask: "Can another AI execute this phase blindly?"
