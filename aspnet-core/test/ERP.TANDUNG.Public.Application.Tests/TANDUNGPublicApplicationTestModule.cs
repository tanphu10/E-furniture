using Volo.Abp.Modularity;

namespace ERP.TANDUNG.Public;

[DependsOn(
    typeof(TANDUNGPublicApplicationModule),
    typeof(TANDUNGDomainTestModule)
)]
public class TANDUNGPublicApplicationTestModule : AbpModule
{

}
