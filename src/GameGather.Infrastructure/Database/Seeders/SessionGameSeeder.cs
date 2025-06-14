using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Infrastructure.Database.Seeders
{
    public class SessionGameSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {
            if (!context.PostGames.Any())
            {
                var existingUser = context.Users.FirstOrDefault();

                if (existingUser != null)
                {

                    var sessionGame = SessionGame.Create
                    (
                        gameMasterId: existingUser.Id,
                        gameMasterName: existingUser.FirstName + " " + existingUser.LastName,
                        name: "Smoczy zabójca dla początkujących",
                        description: "Jest to gra która pokazuje możliwości strony aplikacji i zwabienia ludzi do gry u nas"
                    );

                    context.SessionGames.Add(sessionGame);
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
