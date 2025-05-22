using ITL.Domain;
using ITL.Domain.Repositories;
using ITL.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ITL.Infrastructure.Repositories;

public static class UnitOfWorkExtension
{
    public static IServiceCollection SetupUnitOfWork([NotNullAttribute] this IServiceCollection serviceCollection)
    {
        // Register repositories with DI, sharing the same DbContext instance
        serviceCollection.AddScoped<IPermissionRepository, PermissionRepository>();
        serviceCollection.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        return serviceCollection;
    }
}
