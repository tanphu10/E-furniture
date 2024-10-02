﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ERP.TANDUNG.Admin.Roles
{
    public class RoleInListDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
