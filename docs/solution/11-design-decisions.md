# Design Decisions and Open Questions

## Purpose

Record design decisions and resolve the open questions in `10-assumptions-constraints-risks-and-open-items.md`. Requirements stay unchanged; these decisions only fill gaps needed to implement.

---

## Decision log

| ID | Decision | Rationale | Refs | Docs |
| --- | --- | --- | --- | --- |
| DD-01 | Three layers (pages, application, persistence) in one local ASP.NET Core process | Meets local-only NFRs; browser talks only to localhost | NFR-1.1–NFR-1.3 | 01, 02 |
| DD-02 | JSON file under `%AppData%\EmployeeRecordsTool\` | Local persistence with no database or network | NFR-2.1–NFR-2.2 | 02, 03, 04 |
| DD-03 | HTML + CSS + Bootstrap 5 (local) UI; ASP.NET Core 8 Razor Pages backend; local JSON | Requested simple HTML UI; most compatible local backend | NFR-1, NFR-2, NFR-3 | `03-tech-stack.md` |
| DD-04 | Hidden system id (GUID) | Needed to edit/deactivate the right row when names are not unique | DD-12 | 04 |
| DD-05 | Full-file rewrite on each successful save | Simple and enough for a single-user list | NFR-2.1 | 04 |
| DD-06 | Insertion order; no sort UI | Sort is out of scope; resolves list-order question | Scope | 04, 05 |
| DD-07 | Visible Edit / Deactivate / Reactivate buttons on the HTML list | Clearer for a basic UI than hidden menus | NFR-3.1 | 05 |
| DD-08 | Create status defaults to Active; user may choose Inactive | BR-2.2 | BR-2.2 | 05, 06 |
| DD-09 | Separate Deactivate and Reactivate labels and availability | Matches BR-2.3 and BR-2.4 (not a generic toggle) | BR-2.3, BR-2.4 | 05 |
| DD-10 | Windows 10/11 as the v1 OS target | Requirements say “local machine”; Windows is the practical v1 desktop target | NFR-1.1 | 03, 07 |
| DD-11 | Missing file → empty list; corrupt file → error, keep file | First run vs protect existing data | NFR-4.1 | 08 |
| DD-12 | First name + last name are **not** unique | Requirements never require uniqueness; known limitation already states two people can look the same | Open questions | 04, 06 |
| DD-13 | Inactive employees **always** shown in the list | Journeys say the employee remains present after deactivate | User journeys, AC-3.3 | 05, 06 |
| DD-14 | `127.0.0.1` loopback is allowed; internet, CDN, and remote APIs are not | NFR-1.2 / AC-6.1 mean no network *connection to the outside*; scope forbids hosting/sync/external APIs, not a local browser talking to a local process | NFR-1.2, NFR-1.3, AC-6.1, AC-6.2 | 02, 03, 07 |
| DD-15 | Default Bootstrap 5 layout is the simple UI; no custom theme or branding | NFR-3.3 and scope forbid visual *polish*; Bootstrap is layout so HTML forms are readable | NFR-3.1, NFR-3.3 | 03, 05 |

## Resolved open questions

### Must first name plus last name be unique?

**Decision:** Not unique.

Create and edit do not reject a duplicate name. The hidden id distinguishes records (DD-04, DD-12).

**Why:** No business rule requires uniqueness. The requirements package already lists “two employees can look the same” as a known limitation. Adding uniqueness would invent a rule.

**Status:** Resolved

### Should the list show inactive employees by default?

**Decision:** Always show them with status Inactive (DD-13).

**Why:** Deactivate and reactivate journeys both say the employee remains in the list. Hiding inactive rows would break FR-5.1 for those records and would add a filter, which is out of scope.

**Status:** Resolved

### Is there a preferred order for the list?

**Decision:** Insertion order (order in the file). No sort control (DD-06).

**Why:** Search, sort, and extra list behaviour are out of scope. Insertion order is stable and needs no extra UI.

**Status:** Resolved

### Is status editable on create only, or also during edit?

**Decision:** Create may set Active or Inactive (default Active). After create, status changes only through Deactivate and Reactivate. The edit form has names only.

**Why:** FR-2.2 says edit can change first name and last name. Deactivate and reactivate are separate FRs with their own guards (BR-2.3, BR-2.4). Putting status on edit would duplicate those flows.

**Status:** Resolved

### Localhost HTTP vs “no network” / “no cloud”

**Decision:** The app may use `http://127.0.0.1` so a browser can show HTML pages. It must not use the internet, a CDN, cloud hosting, or a remote API (DD-14).

**Why:** HTML/CSS/Bootstrap needs a local host. Requirements forbid cloud and *outside* services and a *network connection* for using the tool. Loopback is on the same machine. Verify offline (MAT-08).

**Status:** Resolved

### Bootstrap vs “visual polish not required”

**Decision:** Use default Bootstrap 5 from files in the repo. No custom theme, branding, or extra UI kits (DD-15).

**Why:** The requested UI is simple HTML/CSS/Bootstrap. Scope’s “visual design polish” means branding, themes, and refined look — not a readable default layout.

**Status:** Resolved

## Risks from requirements — design response

| Risk | If it happens | Design response |
| --- | --- | --- |
| Names are not unique | Two employees can look the same | Allowed (DD-12); id used internally |
| Local storage is cleared | The list is lost | Accepted limitation; file lives only on the machine |
| Scope grows to extra fields or security | v1 takes longer | This package excludes those items; do not add them in implementation |

## Decisions needed (from requirements) — closed

| Decision | Options in requirements | Chosen |
| --- | --- | --- |
| Name uniqueness | Unique or not unique | Not unique (DD-12) |
| Inactive rows in the list | Always shown or hidden | Always shown (DD-13) |
| Default list order | None, name, or status | None / insertion order (DD-06) |
| Status on edit | Name only, or name and status | Name only (FR-2.2) |

## Deferred (must not ship in v1)

| Item | Why deferred |
| --- | --- |
| Additional employee fields | Out of scope |
| Search and sort options | Out of scope |
| Permanent delete | Out of scope |
| Visual design polish | NFR-3.3; default Bootstrap layout is in v1 (DD-15) |
| Database server | Not required; JSON file is the store (DD-02) |
| Multi-user or shared access | Out of scope |
| Login / roles | Out of scope |
| macOS / Linux | Not required; v1 is Windows (DD-10) |

No open design blockers remain. The stack, persistence, and NFR interpretations above are the ones to implement.
