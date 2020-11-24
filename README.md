# KCTest
Sample Web API implementation with .NET Core and DDD using Clean Architecture.

## Solution Design
The solution design focuses on a basic Domain Driven Design techniques and implementation, while keeping the things as simple as possible but can be extended as needed. Multiple assemblies are used for separation of concerns to keep logic isolated from the other components. **.NET 5 C#** is the default framework and language for this application.

### Assembly Layers
-   **KCTest.Domain**  - This assembly contains common, entities and interfaces.
-   **KCTest.Application**  - This assembly contains all services implementations.
-   **KCTest.Infrastructure**  - This assembly contains the infrastructure of data persistence.
-   **KCTest.API**  - This assembly is the web api host.

## Validation
Data validation using [FluentValidation](https://github.com/JeremySkinner/FluentValidation)

## How to run application: 
[(Backend)](https://github.com/Jadhielv/KCTest/tree/master/Backend)

1. Create empty database, name: **`KCTest`**.
2. Execute [migrations](https://github.com/Jadhielv/KCTest/tree/master/Backend/src/KCTest.Infrastructure/Migrations).
2. Set connection string (in [appsettings.json](https://github.com/Jadhielv/KCTest/blob/master/Backend/src/KCTest.API/appsettings.json) or by user secrets mechanism).
3. Run .. .

[(kctest-frontend)](https://github.com/Jadhielv/KCTest/tree/master/kctest-frontend)

Follow the instructions in [README](https://github.com/Jadhielv/KCTest/blob/master/kctest-frontend/README.md) file.

## License

This project is open source and available under the: [MIT License](LICENSE)