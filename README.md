# TransApi
ASP.NET Core Web API created to manage a simple logistics platform with make use of the Onion Architecture and DDD practices.

Swagger to review on: http://transapitom-001-site1.atempurl.com
# Features
* .NET 6
* Onion Architecture
* CRUD Operations
* DDD
* CQS
* Entity Framework 
* Microsoft SQL Server
* JWT
* AutoMapper
* SeriLog
* Swagger
* SMTP
# ENDPOINTS
## Users
* GET /users - Retrieve all users.
* GET /users/userId - Retrieve a user by their ID.
* GET /users/email - Retrieve a user by email.
* POST /users/register - Register a new user.
* DELETE /users/userId - Remove a user.
## Account
* POST users/account/login - Logs in the user and returns an access token.
* PUT users/account/password - User password change.
* DELETE /users/account/me - Deletion of account by user.
## Companies
* GET /companies - Retrieve all companies.
* GET /companies/companyid - Retrieve a comapny by their ID.
* POST /companies/ - Create a new company.
* DELETE /companies/companyId - Remove a company.
## Companies orders
* GET /companies/orders - Retrieve all orders for company.
* GET /companies/oders/id - Retrieve a comapny order by order ID.
* POST /companies/orders - Create a new order.
* PUT /companies/orders/id/paid - Updates payment status.
* DELETE /companies/orders/id - Remove a order by order ID.
## Companies Driver
* GET /companies/drivers - Retrieve all drivers for company.
* POST /companies/drivers - Create a new driver.
* DELETE /companies/drivers/driverId - Remove a driver.
## Companies Vehicle
* GET /companies/vehicles - Retrieve all vehicles for company.
* POST /companies/vehicles - Create a new vehicle.
* DELETE /companies/drivers/vehicles - Remove a vehicle.

# JWT Authorization
## UsersController

Before receiving a JWT token, the user must create an account.
* POST ```/users/register```
  
  * Create new user.
  * POST HTTP Example: ```https://domainName/users/registers```
  * Request Body Example:
  ```
  {
    "email": "test@emai.com",
    "password": "secret",
    "username": "Test",
    "fullname": "Test",
    "role": "user"
  }
  ```
  * Response: ```201 Created```
    
## AccountController

The AccountController contains the login APIs we are using to get the JWT token authentication.  
* POST ```/account/login```
  
  * Returns the JWT token after the user enters their email and password.
  * POST HTTP Example: ```https://domainName/account/login```
  * Request Body Example:
  ```
  {
    "email": "test@email.com",
    "password": "secret"
  }
  ```
  * Response Example:
  ```
  {
    "token":                       
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlMWIxYmQ3OC05NmJmLTQ3Y2YtOTZjMi1hNWNjOWRkYTg2MjYiLCJ1bmlxdWVfbmFtZSI6ImUxYjFiZDc4LTk2YmYtNDdjZi05NmMyLWE1Y2M5ZGRhODYyNiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5
    jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJuYmYiOjE2OTg3NDU2ODQsImV4cCI6MTY5ODgzMjA4NCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI5NCJ9.9tcOlWjyLirae9Dx2VZnEzZEpPf0RmnkOfGVwWhNFys",
    "expiresTime": "2023-11-01T09:48:04.896831Z"
  }
  ```
  * Authorization scheme: ```Bearer token```
  
