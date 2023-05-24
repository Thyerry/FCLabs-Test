using Domain.Models.LoginModels;
using Domain.Utils;
using FluentValidation;

namespace Domain.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(r => r.cpf)
                .NotEmpty().WithMessage("Enter your CPF")
                .MinimumLength(11).WithMessage("Invalid CPF: Too few digits")
                .Must(ValidationMethods.BeOnlyNumeric).WithMessage("Invalid CPF: only digits are permited");
            RuleFor(r => r.password)
                .NotEmpty().WithMessage("Enter your password");
        }
    }
}