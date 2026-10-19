using Microsoft.Extensions.Configuration;
using System;

namespace ITL.API;

internal static class DatabaseProviderConfiguration
{
    public static bool UsesInMemoryDatabase(IConfiguration configuration)
    {
        var provider = configuration["Database:Provider"];

        return provider?.Trim().ToLowerInvariant() switch
        {
            null or "sqlserver" => false,
            "inmemory" => true,
            _ => throw new InvalidOperationException(
                $"Unsupported database provider '{provider}'. Use 'SqlServer' or 'InMemory'.")
        };
    }
}
