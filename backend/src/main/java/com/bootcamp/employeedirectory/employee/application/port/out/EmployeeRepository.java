package com.bootcamp.employeedirectory.employee.application.port.out;

import com.bootcamp.employeedirectory.employee.domain.Employee;
import java.util.List;
import java.util.Optional;

public interface EmployeeRepository {
    List<Employee> findAllOrdered();

    Optional<Employee> findByEmail(String email);

    boolean existsByEmail(String email);

    Employee save(Employee employee);

    Employee replaceEmail(String currentEmail, Employee employee);
}
