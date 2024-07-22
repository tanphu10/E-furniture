using Volo.Abp.Modularity;

namespace ERP.TANDUNG;

[DependsOn(
    typeof(TANDUNGApplicationModule),
    typeof(TANDUNGDomainTestModule)
)]
public class TANDUNGApplicationTestModule : AbpModule
{

}
