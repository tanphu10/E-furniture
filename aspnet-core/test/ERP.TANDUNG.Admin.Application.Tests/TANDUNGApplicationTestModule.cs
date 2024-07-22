using Volo.Abp.Modularity;

namespace ERP.TANDUNG.Admin;

[DependsOn(
    typeof(TANDUNGAdminApplicationModule),
    typeof(TANDUNGDomainTestModule)
)]
public class TANDUNGApplicationTestModule : AbpModule
{

}
