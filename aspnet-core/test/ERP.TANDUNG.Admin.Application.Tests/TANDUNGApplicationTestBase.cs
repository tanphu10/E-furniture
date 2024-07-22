using Volo.Abp.Modularity;

namespace ERP.TANDUNG.Admin;

public abstract class TANDUNGApplicationTestBase<TStartupModule> : TANDUNGTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
