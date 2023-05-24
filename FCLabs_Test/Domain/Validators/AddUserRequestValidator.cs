using Domain.Models.UserModels.ListUser;
using FluentValidation;
using System.Numerics;

namespace Domain.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name can't be null or empty");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email can't be null or empty")
                .EmailAddress().WithMessage("Email must be in the correct format");
            RuleFor(r => r.Phone)
                .Must(BeNumeric).WithMessage("Invalid Phone: only digits are permited");
            RuleFor(r => r.CPF)
                .NotEmpty().WithMessage("CPF can't be null or empty")
                .MinimumLength(11).WithMessage("Invalid CPF: Too few digits")
                .Must(BeNumeric).WithMessage("Invalid CPF: only digits are permited");
            RuleFor(r => r.BirthDate)
                .NotEmpty().WithMessage("Birth Date can't be null or empty");
        }

        private bool BeNumeric(string numericString)
        {
            return numericString != null ? BigInteger.TryParse(numericString, out _) : true;
        }
    }
}