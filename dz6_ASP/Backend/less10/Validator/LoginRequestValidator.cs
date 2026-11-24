using FluentValidation;
using less10.Contracts;

namespace less10.Validator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Pass)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .Matches(@"[A-Z]").WithMessage("Requires uppercase letter")
                .Matches(@"[a-z]").WithMessage("Requires lowercase letter");
        }
    }
}