namespace Domain.Models.LoginModels
{
    public class RecoverPasswordRequest
    {
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}