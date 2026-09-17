# Solution Design Package — Employee Records Tool

## Purpose

This package describes **how** the Employee Records Tool will be built. It translates `docs/requirements` into an implementable design: architecture, technology, data, UI, behaviour, non-functional approach, errors, tests, and decisions.

**Requirements are the source of truth.** This design must satisfy them as written. It must not add login, roles, cloud, hosted or external network services, extra employee fields, search/export, custom visual polish, or permanent delete.

There is **no database**. Employee data is a local JSON file.

## Document map

| File | Title |
| --- | --- |
| `00-readme.md` | Index and conventions |
| `01-solution-overview.md` | What is being built and for whom |
| `02-architecture.md` | Components, layers, and data flows |
| `03-tech-stack.md` | Compatible tech stack and simple UI |
| `04-data-design.md` | Record shape, file schema, persistence |
| `05-ui-design.md` | Screens, actions, and layout |
| `06-behaviour-and-flows.md` | User journeys as system behaviour |
| `07-nfr-and-constraints.md` | How NFRs and constraints are met |
| `08-error-handling.md` | Validation, file, and save failures |
| `09-test-strategy.md` | Automated and manual verification |
| `10-traceability-matrix.md` | Requirement to design to test |
| `11-design-decisions.md` | Decisions and resolved open questions |

## ID conventions

| Prefix | Source |
| --- | --- |
| BO | Business objective (`03-business-objectives.md`) |
| FR | Functional requirement (`05-functional-requirements.md`) |
| BR | Business rule (`06-business-rules.md`) |
| NFR | Non-functional requirement (`09-non-functional-requirements.md`) |
| AC | Acceptance criterion (`11-acceptance-criteria.md`) |
| DD | Design decision introduced in this package |

Cite requirement IDs exactly as authored. Do not invent, merge, or split FRs.

## Compliance snapshot

| Constraint from requirements | Design response |
| --- | --- |
| Local machine only | Local ASP.NET Core app on localhost; HTML/CSS/Bootstrap UI; no cloud |
| No network or outside service | Localhost only; local JSON; Bootstrap files in the repo (no CDN) |
| No database | One local JSON file; no SQL Server, SQLite, or other DB |
| No login, roles, or permissions | Open the list page; full access |
| First name, last name, status only | No extra fields in UI or file |
| No permanent delete | Deactivate / reactivate only |
| Basic working UI | HTML/CSS/Bootstrap list and forms; no custom branding (DD-15) |
| Rejected save must not change the list | Validate before persist; show a message |

## Out of scope (must not be added by design)

- Extra employee fields (email, phone, title, photo)
- Security, login, roles, permissions
- Cloud, sync, external APIs, or multi-user sharing
- Database servers (SQL Server, MariaDB, SQLite as a product dependency)
- Advanced search, filters, export, reporting
- Permanent delete
- Custom visual design polish (branding, themes). Default Bootstrap layout is allowed (DD-15).
