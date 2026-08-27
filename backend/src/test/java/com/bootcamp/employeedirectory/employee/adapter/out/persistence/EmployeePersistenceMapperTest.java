package com.bootcamp.employeedirectory.employee.adapter.out.persistence;

import static org.junit.jupiter.api.Assertions.assertEquals;

import com.bootcamp.employeedirectory.employee.domain.Employee;
import org.junit.jupiter.api.Test;

class EmployeePersistenceMapperTest {
    private final EmployeePersistenceMapper mapper = new EmployeePersistenceMapper();

    @Test
    void roundTripPreservesDomainValues() {
        Employee employee = Employee.create("Ada", "Lovelace", "ada@example.com").deactivate();
        assertEquals(employee, mapper.toDomain(mapper.toEntity(employee)));
    }
}
