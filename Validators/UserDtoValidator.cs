// File: Validators/UserDtoValidator.cs
using FluentValidation;
using Credit_Card_Fraud_Detection.Dtos;

namespace Credit_Card_Fraud_Detection.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(x => x.Gender)
                .Must(g => g == 'M' || g == 'F' || g == 'O')
                .WithMessage("Gender must be 'M', 'F', or 'O'.");

            RuleFor(x => x.Job)
                .MaximumLength(100).When(x => x.Job != null);

            RuleFor(x => x.State)
                .MaximumLength(50).When(x => x.State != null);

            RuleFor(x => x.CityPop)
                .GreaterThanOrEqualTo(0).When(x => x.CityPop.HasValue)
                .WithMessage("City population must be positive.");
        }
    }
}