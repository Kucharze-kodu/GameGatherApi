
using GameGather.Domain.Aggregates.Comments;
using GameGather.Infrastructure.Persistance;


namespace GameGather.Infrastructure.Database.Seeders
{
    public class CommentSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {
            if (!context.Comments.Any())
            {
                var existingUser1 = context.Users.FirstOrDefault(x => x.FirstName =="Jan");
                var existingUser2 = context.Users.FirstOrDefault(x => x.FirstName =="Anna");
                var existingSessionGame = context.SessionGames.FirstOrDefault();

                if (existingUser1 != null&& existingUser2 != null && existingSessionGame != null)
                {
                    var comment = Comment.Create
                    (
                        userId: existingUser1.Id,
                        sessionGameId: existingSessionGame.Id,
                        text: "walka walka"
                    );
                    context.Comments.Add(comment);

                    comment = Comment.Create
                    (
                        userId: existingUser2.Id,
                        sessionGameId: existingSessionGame.Id,
                        text: "najwybitniejsze walki w rpgach"
                    );
                    context.Comments.Add(comment);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
