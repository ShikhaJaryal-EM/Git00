# Requirements Package — Employee Directory

**Version:** 1.0  
**Status:** Approved

This package defines what the Employee Directory application must do. It does not include solution design or implementation.

## Document Index

| # | Document | Description |
|---|----------|-------------|
| 01 | [Executive Summary & Business Context](01-executive-summary-and-context.md) | What the tool is and why it exists |
| 02 | [Stakeholders & Personas](02-stakeholders-and-personas.md) | Who uses the system |
| 03 | [Business Objectives](03-business-objectives.md) | Measurable outcomes the tool must achieve |
| 04 | [Functional Requirements](04-functional-requirements.md) | System capabilities (FR-1.x) |
| 05 | [Business Rules](05-business-rules.md) | Logic, validation, and constraints |
| 06 | [User Journeys](06-user-journeys.md) | Step-by-step user flows |
| 07 | [Data Requirements](07-data-requirements.md) | Entities, fields, storage, and sorting |
| 08 | [Permissions & Access Control](08-permissions-and-access-control.md) | Access model (explicitly none) |
| 09 | [Non-Functional Requirements](09-non-functional-requirements.md) | Quality attributes (NFR-1.x) |
| 10 | [Assumptions, Constraints & Out of Scope](10-assumptions-constraints-out-of-scope.md) | Boundaries and exclusions |

## Acceptance Criteria

1. User can create employees with first name, last name, and email; invalid or duplicate email is rejected with a clear message.
2. User can edit names and email; duplicate email on edit shows a clear error.
3. User can deactivate Active employees and reactivate Inactive ones; no delete option exists.
4. All employees appear in one alphabetically sorted list with Active/Inactive badges.
5. Correct row actions appear per status (Edit + Deactivate vs Edit + Reactivate).
6. Data persists between sessions.
7. Application runs locally in the browser with single-instance enforcement.
