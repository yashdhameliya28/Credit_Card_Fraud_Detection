using FluentValidation;

namespace Credit_Card_Fraud_Detection.Validators
{
    public class DeviceHistoryValidator : AbstractValidator<DeviceHistoryDTO>
    {
        public DeviceHistoryValidator()
        {
            RuleFor(X => X.deviceName)
                .NotEmpty().WithMessage("Device name required...")
                .MaximumLength(20).WithMessage("Device name can not be excced 20 characters...");

            RuleFor(x => x.deviceType)
               .Must(g => g == "Mobile" || g == "Desktop" || g == "Laptop" || g == "Tablet")
               .WithMessage("Device type must be Mobile, Desktop, Tablet or Laptop...");


        }
    }
}
