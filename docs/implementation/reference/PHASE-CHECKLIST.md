# Phase Refactoring Checklist — Package Reference

Adapted from [`Student Resource Files/PHASE-CHECKLIST.md`](../../../Student%20Resource%20Files/PHASE-CHECKLIST.md), which remains authoritative. Use this checklist when creating, changing, or approving a phase in the [implementation plan](../implementation-plan.md).

## Phase structure

- [ ] **Phase Identity** exists.
  - [ ] Phase ID is unique and sequential.
  - [ ] Phase Name is descriptive.
  - [ ] FR Mapping uses exact authored IDs and names or exactly `None (foundational)`.
- [ ] **Goal** exists.
  - [ ] It is one present-tense sentence.
  - [ ] It describes measurable system state, not effort.
  - [ ] It contains no unnecessary implementation detail.
- [ ] **Dependencies** exists.
  - [ ] Every prerequisite uses a valid Phase ID and name.
  - [ ] It states `None` if there are no phase dependencies.
- [ ] **Plan** exists.
  - [ ] Steps are numbered.
  - [ ] Every step begins with an imperative action.
  - [ ] Steps are dependency-ordered and independently verifiable.
  - [ ] Another AI or developer can execute them blindly.
- [ ] **Outcome** exists.
  - [ ] Deliverables are measurable bullets.
  - [ ] Every outcome is independently verifiable.
- [ ] **Automated Verification (AI Gate)** exists.
  - [ ] Checks are numbered and executable.
  - [ ] Every check includes `**Expected:**`.
  - [ ] Checks verify mechanics and can be automated where practical.
- [ ] **Human Verification (Human Gate)** exists.
  - [ ] It contains two to four numbered items.
  - [ ] Items require human judgment and verify intent or alignment.
- [ ] **Success Criteria** exists.
  - [ ] It contains four to eight checkbox-style criteria.
  - [ ] Every criterion is binary and independently verifiable.

## Quality checks

- [ ] All eight required sections are present in the exact canonical order.
- [ ] No section is empty or contains a placeholder.
- [ ] FR mappings match [`docs/requirements/04-functional-requirements.md`](../../requirements/04-functional-requirements.md) exactly.
- [ ] Dependencies reference phases in the same plan.
- [ ] The phase is independently executable after dependencies complete.
- [ ] The phase is sized as a coherent, reviewable increment.
- [ ] Requirements remain authoritative for behavior.
- [ ] Solution design remains authoritative for technical choices.
- [ ] `OI-01` through `OI-04` are not silently resolved.
- [ ] Authentication, roles, permissions, delete, search, filter, pagination, cloud, and multi-user/concurrency scope remain absent.

## Quick reference

1. Phase Identity
2. Goal
3. Dependencies
4. Plan
5. Outcome
6. Automated Verification (AI Gate)
7. Human Verification (Human Gate)
8. Success Criteria

Good goal: `The system can return every employee in one deterministic roster.`  
Bad goal: `Implement the employee list.`

Good plan step: `Create GET /api/employees with a 200 JSON array and no query parameters.`  
Bad plan step: `Set up listing.`

Good verification: `Call GET /api/employees with an empty database. **Expected:** The response is 200 [].`  
Bad verification: `Test the endpoint.`
