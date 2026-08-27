# Canonical SDLC Phase Shape — Package Reference

Adapted from [`Student Resource Files/CANONICAL-PHASE-SHAPE.md`](../../../Student%20Resource%20Files/CANONICAL-PHASE-SHAPE.md), which remains authoritative. This local copy makes the mandatory shape available beside the implementation package.

## Purpose

Every phase must be independently executable, requirement-traceable, mechanically verifiable, reviewable for intent, and structurally consistent.

## Mandatory shape

Every regular or bug-fix phase contains these sections in this exact order:

1. `#### Phase Identity`
2. `#### Goal`
3. `#### Dependencies`
4. `#### Plan`
5. `#### Outcome`
6. `#### Automated Verification (AI Gate)`
7. `#### Human Verification (Human Gate)`
8. `#### Success Criteria`

### 1. Phase Identity

```markdown
#### Phase Identity

**Phase ID**: PHASE-XXX
**Phase Name**: Descriptive Name
**FR Mapping**:
- FR-X.Y Exact authored requirement name
```

- IDs are unique and sequential.
- Names are descriptive.
- FR mappings use exact authored IDs and names without inventing, modifying, merging, or splitting requirements.
- A genuinely foundational phase uses exactly `**FR Mapping**: None (foundational)`.

### 2. Goal

```markdown
#### Goal

The system can [single-sentence measurable state after completion].
```

- Use one present-tense sentence.
- Describe system state, not implementation effort.
- Keep implementation details in Plan.

### 3. Dependencies

```markdown
#### Dependencies

PHASE-XXX Phase Name
```

- List every prerequisite by Phase ID and name.
- Use `None` when no phase dependency exists.

### 4. Plan

```markdown
#### Plan

1. Create [specific artifact and location].
2. Implement [specific behavior and constraints].
3. Verify [independently testable result].
```

- Use numbered, imperative instructions.
- Order by dependency.
- Make every step specific enough for blind execution and independently verifiable.

### 5. Outcome

```markdown
#### Outcome

- A measurable deliverable exists.
- A specific behavior works.
```

- Use bullets.
- State observable, measurable deliverables rather than “done” claims.

### 6. Automated Verification (AI Gate)

```markdown
#### Automated Verification (AI Gate)

1. Run a specific command or verification.
   **Expected:** State the exact successful result.
2. Inspect a specific contract or artifact.
   **Expected:** State the binary expected condition.
```

- Verify mechanics with executable commands, tests, or deterministic inspection.
- Number every check and include an explicit `**Expected:**` result.
- Prefer checks suitable for CI or an automated runner.

### 7. Human Verification (Human Gate)

```markdown
#### Human Verification (Human Gate)

1. Review requirement or design alignment requiring judgment.
2. Confirm user-facing intent or usability.
```

- Include two to four numbered checks.
- Verify intent, alignment, usability, or design judgment rather than repeat automated mechanics.

### 8. Success Criteria

```markdown
#### Success Criteria

- [ ] ✅ Binary criterion one.
- [ ] ✅ Binary criterion two.
- [ ] ✅ Binary criterion three.
- [ ] ✅ Binary criterion four.
```

- Include four to eight independently verifiable checkbox criteria.
- Completion is binary; every criterion must be met.

## Validation

Before accepting a phase, confirm:

- All eight sections exist, are nonempty, and occur in order.
- FR mappings are exact or foundational.
- The goal is a single sentence describing state.
- Dependencies reference valid IDs.
- The plan is numbered, imperative, dependency-ordered, and blind-executable.
- Outcomes are measurable.
- Every automated check has an expected result.
- Human verification contains two to four judgment checks.
- Success criteria contain four to eight binary checkboxes.
- The phase does not silently add scope or resolve an open item.

## Template

```markdown
## PHASE-XXX Phase Name

#### Phase Identity

**Phase ID**: PHASE-XXX
**Phase Name**: Phase Name
**FR Mapping**:
- FR-X.Y Exact authored requirement name

#### Goal

The system can [single-sentence state].

#### Dependencies

PHASE-YYY Dependency Name

#### Plan

1. Create [specific artifact].
2. Implement [specific behavior].
3. Add [specific verification].

#### Outcome

- [Measurable deliverable.]
- [Verifiable behavior.]

#### Automated Verification (AI Gate)

1. [Specific command or check.]
   **Expected:** [Exact result.]
2. [Specific command or check.]
   **Expected:** [Exact result.]

#### Human Verification (Human Gate)

1. [Judgment-based review.]
2. [Intent/alignment review.]

#### Success Criteria

- [ ] ✅ [Binary criterion.]
- [ ] ✅ [Binary criterion.]
- [ ] ✅ [Binary criterion.]
- [ ] ✅ [Binary criterion.]
```

## Completion tracking

Before a phase is marked complete, all success criteria and both verification gates must pass, FR mappings must be rechecked, and downstream dependencies must be ready. Track the result in [`../phase-status.md`](../phase-status.md) and follow [`../step-by-step-execution.md`](../step-by-step-execution.md).
