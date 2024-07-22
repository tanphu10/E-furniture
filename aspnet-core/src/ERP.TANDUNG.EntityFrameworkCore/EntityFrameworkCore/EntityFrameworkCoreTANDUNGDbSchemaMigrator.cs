using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ERP.TANDUNG.Data;
using Volo.Abp.DependencyInjection;

namespace ERP.TANDUNG.EntityFrameworkCore;

public class EntityFrameworkCoreTANDUNGDbSchemaMigrator
    : ITANDUNGDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTANDUNGDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the TANDUNGDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TANDUNGDbContext>()
            .Database
            .MigrateAsync();
    }
}
