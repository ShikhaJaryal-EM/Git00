package com.bootcamp.employeedirectory;

import static org.junit.jupiter.api.Assertions.*;

import com.bootcamp.employeedirectory.employee.application.port.out.EmployeeRepository;
import com.bootcamp.employeedirectory.employee.domain.Employee;
import java.util.List;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.test.context.DynamicPropertyRegistry;
import org.springframework.test.context.DynamicPropertySource;
import org.testcontainers.junit.jupiter.Container;
import org.testcontainers.junit.jupiter.Testcontainers;
import org.testcontainers.postgresql.PostgreSQLContainer;
import org.testcontainers.utility.DockerImageName;

@SpringBootTest
@Testcontainers(disabledWithoutDocker = false)
class EmployeePersistenceIT {
    @Container
    static final PostgreSQLContainer POSTGRES =
            new PostgreSQLContainer(DockerImageName.parse("postgres:17-alpine"))
                    .withDatabaseName("employee_directory")
                    .withUsername("test")
                    .withPassword("test");

    @DynamicPropertySource
    static void databaseProperties(DynamicPropertyRegistry registry) {
        registry.add("spring.datasource.url", POSTGRES::getJdbcUrl);
        registry.add("spring.datasource.username", POSTGRES::getUsername);
        registry.add("spring.datasource.password", POSTGRES::getPassword);
    }

    @Autowired JdbcTemplate jdbc;
    @Autowired EmployeeRepository repository;

    @BeforeEach
    void clearEmployees() {
        jdbc.execute("TRUNCATE TABLE employee");
    }

    @Test
    void flywayCreatesExactlyTheEmployeeTableAndHibernateValidatesIt() {
        Integer employeeTables = jdbc.queryForObject("""
                SELECT count(*) FROM information_schema.tables
                WHERE table_schema = 'public' AND table_name = 'employee'
                """, Integer.class);
        Integer columns = jdbc.queryForObject("""
                SELECT count(*) FROM information_schema.columns
                WHERE table_schema = 'public' AND table_name = 'employee'
                """, Integer.class);
        assertAll(() -> assertEquals(1, employeeTables), () -> assertEquals(4, columns));
    }

    @Test
    void adapterPersistsAndFindsByNormalizedEmail() {
        repository.save(Employee.create(" Ada ", " Lovelace ", " ADA@EXAMPLE.COM "));
        Employee found = repository.findByEmail(" Ada@Example.com ").orElseThrow();
        assertAll(
                () -> assertEquals("ada@example.com", found.email()),
                () -> assertEquals("Ada", found.firstName()));
    }

    @Test
    void normalizedEmailUniquenessRejectsCaseVariants() {
        Employee first = Employee.create("Ada", "One", " ADA@example.com ");
        Employee second = Employee.create("Ada", "Two", "ada@EXAMPLE.COM");
        insertRaw(first.email(), first.firstName(), first.lastName(), first.status().name());
        assertEquals(first.email(), second.email());
        assertThrows(DataIntegrityViolationException.class,
                () -> insertRaw(second.email(), second.firstName(), second.lastName(), second.status().name()));
    }

    @Test
    void databaseRejectsBlankFirstAndLastNames() {
        assertAll(
                () -> assertThrows(DataIntegrityViolationException.class,
                        () -> insertRaw("a@example.com", " ", "Valid", "ACTIVE")),
                () -> assertThrows(DataIntegrityViolationException.class,
                        () -> insertRaw("b@example.com", "Valid", "   ", "ACTIVE")));
    }

    @Test
    void databaseRejectsInvalidStatus() {
        assertThrows(DataIntegrityViolationException.class,
                () -> insertRaw("a@example.com", "A", "B", "PENDING"));
    }

    @Test
    void databaseRejectsUnnormalizedEmail() {
        assertThrows(DataIntegrityViolationException.class,
                () -> insertRaw("ADA@Example.com", "A", "B", "ACTIVE"));
    }

    @Test
    void adapterOrdersCaseInsensitivelyByLastNameFirstNameThenEmail() {
        repository.save(Employee.create("Zoe", "alpha", "z@example.com"));
        repository.save(Employee.create("amy", "Beta", "b@example.com"));
        repository.save(Employee.create("Amy", "alpha", "a@example.com"));
        List<String> emails = repository.findAllOrdered().stream().map(Employee::email).toList();
        assertEquals(List.of("a@example.com", "z@example.com", "b@example.com"), emails);
    }

    @Test
    void adapterAtomicallyReplacesEmailWithoutLeavingOldIdentity() {
        Employee original = repository.save(Employee.create("Ada", "Lovelace", "old@example.com"));
        Employee changed = original.updateDetails("Augusta", "Lovelace", "new@example.com");
        repository.replaceEmail(original.email(), changed);
        assertAll(
                () -> assertFalse(repository.existsByEmail("old@example.com")),
                () -> assertEquals(changed, repository.findByEmail("new@example.com").orElseThrow()));
    }

    private void insertRaw(String email, String firstName, String lastName, String status) {
        jdbc.update("""
                INSERT INTO employee (email, first_name, last_name, status)
                VALUES (?, ?, ?, ?)
                """, email, firstName, lastName, status);
    }
}
