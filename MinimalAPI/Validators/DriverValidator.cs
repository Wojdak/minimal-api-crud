using FluentValidation;
using MinimalAPI.Models;

namespace MinimalAPI.Validators
{
    public class DriverValidator : AbstractValidator<Driver>
    {
        public DriverValidator()
        {
            RuleFor(d => d.RacingNumber)
                .NotEmpty()
                .LessThanOrEqualTo(99)
                .GreaterThan(0)
                .WithMessage("The number must be between 1 and 99");
            RuleFor(d => d.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(40)
                .WithMessage("The length of the name must be between 5-40 characters");
            RuleFor(d => d.Nationality)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(40)
                .WithMessage("The length of the nationality must be between 5-40 characters");
            RuleFor(d => d.Team)
                .MinimumLength(5)
                .MaximumLength(40)
                .WithMessage("The length of the team name must be between 5-40 characters");
        }
    }
}
