# Customer Management Solution

## Overview

This solution includes two main applications:
- **CustomerApi**: An ASP.NET Core Web API for managing customer data.
- **CustomerMvc**: An ASP.NET Core MVC application for interacting with the API via a web interface.

## Setup Instructions

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- SQL Server or a configured database

### Running the API

1. Navigate to the `CustomerApi` directory:
    ```sh
    cd CustomerApi
    ```

2. Update the database schema:
    ```sh
    dotnet ef database update
    ```
   Ensure you have the `Microsoft.EntityFrameworkCore.Tools` package installed and configured.

3. Run the API:
    ```sh
    dotnet run
    ```

4. The API will be available at  the URL specified in your `launchSettings.json`.

### Running the MVC Application

1. Navigate to the `CustomerMvc` directory:
    ```sh
    cd CustomerMvc
    ```

2. Run the application:
    ```sh
    dotnet run
    ```

3. The MVC application will be available at the URL specified in your `launchSettings.json`.

### Configuration

- Ensure that the `appsettings.json` files in both `CustomerApi` and `CustomerMvc` are correctly configured with appropriate connection strings and API base URLs.
- For the `CustomerApi`, configure the database connection in `appsettings.json` under the `ConnectionStrings` section.
- For the `CustomerMvc`, configure the API base URL in `appsettings.json` under the `ApiSettings` section.

### Testing

1. Navigate to the `CustomerApi.Tests` directory:
    ```sh
    cd CustomerApi.Tests
    ```

2. Run the tests:
    ```sh
    dotnet test
    ```

## Project Structure

The solution is organized into the following folders:

- **CustomerApi**: Contains the ASP.NET Core Web API project.
- **CustomerMvc**: Contains the ASP.NET Core MVC application project.
- **Solution Items**: Contains files used by the solution as a whole:
  - `README.md`: Documentation and setup instructions.
  - `.gitignore`: Git ignore rules for the solution.
- **Tests**: Contains the test projects:
  - **CustomerApi.Tests**: Unit tests for the `CustomerApi` project.

