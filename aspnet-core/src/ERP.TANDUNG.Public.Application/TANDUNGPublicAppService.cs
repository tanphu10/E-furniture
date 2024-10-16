using System;
using System.Collections.Generic;
using System.Text;
using ERP.TANDUNG.Localization;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG.Public;

/* Inherit your application services from this class.
 */
public abstract class TANDUNGPublicAppService : ApplicationService
{
    protected TANDUNGPublicAppService()
    {
        LocalizationResource = typeof(TANDUNGResource);
    }
}
