using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, CleanArchitecture.Infrastructure.Contexts.ApplicationDbContext context)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Mukesh",
                LastName = "Murugan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    var createdUser = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (!context.Users.Any(u => u.Id == createdUser.Id))
                    {
                        context.Users.Add(new CleanArchitecture.Core.Entities.User
                        {
                            Id = createdUser.Id,
                            Username = createdUser.UserName,
                            Email = createdUser.Email,
                            PasswordHash = createdUser.PasswordHash,
                            CreatedAt = System.DateTime.UtcNow,
                            RoleId = CleanArchitecture.Core.Enums.UserRole.Admin
                        });
                        await context.SaveChangesAsync();
                    }

                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
