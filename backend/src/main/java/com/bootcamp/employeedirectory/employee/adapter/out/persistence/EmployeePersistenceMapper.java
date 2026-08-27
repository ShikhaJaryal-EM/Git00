package com.bootcamp.employeedirectory.employee.adapter.out.persistence;

import com.bootcamp.employeedirectory.employee.domain.Employee;
import org.springframework.stereotype.Component;

@Component
class EmployeePersistenceMapper {
    EmployeeJpaEntity toEntity(Employee employee) {
        return new EmployeeJpaEntity(
                employee.email(), employee.firstName(), employee.lastName(), employee.status());
    }

    Employee toDomain(EmployeeJpaEntity entity) {
        return Employee.restore(
                entity.getFirstName(), entity.getLastName(), entity.getEmail(), entity.getStatus());
    }
}
