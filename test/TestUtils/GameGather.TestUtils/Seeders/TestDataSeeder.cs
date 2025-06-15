using Bogus;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using GameGather.UnitTests.Utils.Builders.DomainUsers;

namespace GameGather.IntegrationTests.TestUtils.Seeders;

public class TestDataSeeder
{
    private readonly GameGatherDbContext _dbContext;
    private readonly Faker _faker;
    private readonly IPasswordHasher _passwordHasher;

    public TestDataSeeder(GameGatherDbContext dbContext, 
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _faker = new Faker();
    }

    public async Task SeedAsync()
    {
        await SeedUsersAsync();
        await _dbContext.SaveChangesAsync();

    }
    
    private async Task SeedUsersAsync()
    {
        if (_dbContext.Users.Any())
        {
            return; // Users already seeded
        }
        
        var amount = 3;
        var id = amount + 1;
        
        var users = _faker.MakeLazy(
            amount,
            () => new UserBuilder()
                .WithUserId(new UserIdBuilder()
                    .WithValue(id++)
                    .Build())
                .WithFirstName(_faker.Person.Random.Word())
                .WithLastName(_faker.Person.Random.Word())
                .WithEmail(_faker.Internet.Email())
                .WithPassword(new PasswordBuilder()
                    .WithValue(_passwordHasher
                        .Hash(_faker.Internet.Password()))
                    .WithLastModifiedOnUtc(DateTime.SpecifyKind(_faker
                        .Date.Recent(), DateTimeKind.Utc))
                    .Build())
                .WithBirthday(DateTime.SpecifyKind(_faker
                    .Date.Past(30), DateTimeKind.Utc))
                .WithCreatedOnUtc(DateTime.SpecifyKind(_faker
                    .Date.Past(), DateTimeKind.Utc))
                .WithLastModifiedOnUtc(DateTime.SpecifyKind(_faker
                    .Date.Recent(), DateTimeKind.Utc))
                .WithVerifiedOnUtc(DateTime.SpecifyKind(_faker
                    .Date.Recent(7), DateTimeKind.Utc))
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithValue(_faker.Random.Guid())
                    .WithCreatedOnUtc(DateTime.SpecifyKind(_faker
                        .Date.Past(7), DateTimeKind.Utc))
                    .WithExpiresOnUtc(DateTime.SpecifyKind(_faker
                        .Date.Future(7), DateTimeKind.Utc))
                    .WithLastSendOnUtc(DateTime.SpecifyKind(_faker
                        .Date.Recent(7), DateTimeKind.Utc))
                    .WithUsedOnUtc(DateTime.SpecifyKind(_faker
                        .Date.Past(7), DateTimeKind.Utc))
                    .Build())
                .WithResetPasswordToken(null)
                .WithBan(null)
                .WithRole(Role.User));

        var userWithExpiredPassword = users
            .ElementAt(amount - 1)
            .WithEmail(Constants.LoginUserWithExpiredPassword.Email)
            .WithPassword(new PasswordBuilder()
                .WithValue(_passwordHasher
                    .Hash(Constants.LoginUserWithExpiredPassword.Password))
                .WithExpiredPassword()
                .Build());

        var userWithNotVerifiedEmail = users
            .ElementAt(amount - 2)
            .WithEmail(Constants.LoginUserWithNotVerifiedEmail.Email)
            .WithPassword(new PasswordBuilder()
                .WithValue(_passwordHasher
                    .Hash(Constants.LoginUserWithNotVerifiedEmail.Password))
                .Build())
            .WithVerifiedOnUtc(null);
        
        var userWithValidCredentials = users
            .ElementAt(amount - 3)
            .WithEmail(Constants.LoginUser.Email)
            .WithPassword(new PasswordBuilder()
                .WithValue(_passwordHasher
                    .Hash(Constants.LoginUser.Password))
                .Build());
        
        var usersToAdd = users
            .Select(r => r.Build())
            .Append(userWithExpiredPassword.Build())
            .Append(userWithNotVerifiedEmail.Build())
            .Append(userWithValidCredentials.Build())
            .ToList();
            
        
        await _dbContext.Users.AddRangeAsync(usersToAdd);
    }
}