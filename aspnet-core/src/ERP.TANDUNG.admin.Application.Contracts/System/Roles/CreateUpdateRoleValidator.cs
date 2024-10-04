using FluentValidation;

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
