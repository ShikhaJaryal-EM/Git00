# Step-by-Step Phase Execution

Follow this workflow for every regular or approved bug-fix phase. The authoritative source is the [Phase Execution Guide](../../Student%20Resource%20Files/PHASE-EXECUTION-GUIDE.md); this project-specific version encodes its required pauses and artifacts.

## 0. Load and classify the phase

1. Confirm [bugs-and-issues-log.md](bugs-and-issues-log.md) exists.
2. Read the full phase specification in [implementation-plan.md](implementation-plan.md).
3. For a regular phase, verify its dependencies are `Completed` in [phase-status.md](phase-status.md).
4. For an approved bug-fix phase, also read its corresponding bug entry.
5. Reconfirm the phase's FR mapping, scope exclusions, and applicable pending open items (`OI-01` through `OI-04`).

## 1. Phase Education

Before changing files, present:

### What

- Purpose and intended system state.
- Included scope and explicit exclusions.
- Context within the seven-phase dependency chain.

### How

- High-level technical approach.
- Component and clean-architecture relationships.
- Patterns and project conventions.

### Dependencies

- Required completed phases.
- External tools, services, and libraries.
- Work blocked until this phase completes.

### Key Deliverables

- Code artifacts.
- Configuration.
- Documentation.
- Unit, integration, component, end-to-end, and acceptance tests as applicable.

## 2. Game Plan

Present a phase-specific plan covering:

1. High-level architecture, technology choices, integration strategy, and risk mitigation.
2. Dependency-ordered operations, parallelizable work, and the critical path.
3. API, data, error, validation, local-security, and open-item decisions with rationale.
4. Existing APIs, shared components, migrations, configuration, and test infrastructure used.

Do not add behavior outside the phase or silently resolve an open item.

## 3. Implementation Checklist

Create a granular, actionable, requirement-traceable checklist before implementation. Include applicable categories:

- [ ] Project structure and setup
- [ ] Dependencies and configuration
- [ ] Domain and core implementation
- [ ] Persistence and migrations
- [ ] API and OpenAPI contracts
- [ ] Frontend behavior and accessibility
- [ ] Validation and error handling
- [ ] Logging, local-origin controls, and safe diagnostics
- [ ] Unit, integration, component, end-to-end, and acceptance tests
- [ ] Documentation
- [ ] Linting, type-checking, build, and packaging

Each nested task must be small, specific, testable, and tied to a plan step or requirement where applicable.

## 4. Definition of Done

Define binary phase-specific checks under all applicable headings:

### Code Quality

- [ ] Project conventions are followed.
- [ ] Lint and type checks have no errors or warnings.
- [ ] Clean builds and packages succeed.
- [ ] No unrelated or excluded-scope behavior was added.

### Functional Requirements

- [ ] Every mapped requirement and applicable business rule is implemented.
- [ ] Edge and error cases are handled.
- [ ] Integration points work.
- [ ] Pending open items use only documented defaults and remain visibly pending.

### Testing

- [ ] Required unit, integration, component, end-to-end, and acceptance tests pass.
- [ ] No test is skipped or disabled.
- [ ] Tests are deterministic.
- [ ] Coverage satisfies the phase-specific target or documented scenario matrix.

### Documentation and Verification

- [ ] OpenAPI, configuration, and operating documentation are updated when applicable.
- [ ] Automated Verification (AI Gate) passes.
- [ ] Human Verification (Human Gate) is ready for execution.
- [ ] Every Success Criterion is met.

## 5. Test Coverage

Before implementation, state for every applicable layer:

- **Unit tests:** scope, target, file locations, key scenarios, and mocking.
- **Integration tests:** scope, PostgreSQL Testcontainer environment, locations, scenarios, and data lifecycle.
- **Frontend tests:** network-boundary mocking, observable behaviors, accessibility, errors, and excluded controls.
- **End-to-end/acceptance tests:** user journeys, framework, setup, and test data.
- **Execution:** exact commands, expected successful output, coverage-report location, and CI/local integration.

All tests must pass, none may be skipped or disabled, and tests must be deterministic and practical for development.

