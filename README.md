# Customer Management Solution

## Overview

This solution includes two applications:
- **CustomerApi**: ASP.NET Core Web API for managing customers.
- **CustomerMvc**: ASP.NET Core MVC application for frontend interaction with the API.

## Setup Instructions

### Prerequisites
- .NET Core SDK
- SQL Server or other configured database

### Running the API

1. Navigate to the `CustomerApi` directory:
    ```sh
    cd CustomerApi
    ```

2. Update the database:
    ```sh
    dotnet ef database update
    ```

3. Run the API:
    ```sh
    dotnet run
    ```

### Running the MVC Application

1. Navigate to the `CustomerMvc` directory:
    ```sh
    cd CustomerMvc
    ```

2. Run the application:
    ```sh
    dotnet run
    ```

## Testing

1. Navigate to the `CustomerApi.Tests` directory:
    ```sh
    cd CustomerApi.Tests
    ```

2. Run the tests:
    ```sh
    dotnet test
    ```

## Project Structure