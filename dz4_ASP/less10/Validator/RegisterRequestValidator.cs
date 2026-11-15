using FluentValidation;
using less10.DTOs;

namespace less10.Validator
{
    public class IdentityRegisterRequestValidator : AbstractValidator<IdentityRegisterRequest>
    {
        public IdentityRegisterRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Pass)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20)
                .Matches(@"[A-Z]").WithMessage("Requires uppercase letter")
                .Matches(@"[a-z]").WithMessage("Requires lowercase letter");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Pass)
                .WithMessage("Passwords do not match.");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 120)
                .WithMessage("Age must be between 0 and 120.");
        }
    }
}
