using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {   var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            Console.WriteLine("connectionString: " + connectionString);
            connectionString = connectionString!=null ? connectionString : config.GetConnectionString("PostgresDefaultConnection"); 
            Console.WriteLine("connectionString: " + connectionString);
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlite("Connection String");
                //options.UseSqlite(config.GetConnectionString("DefaultConnection"));
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}