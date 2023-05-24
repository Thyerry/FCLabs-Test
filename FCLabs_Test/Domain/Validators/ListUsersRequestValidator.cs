using Domain.Models.UserModels.ListUser;
using FluentValidation;

namespace Domain.Validators;

public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    public ListUsersRequestValidator()
    {
        RuleFor(r => r.Page)
            .GreaterThan(0)
            .WithMessage("Page numeber must be greater than 1");

        RuleFor(r => r.CPF)
            .Must(BeOnlyNumeric).WithMessage("Only Numeric Characters permited")
            .MinimumLength(11).WithMessage("Invalid CPF Number");

        RuleFor(r => r.BirthDateRangeStart)
            .LessThan(r => r.BirthDateRangeEnd)
            .WithMessage("Birth date range start can't be greater than the range end");

        RuleFor(r => r.InclusionDateRangeStart)
            .LessThan(r => r.InclusionDateRangeEnd)
            .WithMessage("Inclusion date range start can't be greater than the range end");

        RuleFor(r => r.LastChangeDateRangeStart)
            .LessThan(r => r.LastChangeDateRangeEnd)
            .WithMessage("Last change date range start can't be greater than the range end");
    }

    private bool BeOnlyNumeric(string cpf)
    {
        return cpf != null ? long.TryParse(cpf, out _) : true;
    }
}