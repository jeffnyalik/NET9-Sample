using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Webbs.Extensions
{   
    public static class ServiceExtensions
    {
        //configure cors 
        public static void configureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin().SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
                });
            });
        }
        
        //configure IIS configs
        public static void configureIIsConfiguration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                //default values for now
            });
        }

        //Configure logger services
        public static void configureLoggerServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        //Configure sql server
        public static void configureSqlServer(this IServiceCollection services, IConfiguration config)
        {
           var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }
    }
}
