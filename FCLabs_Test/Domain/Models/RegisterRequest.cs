namespace Domain.Models;

public class RegisterRequest
{
    public string UserName { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}