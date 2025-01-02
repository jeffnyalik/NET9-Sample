using AuthServer.Data;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Webbs.Extensions
{
    public static class IdentityExtensions
    {
        public static void configureIdentity(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }
    }
}
