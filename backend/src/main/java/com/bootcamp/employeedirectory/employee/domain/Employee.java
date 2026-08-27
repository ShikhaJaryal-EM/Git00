package com.bootcamp.employeedirectory.employee.domain;

import java.util.Objects;
import java.util.regex.Pattern;

public record Employee(String firstName, String lastName, String email, EmployeeStatus status) {
    public static final int MAX_NAME_CODE_POINTS = 100; // OI-03 pending default
    private static final int MAX_EMAIL_LENGTH = 254;
    private static final Pattern EMAIL = Pattern.compile("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");

    public Employee {
        firstName = normalizeName(firstName, "firstName");
        lastName = normalizeName(lastName, "lastName");
        email = normalizeEmail(email);
        status = Objects.requireNonNull(status, "status must not be null");
    }

    public static Employee create(String firstName, String lastName, String email) {
        return new Employee(firstName, lastName, email, EmployeeStatus.ACTIVE);
    }

    public static Employee restore(
            String firstName, String lastName, String email, EmployeeStatus status) {
        return new Employee(firstName, lastName, email, status);
    }

    public Employee updateDetails(String firstName, String lastName, String email) {
        return new Employee(firstName, lastName, email, status);
    }

    public Employee deactivate() {
        requireStatus(EmployeeStatus.ACTIVE, "Only an active employee can be deactivated");
        return new Employee(firstName, lastName, email, EmployeeStatus.INACTIVE);
    }

    public Employee reactivate() {
        requireStatus(EmployeeStatus.INACTIVE, "Only an inactive employee can be reactivated");
        return new Employee(firstName, lastName, email, EmployeeStatus.ACTIVE);
    }

    public static String normalizeEmail(String value) {
        Objects.requireNonNull(value, "email must not be null");
        String normalized = value.trim().toLowerCase(java.util.Locale.ROOT);
        if (normalized.isEmpty() || normalized.length() > MAX_EMAIL_LENGTH
                || !EMAIL.matcher(normalized).matches()) {
            throw new IllegalArgumentException("email must be a valid address of at most 254 characters");
        }
        return normalized;
    }

    private static String normalizeName(String value, String field) {
        Objects.requireNonNull(value, field + " must not be null");
        String normalized = value.trim();
        if (normalized.isEmpty()) {
            throw new IllegalArgumentException(field + " must not be blank");
        }
        if (normalized.codePointCount(0, normalized.length()) > MAX_NAME_CODE_POINTS) {
            throw new IllegalArgumentException(field + " must contain at most 100 Unicode characters");
        }
        return normalized;
    }

    private void requireStatus(EmployeeStatus expected, String message) {
        if (status != expected) {
            throw new IllegalStateException(message);
        }
    }
}
