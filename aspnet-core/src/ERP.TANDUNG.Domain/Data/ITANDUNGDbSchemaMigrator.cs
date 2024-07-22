using System.Threading.Tasks;

namespace ERP.TANDUNG.Data;

public interface ITANDUNGDbSchemaMigrator
{
    Task MigrateAsync();
}
