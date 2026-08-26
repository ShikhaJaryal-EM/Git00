# User Journeys

**Version:** 1.0  
**Status:** Approved

Step-by-step flows for the primary user persona ([Office Administrator](02-stakeholders-and-personas.md)).

---

## UJ-1: View Employee List

**Trigger:** User opens the application.

1. Application starts locally and opens in the browser.
2. User sees a list of all employees sorted alphabetically.
3. Each row shows First Name, Last Name, Email, and an Active/Inactive badge.
4. Each row shows context-appropriate actions (Edit + Deactivate, or Edit + Reactivate).

**Outcome:** User has a complete view of the employee roster.

---

## UJ-2: Create Employee

**Trigger:** User wants to add a new employee.

1. User initiates the create flow (form, modal, or dedicated view).
2. User enters First Name, Last Name, and Email.
3. User submits the form.
4. **If validation fails** (empty name, invalid email, duplicate email): system displays a clear error; user corrects and resubmits.
5. **If validation passes:** employee is added with Active status; list refreshes to include the new employee.

**Outcome:** New employee appears in the list with Active status.

---

## UJ-3: Edit Employee

**Trigger:** User wants to update an employee's name or email.

1. User clicks **Edit** on an employee row (Active or Inactive).
2. Form opens pre-filled with current First Name, Last Name, and Email.
3. User modifies one or more fields and submits.
4. **If validation fails** (empty name, invalid email, duplicate email): system displays a clear error; user corrects and resubmits.
5. **If validation passes:** employee record is updated; list refreshes. Status is unchanged.

**Outcome:** Employee details are updated; status remains the same.

---

## UJ-4: Deactivate Employee

**Trigger:** User wants to mark an employee as no longer active.

1. User clicks **Deactivate** on an Active employee row.
2. System sets the employee's status to Inactive.
3. List refreshes: badge changes to Inactive; row actions change to Edit + Reactivate.

**Outcome:** Employee is inactive but still visible in the list.

---

## UJ-5: Reactivate Employee

**Trigger:** User wants to restore a previously deactivated employee.

1. User clicks **Reactivate** on an Inactive employee row.
2. System sets the employee's status to Active.
3. List refreshes: badge changes to Active; row actions change to Edit + Deactivate.

**Outcome:** Employee is active again.
