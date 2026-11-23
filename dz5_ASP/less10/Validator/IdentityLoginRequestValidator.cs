using FluentValidation;
using less10.Contracts;

namespace less10.Validator
{
    public class IdentityLoginRequestValidator : AbstractValidator<IdentityLoginRequest>
    {
        public IdentityLoginRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login is required.")
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Pass)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6)
                .MaximumLength(20)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z]).+$")
                .WithMessage("Password must contain uppercase and lowercase letters.");
        }
    }
}
