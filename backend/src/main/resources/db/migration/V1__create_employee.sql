CREATE TABLE employee (
    email VARCHAR(254) PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    status VARCHAR(8) NOT NULL,
    CONSTRAINT ck_employee_email_normalized
        CHECK (email = lower(btrim(email)) AND btrim(email) <> ''),
    CONSTRAINT ck_employee_first_name_nonblank CHECK (btrim(first_name) <> ''),
    CONSTRAINT ck_employee_last_name_nonblank CHECK (btrim(last_name) <> ''),
    CONSTRAINT ck_employee_status CHECK (status IN ('ACTIVE', 'INACTIVE'))
);
