Task Management API
Overview
This project is a simple Task Management API built with .NET 6+. It allows administrators and users to manage tasks and users with role-based access control. The application supports the creation, retrieval, updating, and deletion of tasks and users, with specific access restrictions based on user roles.

Features
Role-Based Access Control (RBAC):

Admin users can create, delete, and view all tasks and users.
Regular users can only access their own tasks and update task status.
Entities:

User: Represents a user with fields for Name and Role (Admin/User).
Task: Represents a task with fields for Title, Description, Status, and an assigned User.
Database:

Uses Entity Framework Core with an in-memory database for data storage.
Includes seeded data with two users (Admin and User) and three tasks.
Swagger Documentation:

API documentation is auto-generated using Swagger/OpenAPI for easy interaction with the API.
Getting Started
Prerequisites
.NET 6+ SDK installed: Download .NET SDK.
A code editor like Visual Studio or Visual Studio Code.
Clone the Repository
bash
Copy code
git clone https://github.com/nasreenmulla/Taskmanagementapi.git
cd TaskManagementAPI
Install Dependencies
Run the following command to restore required dependencies:

bash
Copy code
dotnet restore
Run the Application
To start the application, use:

bash
Copy code
dotnet run


API Endpoints
Users
GET /api/users
Retrieves a list of all users. (Admin only)

GET /api/users/{id}
Retrieves details of a specific user by ID.

POST /api/users
Creates a new user. (Admin only)

PUT /api/users/{id}
Updates user details. (Admin only)

DELETE /api/users/{id}
Deletes a user. (Admin only)

Tasks
GET /api/tasks
Retrieves a list of all tasks. (Admin only)

GET /api/tasks/{id}
Retrieves a specific task. Users can only access tasks assigned to them.

POST /api/tasks
Creates a new task. (Admin only)

PUT /api/tasks/{id}
Updates a task. Admins can update all fields, while users can only update task status.

DELETE /api/tasks/{id}
Deletes a task. (Admin only)

Role-Based Access Control
Admin: Can create, retrieve, update, and delete tasks and users.
User: Can only interact with tasks assigned to them (view, update status).
The role of the user is set through the request header as Role: Admin or Role: User.



bash
Copy code
# Example: Get a user by ID (for Admin only)
curl -X GET "http://localhost:5000/api/users/1" -H "Role: Admin"

# Example: Get tasks assigned to a specific user
curl -X GET "http://localhost:5000/api/tasks/1" -H "Role: User" -H "UserId: 2"
Unit Tests
The project includes unit tests that ensure core functionality. To run the tests, navigate to the test project folder and run:

bash
Copy code
dotnet test
Project Structure
graphql
Copy code
/TaskManagementAPI
├── /Controllers        # API Controllers for Users and Tasks
├── /Models            # Entity Models (User, Task)
├── /Data              # DbContext and Seed Data
├── /Services          # Business logic for Users and Tasks
├── /Tests             # Unit Tests
├── Program.cs         # Main program setup and middleware
├── TaskManagementAPI.csproj
License
This project is licensed under the MIT License - see the LICENSE file for details.

Conclusion
This project provides a comprehensive task management system with role-based access control. By following the instructions, you should be able to build, run, and interact with the API easily.
