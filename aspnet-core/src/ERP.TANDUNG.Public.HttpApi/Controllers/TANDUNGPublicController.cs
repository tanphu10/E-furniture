using ERP.TANDUNG.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ERP.TANDUNG.Public.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TANDUNGPublicController : AbpControllerBase
{
    protected TANDUNGPublicController()
    {
        LocalizationResource = typeof(TANDUNGResource);
    }
}
