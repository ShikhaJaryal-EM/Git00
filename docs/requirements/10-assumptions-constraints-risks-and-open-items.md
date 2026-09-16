# Assumptions, Constraints, Risks, and Open Items

## Assumptions

- One person uses the tool at a time on one local machine
- The user can start the tool and use a basic interface
- First name and last name are enough to identify an employee in v1
- Active and inactive are the only status values needed
- Permanent delete is not needed in v1

## Constraints

- Must run locally
- Must not use cloud or other outside services
- Must not include login, roles, permissions, or access control
- Visual polish is not required
- v1 fields are limited to first name, last name, and status

## Out of Scope

- Extra employee fields
- Security and access control
- Cloud, sync, or multi-user sharing
- Advanced search, export, or reporting
- Permanent delete
- Visual design polish

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

- Must first name plus last name be unique?
- Should the list show inactive employees by default?
- Is there a preferred order for the list?
- Is status editable on create only, or also during edit?

## Decisions Needed

| Decision                         | Options                         | Needed because                          |
| -------------------------------- | ------------------------------- | --------------------------------------- |
| Name uniqueness                  | Unique or not unique            | Affects create and edit rules           |
| Inactive rows in the list        | Always shown or hidden          | Affects list behavior                   |
| Default list order               | None, name, or status           | Affects how users find people           |
| Status on edit                   | Name only, or name and status   | Affects the edit journey                |
