using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace GameGather.Infrastructure.Configuration
{
    public sealed class SessionGameListConfigurationv : IEntityTypeConfiguration<SessionGameList>
    {

        public void Configure(EntityTypeBuilder<SessionGameList> builder)
        {
            builder
                .Property(c => c.UserId)
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

            // link many Users to many SessionGame
            builder
                .HasKey(sgl => new { sgl.UserId, sgl.SessionGameId });

            builder
                .Property(sgl => sgl.UserId)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            builder
                .Property(sgl => sgl.SessionGameId)
                .HasConversion(
                    id => id.Value,
                    value => SessionGameId.Create(value));


        }
    }
}
