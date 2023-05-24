using Domain.Models.LoginModels;
using Domain.Utils;
using FluentValidation;

namespace Domain.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name can't be null or empty");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email can't be null or empty")
                .EmailAddress().WithMessage("Email must be in the correct format");
            RuleFor(r => r.CPF)
                .NotEmpty().WithMessage("CPF can't be null or empty")
                .MinimumLength(11).WithMessage("Invalid CPF: Too few digits")
                .Must(ValidationMethods.BeOnlyNumeric).WithMessage("Invalid CPF: only digits are permited");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password can't be null or empty")
                .Equal(r => r.ConfirmPassword).WithMessage("Passwords don't match");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Password Confirmation can't be null or empty")
                .Equal(r => r.Password).WithMessage("Passwords don't match");
        }
    }
}