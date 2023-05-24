namespace Domain.Models.UserModels.ListUser
{
    public class AddUserRequest
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MotherName { get; set; }
    }
}