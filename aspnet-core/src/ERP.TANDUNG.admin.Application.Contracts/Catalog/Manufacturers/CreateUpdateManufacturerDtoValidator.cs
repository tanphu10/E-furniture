using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TANDUNG.Admin.Catalog.Manufacturers
{
    public class CreateUpdateManufacturerDtoValidator : AbstractValidator<CreateUpdateManufacturerDto>
    {
        public CreateUpdateManufacturerDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CoverPicture).MaximumLength(250);
        }
    }
}
