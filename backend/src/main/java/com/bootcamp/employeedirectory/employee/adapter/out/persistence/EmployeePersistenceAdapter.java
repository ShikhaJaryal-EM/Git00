package com.bootcamp.employeedirectory.employee.adapter.out.persistence;

import com.bootcamp.employeedirectory.employee.application.port.out.EmployeeRepository;
import com.bootcamp.employeedirectory.employee.domain.Employee;
import java.util.List;
import java.util.Optional;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

@Repository
@Transactional(readOnly = true)
class EmployeePersistenceAdapter implements EmployeeRepository {
    private final SpringDataEmployeeRepository repository;
    private final EmployeePersistenceMapper mapper;

    EmployeePersistenceAdapter(
            SpringDataEmployeeRepository repository, EmployeePersistenceMapper mapper) {
        this.repository = repository;
        this.mapper = mapper;
    }

    @Override
    public List<Employee> findAllOrdered() {
        return repository.findAllOrdered().stream().map(mapper::toDomain).toList();
    }

    @Override
    public Optional<Employee> findByEmail(String email) {
        return repository.findById(Employee.normalizeEmail(email)).map(mapper::toDomain);
    }

    @Override
    public boolean existsByEmail(String email) {
        return repository.existsById(Employee.normalizeEmail(email));
    }

    @Override
    @Transactional
    public Employee save(Employee employee) {
        return mapper.toDomain(repository.save(mapper.toEntity(employee)));
    }

    @Override
    @Transactional
    public Employee replaceEmail(String currentEmail, Employee employee) {
        int updated = repository.replaceEmail(
                Employee.normalizeEmail(currentEmail),
                employee.email(),
                employee.firstName(),
                employee.lastName(),
                employee.status());
        if (updated != 1) {
            throw new IllegalStateException("Employee not found: " + currentEmail);
        }
        return employee;
    }
}
