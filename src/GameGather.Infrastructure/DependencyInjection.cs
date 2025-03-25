using System.Reflection.Metadata;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace GameGather.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<GameGatherDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                r =>
                    r.MigrationsAssembly(typeof(DependencyInjection).Assembly.ToString())));
        try
        {
            using (var conn = new NpgsqlConnection(configuration.GetConnectionString("Default")))
            {
                conn.Open();
                Console.WriteLine("Połączenie z bazą danych działa!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd połączenia: {ex.Message}");
        }
        return services;
    }
}