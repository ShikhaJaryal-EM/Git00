# 06 — Cross-Cutting Concerns

## Validation

Validation is layered:

1. Frontend provides immediate required/email feedback.
2. Jakarta Bean Validation rejects invalid request DTOs.
3. Application/domain code enforces trimmed nonblank names, normalized email, uniqueness, and valid status transitions (`BR-3`–`BR-12`).
4. PostgreSQL constraints protect persisted invariants.

The backend is authoritative. Names are trimmed but internal spaces and case are preserved. Email is trimmed and lowercased before lookup or save.

## Error handling

A `@RestControllerAdvice` maps validation exceptions to 400, missing employees to 404, uniqueness/state conflicts to 409, and unexpected exceptions to 500. All errors use the schema in [API design](04-api-design.md). User messages are actionable; internal exception details are logged but never returned.

## Logging

Use Log4j 2 and exclude Spring Boot's default Logback starter to avoid multiple bindings. Log to stdout for Docker collection:

- INFO: startup/shutdown, completed mutations, request method/path/status/duration.
- WARN: 400/404/409 outcomes with safe identifiers.
- ERROR: unexpected failures with stack trace and correlation ID.
- Never log request bodies or full email addresses; use a masked or one-way-derived value.

An HTTP filter accepts or generates `X-Correlation-ID`, places it in Log4j Thread Context, returns it in the response, and clears context after every request.

## Security posture

Authentication, roles, and permission checks are intentionally absent (`PAC-1`–`PAC-3`). This does not imply internet-safe deployment: ports bind for local use, credentials are local configuration, Swagger is local, and errors disclose no internals. CORS permits only the configured local frontend origin.

## Configuration

Runtime values come from environment variables with Compose defaults: database URL/name/user/password, frontend API base URL, allowed origin, and log level. Secrets are not committed. Spring profiles separate local runtime and tests.

## API documentation

Springdoc generates OpenAPI 3 documentation from the implemented controllers and DTO annotations. Operation descriptions include requirement IDs, status codes, validation, and examples. Swagger UI provides local interactive inspection.

## Reliability and performance

Mutations are transactional; database constraints prevent duplicate records. No cache is used, avoiding stale local state. A health endpoint reports backend liveness and database readiness for Compose health checks. Because this is one local user and one instance (`A-1`, `A-2`), distributed locking, rate limiting, and horizontal scaling are out of scope.
