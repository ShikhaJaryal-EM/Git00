package com.bootcamp.employeedirectory.employee.domain;

import static org.junit.jupiter.api.Assertions.*;

import org.junit.jupiter.api.Test;

class EmployeeTest {
    @Test
    void createNormalizesValuesAndStartsActive() {
        Employee employee = Employee.create("  Ada  ", "  Lovelace ", " ADA@Example.COM ");
        assertAll(
                () -> assertEquals("Ada", employee.firstName()),
                () -> assertEquals("Lovelace", employee.lastName()),
                () -> assertEquals("ada@example.com", employee.email()),
                () -> assertEquals(EmployeeStatus.ACTIVE, employee.status()));
    }

    @Test
    void namesPreserveCaseAndInternalSpaces() {
        Employee employee = Employee.create("Mary  Jane", "McDonald", "mary@example.com");
        assertEquals("Mary  Jane", employee.firstName());
        assertEquals("McDonald", employee.lastName());
    }

    @Test
    void rejectsBlankNames() {
        assertAll(
                () -> assertThrows(IllegalArgumentException.class,
                        () -> Employee.create(" ", "Valid", "a@example.com")),
                () -> assertThrows(IllegalArgumentException.class,
                        () -> Employee.create("Valid", "\t", "a@example.com")));
    }

    @Test
    void countsUnicodeCodePointsForNameLimit() {
        String oneHundredEmoji = "😀".repeat(100);
        assertDoesNotThrow(() -> Employee.create(oneHundredEmoji, "Valid", "a@example.com"));
        assertThrows(IllegalArgumentException.class,
                () -> Employee.create(oneHundredEmoji + "😀", "Valid", "a@example.com"));
    }

    @Test
    void rejectsInvalidEmail() {
        assertThrows(IllegalArgumentException.class,
                () -> Employee.create("A", "B", "not-an-email"));
    }

    @Test
    void updateDetailsPreservesStatus() {
        Employee inactive = Employee.create("A", "B", "a@example.com").deactivate();
        assertEquals(EmployeeStatus.INACTIVE,
                inactive.updateDetails("C", "D", "c@example.com").status());
    }

    @Test
    void lifecycleTransitionsAreGuarded() {
        Employee active = Employee.create("A", "B", "a@example.com");
        Employee inactive = active.deactivate();
        assertAll(
                () -> assertEquals(EmployeeStatus.INACTIVE, inactive.status()),
                () -> assertEquals(EmployeeStatus.ACTIVE, inactive.reactivate().status()),
                () -> assertThrows(IllegalStateException.class, active::reactivate),
                () -> assertThrows(IllegalStateException.class, inactive::deactivate));
    }
}
