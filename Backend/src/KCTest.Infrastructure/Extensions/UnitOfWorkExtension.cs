using KCTest.Infrastructure.Database;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KCTest.Domain.Repositories;
using KCTest.Domain;

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