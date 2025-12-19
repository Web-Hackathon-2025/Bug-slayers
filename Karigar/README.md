# Karigar - Hyperlocal Services Marketplace

A comprehensive web application for connecting customers with local service providers (Karigars) in their area.

## Features

### Customer Features
- Browse and search service providers by category and location
- View detailed service provider profiles with ratings and reviews
- Submit service requests
- Track request/booking status
- Submit reviews and ratings after service completion

### Service Provider Features
- Create and manage service provider profile
- Define services, pricing, and availability
- Accept, reject, or reschedule service requests
- View booking history
- Manage service listings

### Admin Features
- View and manage all users (customers and service providers)
- Approve, suspend, or remove service provider accounts
- Monitor and manage service listings
- View and moderate reviews or ratings
- View platform-level activity and usage metrics

## Technology Stack

- **.NET 8.0** - Backend framework
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core** - ORM for data access
- **ASP.NET Core Identity** - Authentication and authorization
- **SQL Server** - Database
- **Bootstrap 5** - UI framework

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full SQL Server instance)
- Visual Studio 2022 or VS Code

### Installation

1. Clone or extract the project to your local machine

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=KarigarDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. Restore client-side libraries (Bootstrap, jQuery):
   ```bash
   dotnet tool install -g Microsoft.Web.LibraryManager.Cli
   libman restore
   ```

5. Build and run the application:
   ```bash
   dotnet build
   dotnet run
   ```

6. Navigate to `https://localhost:5001` or `http://localhost:5000`

### Default Accounts

The application seeds the database with sample accounts:

- **Admin**: 
  - Email: `admin@karigar.com`
  - Password: `Admin@123`

- **Customer**: 
  - Email: `customer@karigar.com`
  - Password: `Customer@123`

- **Service Provider**: 
  - Email: `provider@karigar.com`
  - Password: `Provider@123`

## Project Structure

```
Karigar/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs
│   ├── AdminController.cs
│   ├── CustomerController.cs
│   ├── HomeController.cs
│   ├── ReviewController.cs
│   └── ServiceProviderController.cs
├── Data/                 # Data access layer
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
├── Models/               # Entity models
│   ├── ApplicationUser.cs
│   ├── Availability.cs
│   ├── Review.cs
│   ├── Service.cs
│   ├── ServiceProvider.cs
│   └── ServiceRequest.cs
├── Views/                # Razor views
│   ├── Account/
│   ├── Admin/
│   ├── Customer/
│   ├── Home/
│   ├── Review/
│   ├── ServiceProvider/
│   └── Shared/
├── ViewModels/           # View models
│   ├── ServiceProviderViewModel.cs
│   └── ServiceRequestViewModel.cs
└── wwwroot/              # Static files
```

## Service Request Workflow

1. **Requested** - Customer submits a service request
2. **Confirmed** - Service provider accepts the request
3. **InProgress** - Service provider marks request as in progress
4. **Completed** - Service is completed
5. **Cancelled** - Request is cancelled by customer or provider
6. **Rejected** - Service provider rejects the request

## Database

The application uses Entity Framework Core with Code First migrations. The database is automatically created on first run with seed data.

## Security

- Role-based access control (Admin, Customer, ServiceProvider)
- Password hashing using ASP.NET Core Identity
- Anti-forgery tokens on all forms
- Authorization attributes on controllers and actions

## License

This project is created for a web application development competition.

