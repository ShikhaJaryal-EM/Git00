# Assumptions, Constraints, Risks, and Open Items

## Assumptions

- One person uses the tool at a time on one local machine
- The user can start the tool and use a basic interface
- Starting the tool may open a page in a local browser on the same machine
- First name and last name are enough to identify an employee in the UI in v1
- Active and inactive are the only status values needed
- Permanent delete is not needed in v1
- Local storage on the same machine remains available between sessions

## Constraints

- Must run locally
- Must not use cloud or other outside services
- Must not require the internet
- Must not include login, roles, permissions, or access control
- Visual polish is not required (a readable default layout is allowed)
- v1 fields shown to the user are limited to first name, last name, and status
- A database server is not required

## Out of Scope

- Extra employee fields
- Security and access control
- Cloud, sync, hosted services, or multi-user sharing
- External APIs or internet-dependent services
- A database server as a required store
- Advanced search, export, or reporting
- Permanent delete
- Visual design polish (branding, custom themes)

## Known Limitations

- No protection against another person using the same machine
- No guarantee that two people will not have the same name
- No shared list across machines
- Appearance may be plain

## Deferred Items

- Additional employee fields
- Search and sort options
- Permanent delete
- Visual design polish
- Multi-user or shared access

## Dependencies

- A local machine that can run the tool
- A user who can enter first name, last name, and status
- Local storage that remains available between sessions

## Risks

| Risk                                      | If it happens                                      |
| ----------------------------------------- | -------------------------------------------------- |
| Names are not unique                      | Two employees can look the same                    |
| Local storage is cleared                  | The list is lost                                   |
| Scope grows to extra fields or security   | v1 takes longer and is harder to finish            |

## Open Questions

All open questions are **resolved**. Design detail is in `docs/solution/11-design-decisions.md`.

| Question | Decision | Why |
| --- | --- | --- |
| Must first name plus last name be unique? | **No.** Duplicate names are allowed. | No business rule required uniqueness. The known limitation already allows two people to look the same. |
| Should the list show inactive employees by default? | **Yes. Always show them.** | Deactivate and reactivate journeys keep the employee in the list. Hiding rows would add a filter. |
| Is there a preferred order for the list? | **Insertion order. No sort control.** | Search and sort are out of scope. |
| Is status editable on create only, or also during edit? | **Create may set status. Edit changes names only.** Later status changes use deactivate and reactivate. | FR-2.2 is names only. FR-3 and FR-4 are the status flows. |

## Decisions Needed

All decisions below are **closed**.

| Decision                         | Chosen                          | Needed because                          |
| -------------------------------- | ------------------------------- | --------------------------------------- |
| Name uniqueness                  | Not unique                      | Affects create and edit rules           |
| Inactive rows in the list        | Always shown                    | Affects list behavior                   |
| Default list order               | Insertion order (no sort)       | Affects how users find people           |
| Status on edit                   | Name only                       | Affects the edit journey                |
