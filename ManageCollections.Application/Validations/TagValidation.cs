using FluentValidation;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Validations
{
    public class TagValidation : AbstractValidator<Tag>
    {
        public TagValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(15)
                .MinimumLength(3)
                .WithMessage("Tag name is invalid");
        }
    }
}
