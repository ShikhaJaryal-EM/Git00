# UI Design

## Purpose

Define the screens and actions needed for a basic working interface. The UI is HTML, CSS, and Bootstrap. Visual polish and custom branding are not required (NFR-3.3). The user must complete create, edit, deactivate, and reactivate from the UI (NFR-3.2, AC-6.3).

## Requirement references

- FR-1.1–FR-5.2
- BR-2.2–BR-3.3
- NFR-3.1–NFR-3.3
- `08-user-journeys.md`

---

## Design goals

| Goal | Approach | Refs |
| --- | --- | --- |
| Basic and obvious | One main list; labelled buttons; simple forms | NFR-3.1 |
| Immediate feedback | List updates after each accepted change | FR-1.3, FR-2.3, FR-3.2, FR-4.2 |
| Clear rejection | Plain message when create or edit is rejected | BR-3.3 |
| No delete confusion | No delete or remove control | Scope |
| Status separate from edit | Deactivate / Reactivate on the list; edit names only | FR-2.2 |

## Screens

| Screen | Page | Role | Refs |
| --- | --- | --- | --- |
| Main list | `Pages/Index.cshtml` | Employee list + status actions | FR-5.1 |
| Create employee | `Pages/Create.cshtml` | Add first name, last name, status | FR-1.1, FR-1.2 |
| Edit employee | `Pages/Edit.cshtml` | Change first and last name | FR-2.1, FR-2.2 |

No login, no settings, no extra navigation.

## Main list

```
┌──────────────────────────────────────────────────────────────┐
│  Employee Records Tool                                       │
├──────────────────────────────────────────────────────────────┤
│  [ Create employee ]                                         │
├──────────────────────────────────────────────────────────────┤
│  First name    │  Last name     │  Status   │  Actions       │
│────────────────┼────────────────┼───────────┼────────────────│
│  Jane          │  Smith         │  Active   │ [Edit] [Deactivate] │
│  John          │  Doe           │  Inactive │ [Edit] [Reactivate] │
└──────────────────────────────────────────────────────────────┘
```

| Control | HTML / Bootstrap | Behaviour | Refs |
| --- | --- | --- | --- |
| List | `table.table` | Shows every saved record | FR-5.1, DD-13 |
| Columns | table headers | First name, last name, status | FR-5.2 |
| Create employee | `a.btn` / form | Opens the create page | FR-1.1 |
| Edit | `a.btn btn-secondary` | Opens the edit page for that row | FR-2.1 |
| Deactivate | `button` POST, only if Active | Marks inactive | FR-3.1, BR-2.3 |
| Reactivate | `button` POST, only if Inactive | Marks active | FR-4.1, BR-2.4 |

**DD-07:** Edit, Deactivate, and Reactivate are visible buttons, not hidden menus.

**DD-09:** The status action label is **Deactivate** or **Reactivate**, not a generic Toggle, so it matches BR-2.3 and BR-2.4.

No search, filter, sort, or delete controls.

## Create form

```
┌─────────────────────────────────────┐
│  Create employee                    │
├─────────────────────────────────────┤
│  First name:  [________________]    │
│  Last name:   [________________]    │
│  Status:      [ Active      ▼]      │
│                                     │
│  First name is required.            │  ← shown only after a rejected save
│                                     │
│              [ Cancel ]  [ Save ]   │
└─────────────────────────────────────┘
```

| Field | Notes | Refs |
| --- | --- | --- |
| First name | `input.form-control` — required | FR-1.2, BR-1.1 |
| Last name | `input.form-control` — required | FR-1.2, BR-1.2 |
| Status | `select.form-select` — Active or Inactive; default Active | BR-2.1, BR-2.2, DD-08 |
| Save | `button.btn` — validate then persist | FR-1.3 |
| Cancel | Link back to the list; no write | — |

**DD-08:** Status defaults to Active. The user may choose Inactive before save (BR-2.2).

## Edit form

```
┌─────────────────────────────────────┐
│  Edit employee                      │
├─────────────────────────────────────┤
│  First name:  [________________]    │
│  Last name:   [________________]    │
│                                     │
│  Last name is required.             │
│                                     │
│              [ Cancel ]  [ Save ]   │
└─────────────────────────────────────┘
```

Names are pre-filled. **Status is not on this form.** Status changes use Deactivate / Reactivate on the list (FR-2.2).

## Validation feedback

| Condition | UI | Refs |
| --- | --- | --- |
| Missing or whitespace-only first or last name | `text-danger` or `alert alert-danger` on the same page | BR-1.4, BR-3.3 |
| Rejected create | List unchanged | AC-1.3, AC-1.4, BR-3.4 |
| Rejected edit | Saved name unchanged | AC-2.3, AC-2.4, BR-3.4 |

Messages use plain language, for example “First name is required.”

## Navigation

```
Start local app → open http://127.0.0.1 → Main list
              ├── Create employee → Save or Cancel → Main list
              ├── Edit → Save or Cancel → Main list
              ├── Deactivate → stay on Main list
              └── Reactivate → stay on Main list
```

## UI exclusions

| Element | Why |
| --- | --- |
| Login | Out of scope |
| Delete / Remove | Out of scope |
| Search / filter / sort | Out of scope |
| Manual Save for the whole app | Each accepted change persists immediately |
| Employee id field | Internal only (DD-04) |
| Status on edit | FR-2.2 |
| Theming / branding | NFR-3.3; Bootstrap default layout only |
