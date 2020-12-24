using KCTest.Domain;
using KCTest.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace KCTest.Infrastructure.Repositories
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection SetupUnitOfWork([NotNullAttribute] this IServiceCollection serviceCollection)
        {
            //TODO: Find a way to inject the repositories and share the same context without creating a instance.
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var scopeFactory = f.GetRequiredService<IServiceScopeFactory>();
                var context = f.GetService<KCTestContext>();
                return new UnitOfWork(
                    context,
                    new PermissionRepository(context.Permissions),
                    new PermissionTypeRepository(context.PermissionTypes)
                );
            });
            return serviceCollection;
        }
    }
}