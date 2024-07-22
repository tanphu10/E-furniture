using System;
using System.Collections.Generic;
using System.Text;
using ERP.TANDUNG.Localization;
using Volo.Abp.Application.Services;

namespace ERP.TANDUNG;

/* Inherit your application services from this class.
 */
public abstract class TANDUNGAppService : ApplicationService
{
    protected TANDUNGAppService()
    {
        LocalizationResource = typeof(TANDUNGResource);
    }
}
