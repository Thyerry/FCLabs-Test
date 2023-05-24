namespace Domain.Models.UserModels;

public class UserResponse
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CPF { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? MotherName { get; set; }
    public int? Status { get; set; }
    public DateTime? InclusionDate { get; set; }
    public DateTime? LastChangeDate { get; set; }
}
