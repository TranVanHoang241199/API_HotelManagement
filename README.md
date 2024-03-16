# Hotel Management System with ASP.NET Core 7 API

This repository contains the source code of a hotel management system built using the ASP.NET Core 7 API. The project is designed to support mobile and web applications with the latest version of the .NET Core framework .

This project is a support repository for Hotels that stores customer information, history, room information, services provided, room number, status... "ASP.NET Core 7 API - Cross - Platform Development", can be found at this URL: http://hotel.somee.com/swagger/index.html

## Table of Contents

- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Features](#features)
- [Documentation](#documentation)
- [Contributing](#contributing)
- [License](#license)

## Technologies

The project uses the following technologies:

- ASP.NET Core 7 API
- Entity Framework Core
- SQL Server
- AutoMapper
- JWTBearer
- serilog

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Install the latest version of .NET Core SDK and SQL Server on your machine.
3. Update the connection string in the `appsettings.json` file with your SQL Server connection string.
4. Run the following commands in the root directory of the project to set up the database: `update-database`
5. Run the following command to start the web application: `dotnet run --project API_HotelManagement`


## Project Structure

The solution is composed of the following projects:

- `API_HotelManagement`: Contains the API controllers, version, log.
- `API_HotelManagement.Business`: Contains the service (IHandler, Handler, ModelView/CreateUpdate).
- `API_HotelManagement.common`: Contains the Help business, extensions, type, ApiResponse,..
- `API_HotelManagement.Data`: Contains the Entity, Context.

## Featured

The project includes the following features:

- Authentication and authorization using Auth and Token
- Operate CRUD for rooms, services, Category Rooms/Services, orders, Detail Rooms/Services...


## Documentation

The repository contains documentation for each of the features, including:

- An overview of the feature and its intended use case
- Installation and configuration instructions
- Code walkthroughs and explanations
- Best practices and tips for working with the feature

The documentation is designed to be accessible to developers of all skill levels, from beginners to advanced users.

## Contribute

Contributions to this archive are welcome. If you have a feature or resource that supports Hotel Management development that you would like to contribute, please follow these steps:

1. Branching the repository.
2. Create a new branch for your changes.
3. Make changes and commit them to your branch.
4. Submit a pull request.

Please ensure that your contributions comply with the repository's code of conduct and that they are well documented and follow best practices.

## License

This repository is licensed under the HoangTV(RyanT). See the [LICENSE](LICENSE) file for more information.
