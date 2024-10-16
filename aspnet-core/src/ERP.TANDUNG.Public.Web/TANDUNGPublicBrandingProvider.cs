using Microsoft.Extensions.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using ERP.TANDUNG.Localization;

namespace ERP.TANDUNG.Public.Web;

[Dependency(ReplaceServices = true)]
public class TANDUNGPublicBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TANDUNGResource> _localizer;

    public TANDUNGPublicBrandingProvider(IStringLocalizer<TANDUNGResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
