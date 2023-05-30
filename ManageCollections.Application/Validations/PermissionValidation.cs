using FluentValidation;
using ManageCollections.Domain.Entities.IdentityEntities;

namespace ManageCollections.Application.Validations
{
    public class PermissionValidation : AbstractValidator<Permission>
    {
        public PermissionValidation()
        {
            RuleFor(x => x.PermissionName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(28)
                .MinimumLength(5)
                .WithMessage("Permission name is invalid");
        }
    }
}
