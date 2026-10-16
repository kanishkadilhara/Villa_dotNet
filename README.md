# WhiteLagoon.Web

WhiteLagoon.Web is a villa-management and reservation UI project built with
ASP.NET Core MVC. It is being developed as a hands-on learning project for
understanding Clean Architecture, Entity Framework Core, relational data
modeling, MVC patterns, validation, and dependency boundaries in .NET.

The application currently provides a public villa showcase and administrative
CRUD screens for villas and villa numbers. Reservation and authentication
features are planned areas for future learning.

## Learning goals

This project is intended to help practice:

- Organizing a .NET solution into Domain, Application, Infrastructure, and
  Presentation layers
- Keeping core domain entities independent from infrastructure concerns
- Configuring dependency injection in ASP.NET Core
- Using Entity Framework Core with SQL Server
- Creating and applying EF Core migrations
- Modeling relationships and seed data
- Building MVC controllers, Razor views, and view models
- Validating user input with data annotations and custom rules
- Handling image uploads and serving static files
- Iterating toward a more complete Clean Architecture implementation

> **Current architecture note:** The solution contains the expected
> Clean Architecture projects, but this is a learning-in-progress codebase.
> Some application behavior currently lives in MVC controllers and directly
> uses `ApplicationDbContext`. Future iterations can move use cases,
> repositories, and application services into the Application layer.

## Features

### Villa management

- View the villa catalog on the home page
- View all villas in the management list
- Create a villa with:
  - Name
  - Description
  - Price per night
  - Square-meter size
  - Maximum occupancy
  - Optional image upload
- Edit villa details
- Delete villas
- Validate villa name, price, occupancy, and description
- Store uploaded images under `wwwroot/Images/VillaImage`

### Villa-number management

- Associate a physical villa number with a villa
- View villa numbers together with their related villa
- Create, edit, and delete villa numbers
- Validate that villa numbers are unique
- Store optional special details for each villa number

### Data and UI

- SQL Server persistence through Entity Framework Core
- EF Core migrations included in the Infrastructure project
- Seed data for three villas and three villa numbers
- Bootstrap-based Razor MVC interface
- Static files and local villa image support
- Development and production exception-handling configuration

## Technology stack

- .NET 11
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 10
- SQL Server
- Bootstrap
- Nullable reference types
- Implicit usings

## Solution structure

```text
WhiteLagoon.Web/
├── WhiteLagoon.Web.sln
├── WhiteLagoon.Domain/
│   ├── Entities/
│   │   ├── Villa.cs
│   │   └── VillaNumber.cs
│   └── WhiteLagoon.Domain.csproj
├── WhiteLagoon.Application/
│   └── WhiteLagoon.Application.csproj
├── WhiteLagoon.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs.cs
│   ├── Migrations/
│   └── WhiteLagoon.Infrastructure.csproj
├── WhiteLagoon.Web/
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── VillaController.cs
│   │   └── VillaNumberController.cs
│   ├── Models/
│   ├── ViewModels/
│   ├── Views/
│   ├── wwwroot/
│   ├── Program.cs
│   └── appsettings.json
└── README.md
```

### Layer responsibilities

| Layer | Responsibility |
| --- | --- |
| **Domain** | Core entities and domain validation rules. It should remain independent of databases and UI frameworks where possible. |
| **Application** | A place for use cases, interfaces, DTOs, and application-level business orchestration. This layer is intentionally kept as a learning target for future improvements. |
| **Infrastructure** | EF Core `ApplicationDbContext`, SQL Server configuration, seed data, and database migrations. |
| **Web** | ASP.NET Core MVC controllers, Razor views, view models, static files, dependency injection, and HTTP request handling. |

## Prerequisites

Install the following before running the application:

1. The .NET SDK version required by the project (`net11.0`)
2. SQL Server 2022 or a compatible SQL Server instance
3. Git
4. The EF Core CLI tool, if you want to manage migrations from the command
   line:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

If `dotnet-ef` is already installed, update it when necessary:

