# ITL
Sample Web API implementation with .NET Core and DDD using Clean Architecture.

## Solution Design
The solution design focuses on a basic Domain Driven Design techniques and implementation, while keeping the things as simple as possible but can be extended as needed. Multiple assemblies are used for separation of concerns to keep logic isolated from the other components. **.NET 10 C#** is the default framework and language for this application.

### Assembly Layers
-   **ITL.Domain**  - This assembly contains common, entities and interfaces.
-   **ITL.Application**  - This assembly contains all services implementations.
-   **ITL.Infrastructure**  - This assembly contains the infrastructure of data persistence.
-   **ITL.API**  - This assembly is the web api host.
-   **ITL.Tests**  - This assembly contains unit test classes based on the [NUnit](https://github.com/nunit/nunit) testing framework.

## How to contribute

> :thought_balloon: If you are new in Open Source world feel free to check our [How to contribute guidelines](https://github.com/Jadhielv/ITL/blob/master/CONTRIBUTING.md)

## Validation
Data validation using [FluentValidation](https://github.com/JeremySkinner/FluentValidation)

## How to run application: 
[(Backend)](https://github.com/Jadhielv/ITL/tree/master/Backend)

1. Create empty database, name: **`KCTest`**.
2. Execute [migrations](https://github.com/Jadhielv/ITL/tree/master/Backend/src/ITL.Infrastructure/Migrations).
3. Set connection string (in [appsettings.json](https://github.com/Jadhielv/ITL/blob/master/Backend/src/ITL.API/appsettings.json) or by user secrets mechanism).
4. Run `dotnet run --project Backend/src/ITL.API` to start the API.

[(kctest-frontend)](https://github.com/Jadhielv/ITL/tree/master/kctest-frontend)

Follow the instructions in [README](https://github.com/Jadhielv/ITL/blob/master/kctest-frontend/README.md) file.

## ¡Give a Star!

If you like this project, learn something or you are using it in your applications, please give it a star. Thanks! .. .

## License

This project is open source and available under the -> [MIT License](LICENSE)
