using Volo.Abp.Modularity;

namespace ERP.TANDUNG;

[DependsOn(
    typeof(TANDUNGDomainModule),
    typeof(TANDUNGTestBaseModule)
)]
public class TANDUNGDomainTestModule : AbpModule
{

}
