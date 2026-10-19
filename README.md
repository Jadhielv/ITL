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

1. Install the **.NET 10 SDK**.
2. Run `dotnet restore Backend/Backend.sln` and `dotnet build Backend/Backend.sln`.
3. Start the API with `dotnet run --project Backend/src/ITL.API --launch-profile ITL.API`.
   The Development environment uses an in-memory database by default, so SQL Server is not required. Its seed data (permission types) is recreated on each run and is not persisted after the API stops.
4. Open `https://localhost:5001/swagger` to explore the API.

To use SQL Server instead, set `Database:Provider` to `SqlServer` in `Backend/src/ITL.API/appsettings.Development.json` (or use `Database__Provider=SqlServer` as an environment variable), then set the `KCTestContext` connection string in `appsettings.json`, user secrets, or the `ConnectionStrings__KCTestContext` environment variable. Create the `KCTest` database first; the API applies its migrations on startup.

Outside Development, `Database:Provider` defaults to `SqlServer`. Select `InMemory` only for local development or testing; it is temporary and is not a substitute for persistent storage.

The [kctest-frontend](https://github.com/Jadhielv/ITL/tree/master/kctest-frontend) is built with Vue 3, Pinia, Vue Router 4, BootstrapVue Next, and Bootstrap 5.

Follow the instructions in [README](https://github.com/Jadhielv/ITL/blob/master/kctest-frontend/README.md) file.

## ¡Give a Star!

If you like this project, learn something or you are using it in your applications, please give it a star. Thanks! .. .

## License

This project is open source and available under the -> [MIT License](LICENSE)
