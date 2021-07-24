using System;
using API.Data;
using API.Data.Repositories;
using API.Helpers;
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
        {
            
            string connectionString = null;
            /*
            connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            Console.WriteLine("connectionString: " + connectionString);
            
            connectionString = "host=" + 
                Environment.GetEnvironmentVariable("POSTGRES_HOST") +
                ";port=5432;database=blogdb;username=admin;password=admin"
                ;
            Console.WriteLine("connectionString: " + connectionString);
            
            connectionString = connectionString!=null ? connectionString : config.GetConnectionString("PostgresDefaultConnection"); 
            Console.WriteLine("connectionString: " + connectionString);
            */
            connectionString = connectionString!=null ? connectionString : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"); 
            Console.WriteLine("connectionString: " + connectionString);
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlite("Connection String");
                //options.UseSqlite(config.GetConnectionString("SqliteDefaultConnection"));
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}