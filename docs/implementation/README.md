# Employee Directory Implementation Package

This package turns the approved requirements and solution design into an executable, gated seven-phase delivery workflow. Product behavior remains controlled by the requirements; technical choices remain controlled by the solution design.

## Package index

- [Implementation plan](implementation-plan.md) — seven canonical phases with requirement mappings, dependencies, execution instructions, verification gates, and success criteria.
- [Phase status](phase-status.md) — current state, completion date, and blockers for every regular phase.
- [Bugs and issues log](bugs-and-issues-log.md) — issue register, allowed statuses, and entry template.
- [Step-by-step execution](step-by-step-execution.md) — mandatory workflow for educating, planning, implementing, testing, approving, committing, and merging a phase.
- [Canonical phase shape](reference/CANONICAL-PHASE-SHAPE.md) — package-local phase structure and validation rules adapted from the authoritative student resource.
- [Phase checklist](reference/PHASE-CHECKLIST.md) — package-local checklist adapted from the authoritative student resource.

## Authoritative process references

- [Canonical SDLC Phase Shape](../../Student%20Resource%20Files/CANONICAL-PHASE-SHAPE.md)
- [Phase Refactoring Checklist](../../Student%20Resource%20Files/PHASE-CHECKLIST.md)
- [Phase Execution Guide](../../Student%20Resource%20Files/PHASE-EXECUTION-GUIDE.md)
- [Implementation Plan Refactoring Guide](../../Student%20Resource%20Files/REFACTORING-GUIDE.md)

The student resource files are authoritative if a package-local adaptation conflicts with them.

## Product and design references

### Approved requirements

- [Executive summary and context](../requirements/01-executive-summary-and-context.md)
- [Stakeholders and personas](../requirements/02-stakeholders-and-personas.md)
- [Business objectives](../requirements/03-business-objectives.md)
- [Functional requirements](../requirements/04-functional-requirements.md)
- [Business rules](../requirements/05-business-rules.md)
- [User journeys](../requirements/06-user-journeys.md)
- [Data requirements](../requirements/07-data-requirements.md)
- [Permissions and access control](../requirements/08-permissions-and-access-control.md)
- [Non-functional requirements](../requirements/09-non-functional-requirements.md)
- [Assumptions, constraints, and out of scope](../requirements/10-assumptions-constraints-out-of-scope.md)

### Implementation-ready solution design

- [Solution design index](../solution-design/README.md)
- [Document control](../solution-design/00-document-control.md)
- [Architecture overview](../solution-design/01-architecture-overview.md)
- [Frontend design](../solution-design/02-frontend-design.md)
- [Backend design](../solution-design/03-backend-design.md)
- [API design](../solution-design/04-api-design.md)
- [Data design](../solution-design/05-data-design.md)
- [Cross-cutting concerns](../solution-design/06-cross-cutting-concerns.md)
- [Testing strategy](../solution-design/07-testing-strategy.md)
- [Local containerization](../solution-design/08-local-containerization.md)
- [Decisions and open items](../solution-design/09-decisions-and-open-items.md)
- [Requirements traceability](../solution-design/10-requirements-traceability.md)

Every Markdown file under `docs/requirements` and `docs/solution-design` is an input to this package.

## Operating rules

1. Select the next phase from [phase status](phase-status.md) only after all dependency phases are complete.
2. Read the full phase in the [implementation plan](implementation-plan.md).
3. Follow [step-by-step execution](step-by-step-execution.md), including the explicit Go/No-Go and approval pauses.
4. Record issues without fixing them at log time in [bugs and issues log](bugs-and-issues-log.md).
5. Never commit phase work until the user explicitly says `approved` or `commit`.
6. Preserve the approved exclusions: no authentication, roles, permission checks, permanent delete, search, filter, pagination, cloud resources, or multi-user/concurrent-access design.
7. Do not silently resolve `OI-01` through `OI-04`; use documented defaults only while each remains pending.
