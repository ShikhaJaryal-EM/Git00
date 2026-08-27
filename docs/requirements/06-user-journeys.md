# User Journeys

**Version:** 1.0  
**Status:** Approved

## UJ-1: View Employee List

**Trigger:** User opens the application.

1. Application runs locally in the browser.
2. User sees a list of all employees in alphabetical order.
3. Each row shows First Name, Last Name, Email, and an Active/Inactive badge.
4. Each row shows Edit + Deactivate, or Edit + Reactivate, depending on status.

**Outcome:** User has a complete view of the employee roster.

---

## UJ-2: Create Employee

**Trigger:** User wants to add a new employee.

1. User starts the create flow.
2. User enters First Name, Last Name, and Email.
3. User submits.
4. If validation fails, the system displays a clear error; user corrects and resubmits.
5. If validation passes, the employee is added with Active status.

**Outcome:** New employee appears in the list with Active status.

---

## UJ-3: Edit Employee

**Trigger:** User wants to update an employee's name or email.

1. User clicks Edit on an employee row (Active or Inactive).
2. User modifies First Name, Last Name, and/or Email and submits.
3. If validation fails, the system displays a clear error; user corrects and resubmits.
4. If validation passes, the employee record is updated. Status is unchanged.

**Outcome:** Employee details are updated; status remains the same.

---

## UJ-4: Deactivate Employee

**Trigger:** User wants to mark an employee as inactive.

1. User clicks Deactivate on an Active employee row.
2. System sets the employee's status to Inactive.

**Outcome:** Employee is inactive but still visible in the list.

---

## UJ-5: Reactivate Employee

**Trigger:** User wants to reactivate a previously deactivated employee.

1. User clicks Reactivate on an Inactive employee row.
2. System sets the employee's status to Active.

**Outcome:** Employee is active again.
