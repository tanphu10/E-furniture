﻿using ERP.TANDUNG.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ERP.TANDUNG.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TANDUNGController : AbpControllerBase
{
    protected TANDUNGController()
    {
        LocalizationResource = typeof(TANDUNGResource);
    }
}
