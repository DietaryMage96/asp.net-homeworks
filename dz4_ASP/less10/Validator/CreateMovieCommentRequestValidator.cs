using FluentValidation;
using less10.Contracts;

namespace less10.Validator
{
    public class CreateMovieCommentRequestValidator : AbstractValidator<CreateMovieCommentRequest>
    {
        public CreateMovieCommentRequestValidator()
        {
            RuleFor(x => x.MovieId)
                .GreaterThan(0).WithMessage("MovieId is required");

            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage("AuthorName is required")
                .MaximumLength(100).WithMessage("AuthorName cannot exceed 100 characters");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Comment text is required")
                .MaximumLength(1000).WithMessage("Comment text cannot exceed 1000 characters");
        }
    }
}
