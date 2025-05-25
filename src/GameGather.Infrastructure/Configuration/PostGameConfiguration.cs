using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.Infrastructure.Configuration
{
    public sealed class PostGameConfiguration : IEntityTypeConfiguration<PostGame>
    {

        public void Configure(EntityTypeBuilder<PostGame> builder)
        {
            // Id
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .HasConversion(
                    c => c.Value,
                    value => PostGameId.Create(value))
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.GameMasterId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

            builder
               .Property(c => c.SessionGameId)
               .HasConversion(
                   id => id.Value,
                   value => SessionGameId.Create(value)
               );

        }
    }
}
   

