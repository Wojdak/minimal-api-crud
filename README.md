# Basic Minimal API CRUD

This project was created for learning purposes.

## Getting Started

1. **Configure AppSettings**
   - Open the `appsettings.json` file.
   - Modify the database connection string to point to your desired database server.
   - Update the "Jwt" section with your preferred issuer and audience values to match your URL and port.

2. **Update the Database**  
   Run the following command to apply migrations and update the database:
   ```sh
   dotnet ef database update

## Endpoints

1. **Driver**
- GET /drivers: Retrieve a list of all drivers.
- GET /drivers/{id}: Retrieve driver with the given ID.
- POST /drivers: Create a new driver.
- PUT /drivers/{id}: Update details of a specific driver by ID.
- DELETE /drivers{id}: Delete driver with the given ID.

2. **User**
- POST /register: Create a new user account with the role "Standard".  
  (Username: min. 4 characters) | (Password: min. 5 chars + 1 capital)
- POST /login: Login to a specific account and generate a JWT token used for authorization.

All endpoints from the first section are required to be authorized to test them.  
There are two accounts already in the database:
- Login: `standard_account` Password: `Test123` with the "Standard" role granting access to both GET endpoints.
- Login: `admin_account` Password: `Test123` with the "Administrator" role granting access to all available endpoints.
