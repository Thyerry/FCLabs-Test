using Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

[Table("User")]
public class User
{
    public int Id { get; set; }
    public StatusEnum Status { get; set; }
    public string CPF { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string MotherName { get; set; }
    public DateTime InclusionDate { get; set; }
    public DateTime LastChangeDate { get; set; }
}