
Expense Tracker App
--------
Simple expense tracker app.

Features
--------
-  [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture_(software))
- CQRS and [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](http://automapper.org)
- [API Versioning](https://github.com/dotnet/aspnet-api-versioning)
- [HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
- MongoDB
- .NET5
- Angular
- Testing
   - Unit Test
	   -  [xUnit.net](https://github.com/xunit/xunit)
	   -  [Moq](https://github.com/moq/moq)
	- Integration Test
	- UI Test
		- [Selenium](https://www.selenium.dev/) 

## Docker Build

There is a 'Makefile' for OS X and Linux:

- `make build` executes `docker-compose build`
- `make run` executes `docker-compose up -d`

## App URLs

- Swagger: `http://localhost:5001/swagger/index.html`
- Angular: `http://localhost:4200`
- Health Checks: `http://localhost:5001/health`
- Health Checks UI: `http://localhost:5001/health-ui#/healthchecks`

License
-------
The project is licensed under the MIT license.
