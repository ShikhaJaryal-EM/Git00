# Permissions & Access Control

**Version:** 1.0  
**Status:** Approved

## Summary

**No permissions or access control model is required for this application.**

This is an intentional design decision, not an omission. The application is a single-user, local tool with no authentication layer.

## Requirements

| ID | Requirement |
|----|-------------|
| **PAC-1** | No user login or authentication |
| **PAC-2** | No roles (e.g. admin, viewer) |
| **PAC-3** | No permission checks on any operation |
| **PAC-4** | All users of the application (effectively one person on one machine) have full access to all operations: create, edit, deactivate, and reactivate |

## Rationale

The application runs locally on a single workstation for a single user. Adding authentication or role-based access would introduce unnecessary complexity without business value for this use case.

## Implications for Implementation

- No login screen
- No session management
- No authorization middleware
- No audit logging of who performed an action
