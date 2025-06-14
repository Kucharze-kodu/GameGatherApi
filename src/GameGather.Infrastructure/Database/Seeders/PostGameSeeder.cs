using GameGather.Domain.Aggregates.PostGames;
using GameGather.Infrastructure.Persistance;


namespace GameGather.Infrastructure.Database.Seeders
{
    public class PostGameSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {
            if (!context.PostGames.Any())
            {
                var existingUser = context.Users.FirstOrDefault();
                var existingSessionGame = context.SessionGames.FirstOrDefault();

                if (existingUser != null && existingSessionGame != null)
                {
                    var postGame = PostGame.Create
                    (
                        gameMasterId: existingUser.Id,
                        sessionGameId: existingSessionGame.Id,
                        gameTime: DateTime.UtcNow.AddHours(+2),
                        postDescription: "Sesja testowa i zapoznawcza dla graczy"
                    );

                    context.PostGames.Add(postGame);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
