using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGather.Infrastructure.Configuration
{
    public sealed class SessionGameConfiguration : IEntityTypeConfiguration<SessionGame>
    {
        public void Configure(EntityTypeBuilder<SessionGame> builder)
        {
            // Id
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .HasConversion(
                    c => c.Value,
                    value => SessionGameId.Create(value))
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.GameMasterId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

            builder.Navigation(sg => sg.PostGames)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
