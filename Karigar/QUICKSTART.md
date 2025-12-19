# Karigar - Quick Start Guide

## Prerequisites
- .NET 8.0 SDK installed
- SQL Server LocalDB (comes with Visual Studio) or SQL Server Express

## Quick Setup

1. **Open the project folder** in Visual Studio or VS Code

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Restore client libraries (Bootstrap, jQuery):**
   ```bash
   dotnet tool install -g Microsoft.Web.LibraryManager.Cli
   libman restore
   ```
   Or manually download Bootstrap 5.3.2 and jQuery 3.7.1 to `wwwroot/lib/`

4. **Update connection string** in `appsettings.json` if needed (default uses LocalDB)

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **Access the application:**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`
   - The database will be automatically created on first run

## Default Login Credentials

### Admin Account
- **Email:** admin@karigar.com
- **Password:** Admin@123
- **Access:** Full admin dashboard, user management, service provider approval

### Customer Account
- **Email:** customer@karigar.com
- **Password:** Customer@123
- **Access:** Browse services, submit requests, write reviews

### Service Provider Account
- **Email:** provider@karigar.com
- **Password:** Provider@123
- **Access:** Manage profile, services, and requests (already approved)

## Key Features Implemented

✅ **Customer Features:**
- Browse/search service providers by category and location
- View detailed provider profiles with ratings
- Submit service requests
- Track request status
- Submit reviews after service completion

✅ **Service Provider Features:**
- Create and manage business profile
- Add/edit services with pricing
- Accept/reject/reschedule requests
- Update request status (In Progress, Completed)
- View booking history

✅ **Admin Features:**
- Manage all users (suspend/activate)
- Approve/suspend/remove service providers
- Monitor all service listings
- Moderate reviews
- View platform metrics and analytics

## Testing the Application

1. **As Customer:**
   - Login with customer account
   - Browse service providers on homepage
   - Click "View Details" on any provider
   - Click "Request Service" on a service
   - Fill form and submit request
   - View "My Requests" to see status

2. **As Service Provider:**
   - Login with provider account
   - Go to Dashboard
   - Add new services in "Manage Services"
   - View and accept pending requests
   - Update request status to "In Progress" then "Completed"

3. **As Admin:**
   - Login with admin account
   - View dashboard metrics
   - Approve pending service providers
   - Monitor listings and reviews
   - Manage users

## Troubleshooting

**Database connection issues:**
- Ensure SQL Server LocalDB is installed
- Check connection string in `appsettings.json`
- Try changing to SQL Server Express if LocalDB doesn't work

**Bootstrap/jQuery not loading:**
- Run `libman restore` to download libraries
- Or manually add Bootstrap 5 and jQuery to `wwwroot/lib/`

**Build errors:**
- Run `dotnet clean` then `dotnet build`
- Ensure .NET 8.0 SDK is installed

## Project Structure

- `Controllers/` - All MVC controllers
- `Models/` - Entity models
- `Views/` - Razor views
- `Data/` - DbContext and seed data
- `ViewModels/` - View models for data transfer
- `wwwroot/` - Static files (CSS, JS, images)

## Notes

- The application uses Entity Framework Core with Code First approach
- Database is automatically created on first run
- Seed data includes sample users and a service provider
- All passwords follow the pattern: `[Role]@123`
- Service providers need admin approval before they can receive requests

