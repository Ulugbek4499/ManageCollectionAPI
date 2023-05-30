using FluentValidation;
using ManageCollections.Domain.Entities.IdentityEntities;

namespace ManageCollections.Application.Validations
{
    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(15)
                .MinimumLength(4)
                .WithMessage("Name of the role is invalid");
        }
    }
}
