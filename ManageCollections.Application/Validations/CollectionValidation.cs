using FluentValidation;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Validations
{
    public class CollectionValidation : AbstractValidator<Collection>
    {
        public CollectionValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(28)
                .MinimumLength(5)
                .WithMessage("Collection name is invalid");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(5)
                .WithMessage("Collection description is invalid");

            RuleFor(x => x.Image)
                .NotEmpty()
                .NotNull()
                .WithMessage("Collection description is invalid");

            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Id could not be empty");

            RuleFor(x => x.Topic)
                .NotEmpty()
                .NotNull()
                .WithMessage("Collection topic is invalid");
        }
    }
}
