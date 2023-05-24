using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Helpers;

public static class UserManagerExtension
{
    public static async Task<ApplicationUser> FindByCpfAsync(this UserManager<ApplicationUser> userManager, string cpf)
    {
        return await userManager?.Users?.SingleOrDefaultAsync(u => u.CPF == cpf);
    }
}
