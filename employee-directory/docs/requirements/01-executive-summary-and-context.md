# Executive Summary & Business Context

**Version:** 1.0  
**Status:** Approved

## Executive Summary

The Employee Directory is a **local web application** for capturing and managing a list of employees. Users interact with the application through a browser while all data and processing remain on their local machine.

The tool supports creating employee records, editing employee details, and changing employment status between Active and Inactive. There is no permanent delete — deactivation is the mechanism for removing employees from active use.

## Business Context

Organizations need a simple way to maintain an accurate roster of employees without relying on cloud services or complex enterprise systems. This application addresses that need for a **single user on a single machine** who needs to:

- Record who works for the organization
- Keep employee contact information up to date
- Mark employees as inactive when they leave, without losing historical records
- Reactivate employees when they return

The application is intentionally scoped as a lightweight, standalone tool — not an HR system, not multi-user, and not connected to external services.

## Scope Summary

| In scope | Out of scope |
|----------|--------------|
| Local web UI | Cloud hosting or sync |
| Create, edit, activate/deactivate | Permanent delete |
| Persistent local storage | Authentication and permissions |
| Single-user, single instance | Search, filter, reporting |
