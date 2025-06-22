using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Infrastructure.Database.Seeders
{
    public class PlayerManagerSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {
            if (!context.SessionGameLists.Any())
            {
                var existingUser = context.Users.FirstOrDefault(x => x.FirstName == "Anna");
                var existingSessionGame = context.SessionGames.FirstOrDefault();

                if(existingUser != null && existingSessionGame != null)
                {

                    var playerManager = SessionGameList.Create
                        (
                            userId :existingUser.Id,
                            sessionGameId :existingSessionGame.Id
                        );
                    context.SessionGameLists.Add(playerManager);
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
