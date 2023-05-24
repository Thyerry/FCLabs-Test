using Domain.Models.ListUser;
using FluentValidation;

namespace Domain.Validators;

public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    public ListUsersRequestValidator()
    {
        RuleFor(r => r.Page)
            .LessThan(1)
            .WithMessage("Result page is lesser than 1");

        RuleFor(r => r.CPF)
            .MinimumLength(11)
            .WithMessage("Invalid CPF Number");

        RuleFor(r => r.BirthDateRangeStart)
            .GreaterThan(r => r.BirthDateRangeEnd)
            .WithMessage("Birth date range start can't be greater than the range end");

        RuleFor(r => r.InclusionDateRangeStart)
            .GreaterThan(r => r.InclusionDateRangeEnd)
            .WithMessage("Inclusion date range start can't be greater than the range end");

        RuleFor(r => r.LastChangeDateRangeStart)
            .GreaterThan(r => r.LastChangeDateRangeEnd)
            .WithMessage("Last change date range start can't be greater than the range end");

    }
}