## 6. Go/No-Go gate

After presenting Phase Education, Game Plan, Implementation Checklist, Definition of Done, and Test Coverage, ask exactly:

> Ready to proceed? Please respond with 'go' or 'no-go'

Do not create a branch or implement until the user explicitly says `go`. If the response is `no-go`, address concerns, revise the pre-execution package, and ask again.

## 7. Create and verify the branch

After `go`:

1. Create `feature/{phase-id-short-name}`; for example, `feature/phase-4-create-employee`.
2. Show and verify the current branch before editing.
3. Do not reuse an unrelated branch or commit at branch creation.

## 8. Execute implementation

1. Create only the project structure needed by the phase.
2. Add only required dependencies and configuration.
3. Implement every checklist item in dependency order.
4. Write all planned tests alongside behavior.
5. Run and fix lint and type checks.
6. Run and fix clean builds and packages.
7. Run the complete relevant test suites.
8. Inspect coverage or scenario completion and record evidence.
9. Update OpenAPI and documentation in the same change as affected behavior.
10. Keep the approved exclusions absent and open items explicit.

## 9. Quality gates

Do not proceed to manual verification until all applicable gates pass:

- [ ] Implementation is complete and functional.
- [ ] All planned tests pass.
- [ ] No tests are skipped or disabled.
- [ ] Lint and type checks pass.
- [ ] Builds and packages succeed.
- [ ] Migrations and OpenAPI validation pass.
- [ ] Coverage or required scenario matrix is complete.
- [ ] The phase's Automated Verification (AI Gate) passes.

If a gate fails, fix within phase scope and rerun it; do not request approval with a failing gate.

## 10. Provide the manual test plan

Provide setup prerequisites, environment/database preparation, service start commands, and a scenario register. Every scenario must include:

1. Test ID and name.
2. Description.
3. Prerequisites.
4. Numbered steps.
5. Expected result.
6. **Actual result:** left blank for the tester.
7. **Pass/Fail:** left blank for the tester.

Cover happy paths, validation/errors, boundaries, component integration, local-only posture, and browser/device behavior where applicable. Include keyboard use, status readability, refresh/restart persistence, duplicate feedback, and unavailable-service recovery in the phase where each applies.

## 11. Ask the bug question

After implementation and the manual test plan, but before approval or any commit, ask exactly:

> Is there any need to track any bugs or issues before moving on to the next phase?

If yes, add each item to [bugs-and-issues-log.md](bugs-and-issues-log.md), set **Phase when added** to the current Phase ID, **Status** to `new`, and **Fixed date** empty. Logging does not authorize fixing the issue.

## 12. Request approval without committing

Present:

- Features implemented.
- Files created and modified.
- Dependencies and configuration changes.
- Unit, integration, frontend, end-to-end, and acceptance results.
- Coverage or scenario evidence and exact commands.
- The manual test plan and any special prerequisites.
- Bug entries added.
- Remaining open items and blockers.

Then state exactly:

> All implementation is complete. All tests pass. Ready for review.

> I have NOT committed any code. Waiting for your approval.

Wait for the user to say `approved` or `commit`. Do not commit automatically, skip tests, or proceed with failing tests.

## 13. Post-approval commit and merge question

Only after explicit approval:

1. Review status and diffs, and stage only phase-related files without secrets.
2. Commit once with a descriptive message containing the Phase ID, phase name, concise purpose, and requirement traceability.
3. Show the commit hash and summary.
4. Update [phase-status.md](phase-status.md) as applicable.
5. Ask exactly:

> Would you like me to merge this branch, or will you handle it?

Do not merge until the user answers.

## Bug review and bug-fix phases

When the user requests bug review, process every logged bug in order and ask whether each should be fixed before proceeding. For a selected bug, draft a full canonical `PHASE-BUG-nnn` in chat using the bug and impacted references. Do not add it to the implementation plan until the user approves the phase text. After approval, add it to a clearly separate bug-fix index/section and ask whether to execute it now or continue to the next regular phase. Execution then follows this entire workflow.
