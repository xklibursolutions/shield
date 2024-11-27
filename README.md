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

### Create a new database migration

1. Install the command-line tool:

   ```bash
   dotnet tool update --global dotnet-ef
   ```

2. Create a migration:

   ```bash
   dotnet ef migrations add MigrationName --project src/Shield.Infrastructure --startup-project src/Shield.Api
   ```

### Contributing

We welcome contributions! Please read our Contributing Guidelines for more details.

### License

This project is licensed under the MIT License. See the LICENSE file for details.

### Contact

For any questions or suggestions, please open an issue or contact us at <opencode@xklibursolutions.io>.
