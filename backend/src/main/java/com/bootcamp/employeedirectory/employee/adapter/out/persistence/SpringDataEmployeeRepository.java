package com.bootcamp.employeedirectory.employee.adapter.out.persistence;

import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.data.repository.Repository;

interface SpringDataEmployeeRepository extends Repository<EmployeeJpaEntity, String> {
    java.util.Optional<EmployeeJpaEntity> findById(String email);

    boolean existsById(String email);

    EmployeeJpaEntity save(EmployeeJpaEntity employee);

    @Query("""
            select e from EmployeeJpaEntity e
            order by lower(e.lastName), lower(e.firstName), lower(e.email)
            """)
    List<EmployeeJpaEntity> findAllOrdered();

    @Modifying(clearAutomatically = true, flushAutomatically = true)
    @Query("""
            update EmployeeJpaEntity e
            set e.email = :newEmail, e.firstName = :firstName,
                e.lastName = :lastName, e.status = :status
            where e.email = :currentEmail
            """)
    int replaceEmail(
            @Param("currentEmail") String currentEmail,
            @Param("newEmail") String newEmail,
            @Param("firstName") String firstName,
            @Param("lastName") String lastName,
            @Param("status") com.bootcamp.employeedirectory.employee.domain.EmployeeStatus status);
}
