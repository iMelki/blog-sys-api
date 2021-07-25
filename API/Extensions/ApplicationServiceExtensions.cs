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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            /*
            services.AddDbContext<DataContext>(options =>
            {
                string connectionString = null;
                connectionString = connectionString!=null ? connectionString : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"); 
                Console.WriteLine("connectionString: " + connectionString);
                
                //options.UseSqlite("Connection String");
                //options.UseSqlite(config.GetConnectionString("SqliteDefaultConnection"));
                options.UseNpgsql(connectionString);
            });
            */            
            services.AddDbContext<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connectionString;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use Sqlite onnection string from file.
                    //connectionString = config.GetConnectionString("SqliteDefaultConnection");
                    //options.UseSqlite(config.GetConnectionString(connectionString));
                    //Use PG:
                    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    connectionString = Environment.GetEnvironmentVariable("PG_CONNECTION_STRING");

                    // Parse connection URL to connection string for Npgsql
                    /*
                    connectionString = connectionString.Replace("postgres://", string.Empty);
                    var pgUserPass = connectionString.Split("@")[0];
                    var pgHostPortDb = connectionString.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];
                    */
                    //connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}