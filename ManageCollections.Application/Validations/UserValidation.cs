using FluentValidation;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(28)
                .MinimumLength(5)
                .WithMessage("Username is invalid");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(28)
                .MinimumLength(5)
                .WithMessage("Name is invalid");

            RuleFor(x => x.Password)
                 .NotEmpty()
                 .NotNull()
                 //.Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
                 //.WithMessage("Password is not valid")
                 //.MinimumLength(6)
                 .WithMessage("Password is not valid");

            RuleFor(x => x.Email)
                 .EmailAddress()
                 .WithMessage("Email is not valid");
        }
    }
}
