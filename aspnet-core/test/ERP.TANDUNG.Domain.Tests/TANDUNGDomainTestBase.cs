using Volo.Abp.Modularity;

namespace ERP.TANDUNG;

/* Inherit from this class for your domain layer tests. */
public abstract class TANDUNGDomainTestBase<TStartupModule> : TANDUNGTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
