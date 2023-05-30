using FluentValidation;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Validations
{
    public class ItemValidation : AbstractValidator<Item>
    {
        public ItemValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(28)
                .MinimumLength(5)
                .WithMessage("Item name is invalid");

            RuleFor(x => x.Image)
                .NotEmpty()
                .NotNull()
                .WithMessage("Item image is invalid");
        }
    }
}
