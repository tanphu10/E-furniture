using System;
using System.Collections.Generic;
using System.Text;
using ERP.TANDUNG.Localization;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG.Admin;

/* Inherit your application services from this class.
 */
public abstract class TANDUNGAdminAppService : ApplicationService
{
    protected TANDUNGAdminAppService()
    {
        LocalizationResource = typeof(TANDUNGResource);
    }
}
