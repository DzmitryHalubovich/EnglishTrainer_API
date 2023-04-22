using EnglishTrainer.Contracts;
using EnglishTrainer.Entities;
using EnglishTrainer.LoggerService;
using Microsoft.EntityFrameworkCore;

namespace EnglishTrainer.API.Extensions.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => 
            services.AddCors(
                options => 
                { 
                    options.AddPolicy("CorsPolicy", builder => builder
                        .AllowAnyOrigin() //WithOrigins("https://example.com")
                        .AllowAnyMethod() //WithMethods("POST", "GET")
                        .AllowAnyHeader()); //WithHeaders("accept", "content-type") method to allow only specific headers
                });

        public static void ConfigureIISIntegration(this IServiceCollection services) => 
            services.Configure<IISOptions>(options => {
        });

        public static void ConfigureLoggerService(this IServiceCollection services) => 
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => 
            services.AddDbContext<EFContext>(opts => 
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
