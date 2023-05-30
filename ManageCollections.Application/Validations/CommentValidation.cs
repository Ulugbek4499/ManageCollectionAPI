using FluentValidation;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Validations
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("Content is invalid");

            RuleFor(x => x.ItemId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Item Id could not be empty");

            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Id could not be empty");
        }
    }
}
