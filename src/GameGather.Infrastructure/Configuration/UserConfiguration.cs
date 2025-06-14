using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGather.Infrastructure.Configuration;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Id
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .HasConversion(
                c => c.Value,
                value => UserId.Create(value))
            .ValueGeneratedOnAdd();

        // Firstname
        builder
            .Property(r => r.FirstName)
            .HasMaxLength(100);

        // Lastname
        builder
            .Property(r => r.LastName)
            .HasMaxLength(100);
        
        // Email
        builder
            .Property(r => r.Email)
            .HasMaxLength(255);

        builder
            .HasIndex(r => r.Email)
            .IsUnique();
        
        // Password
        builder
            .OwnsOne(r => r.Password, password =>
            {
                password.ToTable("Passwords");
                password.Property(x => x.Value).HasColumnName("Value");
                password.Property(x => x.LastModifiedOnUtc).HasColumnName("LastModifiedOnUtc");
            });
        
        // VerificationToken
        builder
            .OwnsOne(r => r.VerificationToken, token =>
            {
                token.ToTable("VerificationTokens");
                token.Property(x => x.Value).HasColumnName("Value");
                token.Property(x => x.CreatedOnUtc).HasColumnName("CreatedOnUtc");
                token.Property(x => x.ExpiresOnUtc).HasColumnName("ExpiresOnUtc");
                token.Property(x => x.Type).HasColumnName("Type");
            });
        
        // ResetPasswordToken
        builder
            .OwnsOne(r => r.ResetPasswordToken, token =>
            {
                token.ToTable("ResetPasswordTokens");
                token.Property(x => x.Value).HasColumnName("Value");
                token.Property(x => x.CreatedOnUtc).HasColumnName("CreatedOnUtc");
                token.Property(x => x.ExpiresOnUtc).HasColumnName("ExpiresOnUtc");
                token.Property(x => x.Type).HasColumnName("Type");
            });
        
        // Ban
        builder
            .OwnsOne(r => r.Ban, ban =>
            {
                ban.ToTable("Bans");
                ban.Property(x => x.CreatedOnUtc).HasColumnName("CreatedOnUtc");
                ban.Property(x => x.ExpiresOnUtc).HasColumnName("ExpiresOnUtc");
                ban.Property(x => x.Message).HasColumnName("Message");
            });
        
        // Role
        builder
            .Property(r => r.Role)
            .HasConversion(
                c => c.ToString(),
                c => (Role)Enum.Parse(typeof(Role), c))
            .HasColumnName("Role");


        //link User to commnets
        builder
            .HasMany(sg => sg.Comments)
            .WithOne(pg => pg.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}