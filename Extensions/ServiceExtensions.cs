using Contracts;
using LoggerService;
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
    }
}
