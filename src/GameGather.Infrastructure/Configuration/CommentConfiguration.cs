using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGather.Infrastructure.Configuration
{
    public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(r => r.Id)
                .HasConversion(
                    c => c.Value,
                    value => CommentId.Create(value))
                .ValueGeneratedOnAdd();

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
        } 
    }
}
