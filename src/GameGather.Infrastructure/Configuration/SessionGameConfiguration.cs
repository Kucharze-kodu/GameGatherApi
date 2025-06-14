using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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


            // link SessionGame to commnets
            /*        modelBuilder.Entity<SessionGame>()
                        .HasMany(sg => sg.Comments)
                        .WithOne(pg => pg.SessionGame)
                        .HasForeignKey(c => c.SessionGameId)
                        .OnDelete(DeleteBehavior.Cascade);*/

            builder
                .HasMany(sg => sg.Comments)
                .WithOne()
                .HasForeignKey(c => c.SessionGameId)
                .OnDelete(DeleteBehavior.Cascade);

            // link SessionGame to postGame
            builder
                 .HasMany(sg => sg.PostGames)
                 .WithOne(pg => pg.SessionGame)
                 .HasForeignKey(pg => pg.SessionGameId)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
