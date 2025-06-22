using GameGather.Infrastructure.Database.Seeders;
using GameGather.Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace GameGather.Infrastructure.Utils.Extensions
{
    public static class SeederExtensions
    {
        public async static void ApplySeeder(this WebApplication app)
        {
            using var scope = app
                .Services.
                CreateScope();
            var dbContext = scope
            .ServiceProvider
            .GetRequiredService<GameGatherDbContext>();
            
            await DatabaseSeeder.SeedAsync(dbContext);
        }
    }
}


