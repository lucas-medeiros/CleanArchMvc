using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "usuario@localhost";
            user.Email = "usuario@localhost";
            user.NormalizedUserName = "USUARIO@LOCALHOST";
            user.NormalizedEmail = "USUARIO@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, "User@123456").Result;
            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "User").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "admin@localhost";
            user.Email = "admin@localhost";
            user.NormalizedUserName = "ADMIN@LOCALHOST";
            user.NormalizedEmail = "ADMIN@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, "Admin@123456").Result;
            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "User";
            role.NormalizedName = "USER";
            _ = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            _ = _roleManager.CreateAsync(role).Result;
        }
    }
}
