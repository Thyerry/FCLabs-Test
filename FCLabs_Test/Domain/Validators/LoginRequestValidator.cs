using Domain.Models.LoginModels;
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
                .Must(BeOnlyNumeric).WithMessage("Invalid CPF: only digits are permited");
            RuleFor(r => r.password)
                .NotEmpty().WithMessage("Enter your password");
        }

        private bool BeOnlyNumeric(string cpf)
        {
            return cpf != null ? long.TryParse(cpf, out _) : true;
        }
    }
}