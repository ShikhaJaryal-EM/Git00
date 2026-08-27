package com.bootcamp.employeedirectory.employee.adapter.out.persistence;

import com.bootcamp.employeedirectory.employee.domain.EmployeeStatus;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "employee")
class EmployeeJpaEntity {
    @Id
    @Column(name = "email", length = 254, nullable = false)
    private String email;

    @Column(name = "first_name", length = 100, nullable = false)
    private String firstName;

    @Column(name = "last_name", length = 100, nullable = false)
    private String lastName;

    @Enumerated(EnumType.STRING)
    @Column(name = "status", length = 8, nullable = false)
    private EmployeeStatus status;

    protected EmployeeJpaEntity() {}

    EmployeeJpaEntity(String email, String firstName, String lastName, EmployeeStatus status) {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.status = status;
    }

    String getEmail() { return email; }
    String getFirstName() { return firstName; }
    String getLastName() { return lastName; }
    EmployeeStatus getStatus() { return status; }
}
