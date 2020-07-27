# CigniumChallenge

This repository contains a Visual Studio solution with 2 projects: a .NET Core Web Api Premium Calculator and a .NET Core pure HTML/Javascript/CSS Web site that consumes the service.

## Architecture

* The web service was build with .NET Core WebApi, following the Repository pattern, in order to allow multiple decoupled implementations. Two concrete implmentations were made: a in memory implementation and a sql server implementation.
For database access, it uses Entity Framework, handling database queries to the DbContext object

* The website was build with .NET Core, using only HTML/CSS/Vanilla Javascript for the frontend

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* Visual Studio
* .NET Core 3.1
* SQL Server (optional for testing connection with a database)

### Installing

* Clone the repository locally
* [Optional] Create a SQL Server database called PremiumCalculator and run the scripts from PremiumCalculatorApi/Data/Database/DatabaseScripts.sql. You can choose the Repository implementation in the PremiumCalculatorApi/Startup.cs file
```
//services.AddTransient<IPremiumRuleRepository, DbPremiumRuleRepository>();
services.AddTransient<IPremiumRuleRepository, MockPremiumRuleRepository>();
```
* Build the solution
* Run each of the projects independently. The web service runs on the port 44364 and the web site run on the port 44328. This configuration can be changed in the launchSettings.json file of each project, but the web service has a CORS configuration for allowed domains, so it also needs to be changed in the PremiumCalculatorApi/Startup.cs file 
```
services.AddCors(o => o.AddPolicy("AllowWebOrigins", builder =>
{
    //Allowed domains
    builder.WithOrigins("https://localhost:44328")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));
```

