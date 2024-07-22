using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ERP.TANDUNG;

[Dependency(ReplaceServices = true)]
public class TANDUNGBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TANDUNG";
}
