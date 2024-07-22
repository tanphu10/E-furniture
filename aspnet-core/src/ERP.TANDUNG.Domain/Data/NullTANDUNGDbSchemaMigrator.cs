using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ERP.TANDUNG.Data;

/* This is used if database provider does't define
 * ITANDUNGDbSchemaMigrator implementation.
 */
public class NullTANDUNGDbSchemaMigrator : ITANDUNGDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
