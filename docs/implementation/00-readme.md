# Implementation Package — Employee Records Tool

## Purpose

This package is the **how we will build it in phases** for v1. Requirements (`docs/requirements`) are the source of truth. Solution (`docs/solution`) is the design to implement. This package does not change FRs, add out-of-scope features, or introduce a database, login, cloud, or extra employee fields.

## Document map

| File | Role |
| --- | --- |
| `00-readme.md` | This index |
| `implementation-plan.md` | Phase index (ID, name, FR mapping, links) |
| `CANONICAL-PHASE-SHAPE.md` | Mandatory phase structure |
| `phases/PHASE-001.md` … `PHASE-008.md` | One detailed spec per phase |
| `traceability.md` | Requirement → phase → test mapping |
| `phase-status.md` | Phase execution tracking |
| `bugs-and-issues-log.md` | Bugs found during phase verification (do not fix at log time) |

## Conventions

- **Phase IDs** are sequential (`PHASE-001` … `PHASE-008`). Bug-fix phases use `PHASE-BUG-nnn` when added later.
- **FR Mapping** uses IDs and names **exactly** as authored in `docs/requirements/05-functional-requirements.md`.
- Foundational phases state `FR Mapping: None (foundational)`.
- Persistence is a **local JSON file**. There is **no database**.
- UI is **HTML + CSS + Bootstrap 5 (local files)** on **ASP.NET Core 8 Razor Pages**, bound to `127.0.0.1` only.
- Work **one phase file at a time**. Keep ID, name, and FR mapping identical in the index, the phase file, `CANONICAL-PHASE-SHAPE.md`, `traceability.md`, and `phase-status.md`.

## Phase order

Foundation → JSON store → service create/list → service edit/status → list UI → create/edit UI → status actions UI → persistence and local-only proof.
