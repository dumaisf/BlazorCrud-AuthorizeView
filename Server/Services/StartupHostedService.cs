using BlazorCrud.Server;
using BlazorCrud.Server.Data;
using BlazorCrud.Server.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Services
{
    public class StartupHostedService : IHostedService
    {
        // We need to inject the IServiceProvider so we can create the scoped services
        private readonly IServiceProvider _serviceProvider;

        public StartupHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Get the services
                var logger = services.GetRequiredService<ILogger<Program>>();
                var context = services.GetRequiredService<ApplicationDbContext>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var cache = services.GetRequiredService<IMemoryCache>();

                await DataSecurityInitializer.SeedRolesAsync(roleManager, logger, configuration);
                await DataSecurityInitializer.SeedUsersAsync(roleManager, userManager, logger, configuration);
            }
        }

        // Completed
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}