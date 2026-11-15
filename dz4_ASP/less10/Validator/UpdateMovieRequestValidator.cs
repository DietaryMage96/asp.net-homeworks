
using FluentValidation;
using less10.Contracts;

namespace less10.Validator
{
    public class UpdateMovieRequestValidator : AbstractValidator<UpdateMovieRequest>
    {
        public UpdateMovieRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1888, 2030).WithMessage("Release year must be between 1888 and 2030");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0.0, 10.0).WithMessage("Rating must be between 0 and 10");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required");

            RuleFor(x => x.PosterUrl)
                .NotEmpty().WithMessage("Poster URL is required");

            RuleFor(x => x.FilmUrl)
                .NotEmpty().WithMessage("Film URL is required");
        }
    }
}
