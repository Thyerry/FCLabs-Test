﻿namespace Domain.Models;

public class UserModel
{
    public int Id { get; set; }
    public int Status { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public string MotherName { get; set; }
}