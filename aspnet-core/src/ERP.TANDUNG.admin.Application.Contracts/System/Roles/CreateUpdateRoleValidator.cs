using ERP.TANDUNG.Admin.ProductCategories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TANDUNG.Admin.System.Roles
{
    public class CreateUpdateRoleValidator : AbstractValidator<CreateUpdateRoleDto>
    {
        public CreateUpdateRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}
