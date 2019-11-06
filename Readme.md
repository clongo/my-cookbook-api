# my-cookbook-api
API for the My Cookbook Web site. Works in conjunction with my-cookbook as the frontend client application.

The Vue Client should run at http://localhost:8080/ from the local machine. If not, update `policy.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();` in the ConfigureServices method of Startup.cs.

## Frameworks
•	.NET Core 3.0 API
•	Entity Framework Core (SqlServer) 3.0
•	Google Authentication - For Server Side authorization

## Project setup
```
PM> Update-Package -Reinstall
```
