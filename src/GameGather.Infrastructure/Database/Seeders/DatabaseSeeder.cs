using GameGather.Infrastructure.Persistance;


namespace GameGather.Infrastructure.Database.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {


            await SessionGameSeeder.SeedAsync(context);
            await PostGameSeeder.SeedAsync(context);
            await CommentSeeder.SeedAsync(context);


            await context.SaveChangesAsync();
        }
    }
}
