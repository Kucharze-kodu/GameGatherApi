using System.Reflection.Metadata;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameGather.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<GameGatherDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                r =>
                    r.MigrationsAssembly(typeof(AssemblyReference).Assembly.ToString())));
        return services;
    }
}