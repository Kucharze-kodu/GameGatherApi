
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
                var existingUser = context.Users.FirstOrDefault();
                var existingSessionGame = context.SessionGames.FirstOrDefault();

                if (existingUser != null && existingSessionGame != null)
                {
                    var comment = Comment.Create
                    (
                        userId: existingUser.Id,
                        sessionGameId: existingSessionGame.Id,
                        text: "walka walka"
                    );
                    context.Comments.Add(comment);
                    comment = Comment.Create
                    (
                        userId: existingUser.Id,
                        sessionGameId: existingSessionGame.Id,
                        text: "najwybitniejsze walki w rpgach"
                    );
                    context.Comments.Add(comment);
                }
            }
        }
    }
}
