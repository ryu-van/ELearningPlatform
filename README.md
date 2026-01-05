# E-learning Platform

An online learning platform that supports course management, lesson and video content, student enrollment, examinations (multiple-choice and essay), grading, and role-based access control.

## Table of Contents
- Project Name & Description
- Objectives
- User Roles
- Core Features
- Business Rules
- API Response Format
- Pagination & Search
- Tech Stack
- Architecture
- Security & Authorization
- Getting Started
- Project Structure
- Related Components

## Project Name & Description
- E-learning Platform for managing courses, structured learning content, exams, and role-based operations.

## Objectives
- Provide a standardized online learning environment.
- Enable instructors to manage content and grade assignments.
- Allow learners to access courses, take exams, and track progress.

## User Roles
- Admin: system administration, role/permission management, content approval, and user management.
- Instructor: create and manage courses, chapters, lessons, videos; grade essay submissions.
- Student: enroll in courses, follow learning paths, and take exams.

## Core Features
- Manage courses, chapters, lessons, and videos with enable/disable visibility.
- Media upload (images/videos) with unique filenames to prevent naming conflicts.
- Course enrollment with pagination, search, and status filtering.
- Exams with multiple-choice and essay questions; manual essay grading by instructors/admins.
- Unified search, filter, and pagination across major entities.

## Business Rules
- Visibility: only active entities are visible to end users.
- Media Upload: filenames must be unique (GUID-based) to avoid storage conflicts.
- Essay Grading: only authorized roles (Instructor/Admin) can grade; correctness can be determined via a score threshold (e.g., 50%).
- Content Hierarchy: videos → lessons → chapters → courses.
- Enrollment: a student may enroll in multiple courses; supports keyword and status search.

## API Response Format
- Unified structure: success, message, data, errors.
- Example:

```json
{
  "success": true,
  "message": "Success",
  "data": { "id": 1, "title": "Video 1" },
  "errors": []
}
```

- Reference: [ApiResponse.cs]

## Pagination & Search
- Standard fields: currentPage, pageSize, totalItems, data.
- Keyword search and status-based filtering across modules (User, Video, Enrollment, etc.).
- Reference: [EnrollmentRepository.cs]

## Tech Stack
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (Code-First), LINQ
- AutoMapper (DTO ↔ Model)
- Media Upload Service (Cloud/Local) with unique filename generation
- RBAC (Role-Based Access Control) at controller layer

## Architecture
- Layered: Controller → Service → Repository → Model/DTO
- Mapping via AutoMapper: [MappingProfile.cs]
- Principles:
  - Business logic in Service
  - Data access in Repository
  - Controllers handle HTTP

## Security & Authorization
- Role-based authorization: Admin, Instructor, Student.
- Sensitive endpoints (essay grading, content administration) protected by roles/permissions.
- Secrets are not committed; use appsettings and environment variables.

## Getting Started
- Requirements: .NET SDK 8, database (SQL Server/LocalDB), upload configuration (if using cloud).
- Configuration: update appsettings.json (connection strings, upload settings).
- Build:

```bash
dotnet build
```

- Run:

```bash
dotnet run
```

- Swagger (if enabled): http://localhost:<port>/swagger

## Project Structure
- Controllers: API entry points per module.
- Services: business logic and orchestration.
- Repositories: EF Core data access.
- DTOs: request/response contracts.
- Models: domain entities.
- Mappings: AutoMapper configurations.
- Helpers / Exceptions: shared utilities and exception handling.

## Related Components
- Unique filename upload: [UploadService.cs]
- Essay grading: [ExamAttemptController.cs]