# SHIELD: Secure Hub for Identity and Entry Login Distribution

## Overview

SHIELD is a comprehensive identity server built on .NET Core, designed to manage user authentication, access control, and permissions across various systems and applications. It leverages .NET Core Identity, Entity Framework, and a SQLite database, following the Domain-Driven Design (DDD) pattern. The project also includes Razor Pages for essential user interactions.

## Features

- **Multi-Factor Authentication (MFA)**
- **Single Sign-On (SSO)**
- **Role-Based Access Control (RBAC)**
- **Attribute-Based Access Control (ABAC)**
- **Granular and Dynamic Permissions**
- **Audit Logs and Anomaly Detection**
- **API Support for Integration**
- **Razor Pages for Login, Profile, Registration, and Management**

## Architecture

The project is structured into several layers to promote separation of concerns and maintainability:

- **Presentation Layer:** API Gateway, Web Application, and Razor Pages
- **Application Layer:** Business logic and orchestration
- **Domain Layer:** Core business entities and rules
- **Infrastructure Layer:** Data access and identity management
- **Cross-Cutting Concerns:** Logging, security, and exception handling

```text
src/
├── Shield.Api/
│   ├── Configuration/
│   ├── Features
│   └── Program.cs
├── Shield.Application/
├── Shield.Domain/
│   ├── Entities/
│   ├── Enmums/
├── Shield.Infrastructure/
│   ├── Repositories/
│   ├── Identity/
│   ├── Migrations/
│   └── Services/
├── Shield.Web/
│   ├── Pages/
│   │   ├── Account/
|   |   ├── _ViewImports.cshtml
│   |   ├── _ViewStart.cshtml
│   |   └── wwwroot/
│   └── Program.cs
└── Shield.CrossCutting/
    ├── Configuration/
    ├── DTOs/
    ├── Logging/
    ├── Security/
    ├── Services/
    └── ExceptionHandling/
```

## Getting Started

### Prerequisites

- .NET Core SDK

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/xklibursolutions/shield.git
   cd shield
   ```

2. Install dependencies:

   ```bash
   dotnet restore
   ```

3. Update the configuration files with your database connection strings and other settings.

### Running the Application

1. Run the application:

   ```bash
   dotnet run --project src/Shield.Api
   ```

### Contributing

We welcome contributions! Please read our Contributing Guidelines for more details.

### License

This project is licensed under the MIT License. See the LICENSE file for details.

### Contact

For any questions or suggestions, please open an issue or contact us at <opencode@xklibursolutions.io>.
