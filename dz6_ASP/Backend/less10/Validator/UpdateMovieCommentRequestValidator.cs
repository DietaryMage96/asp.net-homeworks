using FluentValidation;
using less10.Contracts;

namespace less10.Validator
{
    public class UpdateMovieCommentRequestValidator : AbstractValidator<UpdateMovieCommentRequest>
    {
        public UpdateMovieCommentRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Comment text is required")
                .MaximumLength(1000).WithMessage("Comment text cannot exceed 1000 characters");
        }
    }
}
