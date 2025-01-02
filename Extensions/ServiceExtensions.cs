using AuthServer.Data;
using Contracts;
using Entities;
using Entities.Interfaces;
using Entities.Repositories;
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
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("Webbs")));
        }

        public static void configureIdentitySqlServer(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:IdentityConnection"];
            services.AddDbContext<IdentityContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("Webbs")));
        }

        public static void configureJsonSerializerOptions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });
        }

        public static void configureStockRepository(this IServiceCollection services)
        {
            services.AddScoped<IStockRepository, StockRepository>();
        }

        public static void configureCommentRepository(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
        }
    }
}
