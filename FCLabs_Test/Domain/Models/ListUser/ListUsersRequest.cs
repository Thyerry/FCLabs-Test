using Entities.Enums;
using System.ComponentModel;

namespace Domain.Models.ListUser;

public class ListUsersRequest
{
    [DefaultValue(1)]
    public int Page { get; set; }
    public string? Name { get; set; }
    public string? CPF { get; set; }
    public string? Login { get; set; }
    public bool? ReturnActive { get; set; }
    public bool? ReturnInactive { get; set; }
    public DateTime? BirthDateRangeStart { get; set; }
    public DateTime? BirthDateRangeEnd { get; set; }
    public DateTime? InclusionDateRangeStart { get; set; }
    public DateTime? InclusionDateRangeEnd { get; set; }
    public DateTime? LastChangeDateRangeStart { get; set; }
    public DateTime? LastChangeDateRangeEnd { get; set; }
    public AgeRangeEnum? AgeRange { get; set; }
}