using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DefaultArtistUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, CleanArchitecture.Infrastructure.Contexts.ApplicationDbContext context)
        {
            //Seed Default Artist User
            var defaultUser = new ApplicationUser
            {
                UserName = "defaultartist",
                Email = "artist@mozart.ai",
                FirstName = "Mozart",
                LastName = "Artist",
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
                            RoleId = CleanArchitecture.Core.Enums.UserRole.Artist
                        });
                        await context.SaveChangesAsync();
                    }

                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }

            }
        }
    }
}
