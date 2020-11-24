using BlazorCrud.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Data
{
    public static class DataSecurityInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger, IConfiguration configuration)
        {
            logger.LogInformation($"Vérifier la présence du role {configuration["Role"]}");

            if (await roleManager.RoleExistsAsync(configuration["Role"]) == false)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(configuration["Role"]));

                if (result.Succeeded)
                {
                    logger.LogInformation($"Role {configuration["Role"]} ajouté avec succès");
                }
                else
                {
                    var exception = new ApplicationException($"Impossible de créer le role {configuration["Role"]}");
                    throw exception;
                }
            }
        }

        public static async Task SeedUsersAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger logger,
            IConfiguration configuration)
        {
            var adminUserName = configuration["AdminUserName"];

            logger.LogInformation($"Vérifier la présence de l'utilisateur {adminUserName}");
            var user = await userManager.FindByNameAsync(adminUserName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminUserName,
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    logger.LogInformation($"Utilisateur {adminUserName} ajouté avec succès");

                    await CreateAdminPasswordAsync(roleManager, userManager, logger, configuration);
                }
                else
                {
                    var exception = new ApplicationException($"Impossible de créer l'utilisateur {adminUserName}");
                    throw exception;
                }
            }
        }

        public static async Task CreateAdminPasswordAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger logger,
            IConfiguration configuration)
        {
            var adminUserName = configuration["AdminUserName"];
            var adminPassword = configuration["AdminPassword"];

            logger.LogInformation($"Ajouter le mot de passe {adminUserName}");
            var user = await userManager.FindByNameAsync(adminUserName);

            var result = await userManager.AddPasswordAsync(user, adminPassword);

            if (result.Succeeded)
            {
                logger.LogInformation($"Mot de passe {adminUserName} ajouté avec succès");

                await SetroleAdministrateurAsync(roleManager, userManager, logger, configuration);
            }
            else
            {
                var exception = new ApplicationException($"Impossible de créer le mot de passe {adminUserName}");
                throw exception;
            }
        }

        public static async Task SetroleAdministrateurAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger logger,
            IConfiguration configuration)
        {
            var adminUserName = configuration["AdminUserName"];

            logger.LogInformation($"Vérifier si {adminUserName} est {configuration["Role"]}");

            var userAdmin = await userManager.FindByNameAsync(adminUserName);
            var roleExist = await roleManager.RoleExistsAsync(configuration["Role"]);

            if (userAdmin != null && roleExist)
            {
                var userIsAdmin = await userManager.IsInRoleAsync(userAdmin, configuration["Role"]);

                if (!userIsAdmin)
                {
                    logger.LogInformation($"Associer le Role {configuration["Role"]} avec {adminUserName}");
                    await userManager.AddToRoleAsync(userAdmin, configuration["Role"]);
                }
            }
        }
    }
}