

using GameGather.Domain.Aggregates.Users;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GameGather.Infrastructure.Database.Seeders
{
    public class UserSeeder
    {
        public static async Task SeedAsync(GameGatherDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var users = new List<User>
        {
            User.Create(
                firstName: "Jan",
                lastName: "Kowalski",
                email: "jan.kowalski@example.com",
                password: "SecurePassword123!",
                birthday: DateTime.Parse("1990-05-10T22:04:11.925Z").ToUniversalTime(),
                verifyEmailUrl: ""
            ),
            User.Create(
                firstName: "Anna",
                lastName: "Nowak",
                email: "anna.nowak@example.com",
                password: "AnotherPassword456!",
                birthday: DateTime.Parse("1985-11-03T22:04:11.925Z").ToUniversalTime(),
                verifyEmailUrl: ""
            )
        };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            // Opcjonalnie — ręczne oznaczenie jako zweryfikowanych:
            foreach (var user in users)
            {
                user.Verify(user.VerificationToken.Value); // lub bez weryfikacji — zależnie od potrzeb
            }

            await context.SaveChangesAsync();

        }
    }
}
