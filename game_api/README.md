# Rock Paper Scissors Api

## **Important notes about the solution**
- For ease of dev work I made IDs int, but Guid would have been preferable
- Anything to do with users, at the moment it is coupled to the user name, I added identifier to distinguish players with the same name
- I chose to use SQL DB, but there is no great need for it to be relational, and the solution would work just as fine with any NoSQL db

## Technologies
- .NET 6
- ASP.NET Core 6
- Swagger (Documentation)
- Entity Framework Core (SQL Server)
- AutoMapper
- FluentValidation
- XUnit
- NUnit

## Running the solution

If you meet all the requirements, the project will run without any problems and can be used from the first second wihtout any additional set up.
The home page is the generated OpenApi by swagger.

## Running tests

All test run and pass, use your IDE/ test runner to run the tests