# Fyter

Welcome to **Fyter**, an open-source .NET Blazor Server web application dedicated to EA Sports UFC players. This application lets you explore, add, edit, and keep track of UFC fighters in one place—all while leveraging community contributions to ensure fighter data stays accurate and up-to-date.

## About this Project

**Fyter** aims to centralize EA UFC 5 fighters stats information. Players can search for fighters by name, update their stats, request to add new fighters, and even flag outdated information.

## Roles & Access

This project implements role-based access to manage user permissions:

-   **Admin**
    -   Manage user accounts, assign roles, and review permission requests.
    -   Full permission to add, edit, or delete fighters and manage the application.

-   **FighterEditor**
    -   Add new fighters and edit existing fighters.
    -   Mark or resolve outdated stats.
    -   Ideal for active contributors.

-   **Viewer (Default)**
    -   View fighter stats, search, and filter the roster.
    -   Cannot modify or add data.
    -   Perfect for browsing data without making changes.

Users can request roles by visiting their `/Permissions` page.

## Getting Started

### Prerequisites

1.  [.NET 8 SDK or later](https://dotnet.microsoft.com/download)
2.  A supported editor or IDE, such as Visual Studio, Visual Studio Code, or JetBrains Rider.
3.  (Optional) SQLite CLI tools if you want to inspect the database manually.
### Installation and Setup

1.  **Clone the Repository**

    ```bash
    git clone https://github.com/breakingram/Fyter.git
    cd Fyter
    ```

2.  **Navigate to the WebApp Directory**
    ```bash
    cd Fyter.WebApp
    ```

3.  **Configure `appsettings.json` (Optional)**

    -   By default, **Fyter.WebApp** project uses two local SQLite databases:
        -   `IdentityDB` (for user/role management)
        -   `FyterDB` (for fighter data)
    -   If you would like to customize the connection strings, open the appsettings.json file and update the following entries:
        ```json
        {
          "ConnectionStrings": {
            "IdentityDB": "Data Source=identitydb.db;Cache=Shared",
            "FyterDB": "Data Source=FyterDB.db;Cache=Shared"
          },
          "SendGrid": {
            "ApiKey": "",
            "FromEmail": "",
            "FromName": ""
          },
          "Logging": {
            "LogLevel": {
              "Default": "Information",
              "Microsoft.AspNetCore": "Warning"
            }
          },
          "AllowedHosts": "*"
        }
        ```

        > **Note**: The `SendGrid` settings are required only if you want to test email notifications for account confirmations or password resets. Otherwise, you can leave these empty.

4.  **Restore, Build, and Run**

    ```bash
    dotnet restore
    dotnet build
    dotnet run # or 'dotnet watch' (for hot reload)
    ```

    The application will start up, and you should see something like:
    `Now listening on: https://localhost:5001`

    Navigate to https://localhost:5001  URL in your browser to use the application locally.


### Database Migrations (`migrate.sh`)

This project includes a **Bash script** named `migrate.sh`. This script allows you to:

-   Apply migrations for **both** DbContexts at once.
-   Run migrations for a **specific** DbContext by passing parameters.

**Usage** example:
```bash
# Make the script executable (if not already)
chmod +x migrate.sh

# Run migrations for both the IdentityDB and FyterDB contexts
./migrate.sh

# OR run migrations for only the FyterDB context
./migrate.sh -c UfcSqliteContext
```

> **Tip**: Review or modify `migrate.sh` to suit your environment’s needs.

If you still prefer manual control:

```bash
# From the Fyter.WebApp directory
dotnet ef database update --context ApplicationDbContext
```

```bash
# From the Fyter.Plugins.EFCoreSqlite directory
dotnet ef database update --context UfcSqliteContext
```

### Development Data Seeding

When the application runs in **Development** environment, it can optionally seed some sample data. This is controlled by the `SeedData` key in `appsettings.json`. For example:
```json
{
  "SeedData": true,
  "ConnectionStrings": { ... }
}
```

If `SeedData` is `true`, the project will insert demo fighters, user accounts and other test data. This helps you quickly explore the features without starting from a blank database.

### Development Environment Demo Accounts

When running the application in **Development** mode with `SeedData` enabled, two demo user accounts are automatically created:

- **Admin Account**
  - **Username**: `admin`
  - **Email**: `admin@admin.com`
  - **Password**: `AdminPassword123!`
  - **Roles**: `Admin`, `Developer`

- **Normal User Account**
  - **Username**: `user`
  - **Email**: `user@example.com`
  - **Password**: `UserPassword123!`
  - **Role**: `Viewer`

This setup allows you to test different role-based scenarios—like adding fighters, marking stats outdated, or simply viewing fighter data—without creating new users from scratch.
