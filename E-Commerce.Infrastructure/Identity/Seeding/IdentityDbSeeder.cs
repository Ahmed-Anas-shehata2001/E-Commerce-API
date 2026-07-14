using E_Commerce.Application.Common.Constants;
using E_Commerce.Infrastructure.Identity.Autherization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace E_Commerce.Infrastructure.Identity.Seeding;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        await SeedRolesAsync(roleManager);

        await SeedPermissionsAsync(roleManager);

        await SeedAdminUserAsync(userManager);
    }

    private static async Task SeedRolesAsync(
        RoleManager<ApplicationRole> roleManager)
    {
        var roles = new[]
        {
            new ApplicationRole
            {
                Name = Roles.Admin,
                Description = "System administrator with full access."
            },
            new ApplicationRole
            {
                Name = Roles.Customer,
                Description = "Customer who can browse products and place orders."
            },
            new ApplicationRole
            {
                Name = Roles.Seller,
                Description = "Seller who can manage products and orders."
            }
        };

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role.Name!))
                continue;

            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }
    }

    private static async Task SeedPermissionsAsync(
        RoleManager<ApplicationRole> roleManager)
    {
        var rolePermissions = new Dictionary<string, string[]>
        {
            [Roles.Admin] = Permissions.GetAll().ToArray(),

            [Roles.Seller] = new[]
            {
                Permissions.Products.View,
                Permissions.Products.Create,
                Permissions.Products.Update
            },

            [Roles.Customer] = new[]
            {
                Permissions.Products.View
            }
        };

        foreach (var (roleName, permissions) in rolePermissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role is null)
                continue;

            var existingClaims = await roleManager.GetClaimsAsync(role);

            foreach (var permission in permissions)
            {
                if (existingClaims.Any(c =>
                    c.Type == CustomClaimTypes.Permission &&
                    c.Value == permission))
                {
                    continue;
                }

                var result = await roleManager.AddClaimAsync(
                    role,
                    new Claim(CustomClaimTypes.Permission, permission));

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception(errors);
                }
            }
        }
    }

    private static async Task SeedAdminUserAsync(
        UserManager<ApplicationUser> userManager)
    {
        const string email = "AboAnas@gmail.com";

        var admin = await userManager.FindByEmailAsync(email);

        if (admin != null)
            return;

        admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = "Ahmed",
            LastName = "Abo Anas",
            PhoneNumber = "01141463084"
        };

        var createResult = await userManager.CreateAsync(admin, "Admin@123456");

        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception(errors);
        }

        var roleResult = await userManager.AddToRoleAsync(admin, Roles.Admin);

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            throw new Exception(errors);
        }
    }
}