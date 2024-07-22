using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ERP.TANDUNG.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TANDUNGDbContextFactory : IDesignTimeDbContextFactory<TANDUNGDbContext>
{
    public TANDUNGDbContext CreateDbContext(string[] args)
    {
        TANDUNGEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TANDUNGDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TANDUNGDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ERP.TANDUNG.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