```bash
dotnet tool update --global dotnet-ef
```

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/kanishkadilhara/Villa_dotNet.git
cd Villa_dotNet
```

### 2. Configure the database connection

Do not commit real passwords or other secrets to Git. Configure the connection
string with .NET User Secrets, environment variables, or a local configuration
file that is excluded from source control.

For local development, the connection string has the following shape:

```text
Server=localhost,1433;Database=WhiteLagoonDb;User Id=Sa;Password=<your-password>;TrustServerCertificate=True;
```

The project reads the `DefaultConnection` entry from
`WhiteLagoon.Web/appsettings.json`.

For a safer local setup with User Secrets:

```bash
dotnet user-secrets init --project WhiteLagoon.Web
dotnet user-secrets set \
  "ConnectionStrings:DefaultConnection" \
  "Server=localhost,1433;Database=WhiteLagoonDb;User Id=Sa;Password=<your-password>;TrustServerCertificate=True;" \
  --project WhiteLagoon.Web
```

If a database password has ever been committed to a public or shared
repository, rotate it immediately and remove it from configuration history.

### 3. Restore dependencies

```bash
dotnet restore WhiteLagoon.Web.sln
```

### 4. Apply the database migrations

Run this from the repository root:

```bash
dotnet ef database update \
  --project WhiteLagoon.Infrastructure \
  --startup-project WhiteLagoon.Web
```

This creates or updates the `WhiteLagoonDb` database and applies the
migrations included in `WhiteLagoon.Infrastructure/Migrations`.

### 5. Build and run the application

```bash
dotnet build WhiteLagoon.Web.sln
dotnet run --project WhiteLagoon.Web
```

Open the HTTPS or HTTP URL printed by ASP.NET Core in the terminal. The
available screens include:

- `/` - Home page and villa showcase
- `/Villa` - Villa management list
- `/Villa/Create` - Create a villa
- `/VillaNumber` - Villa-number management list
- `/VillaNumber/Create` - Create a villa number

The exact local ports are defined by
`WhiteLagoon.Web/Properties/launchSettings.json`.

## Database model

### `Villas`

Stores the main villa information:

- `VillaId`
- `Name`
- `Description`
- `Price`
- `SquareMeters`
- `occupancy`
- `ImageUrl`
- `CreatedDate`
- `UpdatedDate`

### `VillaNumbers`

Stores individual villa numbers and their relationship to a villa:

- `Villa_Number` - primary key
- `VillaId` - foreign key to `Villas`
- `SpecialDetails`

The current seed data creates:

- Royal Villa - villa number `101`
- Premium Pool Villa - villa number `102`
- Luxury Pool Villa - villa number `103`

## EF Core migration workflow

After changing an entity or the database model, create a migration with:

```bash
dotnet ef migrations add DescribeYourChange \
  --project WhiteLagoon.Infrastructure \
  --startup-project WhiteLagoon.Web
```

Review the generated migration, then apply it:

```bash
dotnet ef database update \
  --project WhiteLagoon.Infrastructure \
  --startup-project WhiteLagoon.Web
```

When changing a model, keep seed data and migration history consistent.

## Development workflow

A typical development loop is:

```bash
dotnet restore
dotnet build WhiteLagoon.Web.sln
dotnet ef database update --project WhiteLagoon.Infrastructure --startup-project WhiteLagoon.Web
dotnet run --project WhiteLagoon.Web
```

Before opening a pull request:

- Confirm the solution builds successfully
- Review generated migrations before applying them
- Check that no passwords, API keys, or local-only files are staged
- Test create, edit, and delete flows for villas and villa numbers
- Test image upload behavior and invalid form submissions
- Confirm that database changes work from a clean local database

## Planned improvements

The following are useful next steps for continuing the Clean Architecture
learning journey:

- Move business use cases from controllers into the Application layer
- Add repository or query abstractions where they provide real value
- Add dependency-injection extension methods per layer
- Add automated unit and integration tests
- Add authentication and authorization for management screens
- Add villa availability and reservation workflows
- Add booking and guest entities
- Add asynchronous EF Core operations
- Add structured logging and global exception handling
- Add robust image validation, size limits, and storage abstraction
- Add pagination, search, and filtering to management screens
- Add CI build and test validation
- Use production secret storage and environment-specific configuration

## Contributing

This repository is primarily a personal learning project, but suggestions and
improvements are welcome. When contributing:

1. Create a focused branch.
2. Keep changes scoped to one feature or learning objective.
3. Follow the existing project and naming conventions.
4. Include migration changes when the data model changes.
5. Explain architectural decisions in the pull request.
6. Never commit secrets or production connection strings.

## License

No license has currently been specified for this repository. Until a license
is added, assume that the code is not licensed for reuse beyond the permissions
provided by the repository owner.
