namespace Domain.Models;

public class ListUsersResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalUsers { get; set; }
    public List<UserModel> Users { get; set; }
}
