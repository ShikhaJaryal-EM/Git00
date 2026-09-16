# Non-Functional Requirements and Constraints

## Purpose

Show how local operation, persistence, UI, and reliability are met without adding out-of-scope capabilities.

## Requirement references

- NFR-1.1–NFR-4.3
- AC-5.1–AC-6.3
- `10-assumptions-constraints-risks-and-open-items.md`

---

## Local operation (NFR-1)

| ID | Requirement | Design |
| --- | --- | --- |
| NFR-1.1 | Runs on a local machine | Desktop executable; UI, logic, and file on that machine |
| NFR-1.2 | No network connection required | No HTTP, sockets, or remote calls |
| NFR-1.3 | No cloud or outside service | JSON file only |

**Verify:** Create, edit, deactivate, and reactivate with the network disabled (AC-6.1, AC-6.2).

## Persistence (NFR-2)

| ID | Requirement | Design |
| --- | --- | --- |
| NFR-2.1 | Records present after normal close and reopen | `Save` after each accepted change; `Load` on startup |
| NFR-2.2 | Persistence without network or cloud | Local file path only |
| NFR-2.3 | Normal restart does not lose saved records | Same as NFR-2.1 |

**Verify:** AC-5.1–AC-5.3.

This section is about **how well** data stays saved, not which fields are stored.

Normal close means the user closes the window after a successful save. Power loss or a killed process during a write is not guaranteed (known limitation: local storage can be lost if the disk is cleared).

## User interface (NFR-3)

| ID | Requirement | Design |
| --- | --- | --- |
| NFR-3.1 | Basic working interface | Main list plus create and edit forms |
| NFR-3.2 | Create, edit, deactivate, reactivate from the UI | All four actions are on-screen (AC-6.3) |
| NFR-3.3 | Visual polish not required | Standard controls; no branding or themes |

## Reliability (NFR-4)

| ID | Requirement | Design |
| --- | --- | --- |
| NFR-4.1 | Rejected create or edit does not corrupt the saved list | Validate before `Save`; failed validation never writes |
| NFR-4.2 | Usable after a rejected save | Form stays open or user returns to the list; can retry |
| NFR-4.3 | Usable after a normal close and reopen | `Load` on startup; empty list is a valid first-run state |

## Assumptions treated as design rules

| Assumption | Implication |
| --- | --- |
| One person, one local machine | No concurrency or file locking |
| First name and last name identify a person in the UI | Duplicate names allowed; internal id used for updates (DD-12) |
| Active and Inactive are the only statuses | Enum of two values |
| Permanent delete not needed | No delete path |

## Constraints treated as design rules

| Constraint | Compliance |
| --- | --- |
| Must run locally | Desktop process |
| Must not use cloud or outside services | Local JSON |
| Must not include login, roles, or access control | No auth UI or checks |
| Visual polish not required | Simple WinForms list and forms (`03-tech-stack.md`) |
| v1 fields limited to first name, last name, status | Schema has those fields plus hidden id |

## Known limitations (accepted)

| Limitation | Design stance |
| --- | --- |
| No protection against another person on the same machine | Anyone who can open the tool can change the list |
| Two people may have the same name | Allowed (DD-12) |
| No shared list across machines | File lives on one machine |
| Appearance may be plain | NFR-3.3 |

## Informal performance (not a contractual NFR)

For a small local list, load and save should feel immediate. There is no performance requirement in the package; this is only a design guideline.
