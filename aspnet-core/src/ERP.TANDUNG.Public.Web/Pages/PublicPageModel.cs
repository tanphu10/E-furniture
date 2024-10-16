using ERP.TANDUNG.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ERP.TANDUNG.Public.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PublicPageModel : AbpPageModel
{
    protected PublicPageModel()
    {
        LocalizationResourceType = typeof(TANDUNGResource);
    }
}
