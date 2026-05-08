# Blog Web Application - ASP.NET Core MVC (.NET 10)

A scalable and responsive Blog Web Application built using ASP.NET Core MVC (.NET 10) following Clean Architecture principles and modern development practices.

---

##  Features

- Complete CRUD Operations for Blog Management
- ASP.NET Core MVC (.NET 10)
- Entity Framework Core (Code-First Approach)
- SQL Server Database Integration
- Repository Pattern & Dependency Injection
- Microsoft Identity Authentication & Authorization
- Role-Based Access Control
- Responsive UI using Bootstrap 5
- Cloudinary API Integration for Image Uploads
- Flora Rich Text Editor Integration
- Clean Architecture & Scalable Project Structure

---

##  Technologies Used

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Microsoft Identity
- Cloudinary API
- Flora Rich Text Editor

---

## Project Description

Developed a scalable and responsive Blog Web Application using ASP.NET Core MVC (.NET 10) following Clean Architecture principles and modern development practices.

Implemented complete CRUD operations using Entity Framework Core (Code-First Approach) with SQL Server for efficient database management.

Applied Repository Pattern and Dependency Injection to create a clean, maintainable, and loosely coupled architecture.

Integrated Microsoft Identity for secure authentication, user management, and role-based authorization.

Designed a modern and mobile-friendly user interface using Bootstrap 5 to improve user experience across devices.

Integrated Cloudinary API for cloud-based image upload and media management functionality.

Added Flora Rich Text Editor to enable advanced blog content creation and formatting features.

Focused on scalability, clean coding standards, and real-world .NET web development practices throughout the project.

---

##  Installation & Setup

### 1️. Clone Repository

```bash
git clone https://github.com/your-username/your-repository-name.git
```

### 2️. Navigate to Project Folder

```bash
cd your-repository-name
```

### 3️. Update Database Connection String

Add your SQL Server connection string inside:

```json
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BlogDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 4️. Apply Migrations

```bash
dotnet ef database update
```

### 5️. Run the Application

```bash
dotnet run
```

---

##  Authentication & Authorization

- User Registration & Login
- Role-Based Authorization
- Secure Authentication using Microsoft Identity

---

##  Cloudinary Integration

Used Cloudinary API for:

- Image Upload
- Image Storage
- Media Management

---

##  Rich Text Editor

Integrated Flora Rich Text Editor for:

- Blog Content Formatting
- Rich Text Writing Experience
- HTML Content Support

---


##  Developer

**Mubashar Yasin**  
Computer Science Student | .NET Web Developer

---

This project is created for learning and portfolio purposes.
