namespace Domain.Models.UserModels.ListUser;

public class ListUsersQueryParameters
{
    public string? Name { get; set; }
    public string? CPF { get; set; }
    public string? Login { get; set; }
    public bool ReturnActive { get; set; }
    public bool ReturnInactive { get; set; }
    public DateTimeRange? BirthDateRange { get; set; }
    public DateTimeRange? InclusionDateRange { get; set; }
    public DateTimeRange? LastChangeDateRange { get; set; }
    public DateTimeRange? AgeDateRange { get; set; }
}