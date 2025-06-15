using GameGather.Application.Utils;
using GameGather.Infrastructure.Persistance;
using GameGather.IntegrationTests.TestUtils.Seeders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GameGather.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender; 
    protected readonly GameGatherDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;
    protected readonly IPasswordHasher PasswordHasher;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory
            .Services
            .CreateScope();

        Sender = _scope
            .ServiceProvider
            .GetRequiredService<ISender>();
        DbContext = _scope
            .ServiceProvider
            .GetRequiredService<GameGatherDbContext>();
        PasswordHasher = _scope
            .ServiceProvider
            .GetRequiredService<IPasswordHasher>();
        
        DataSeeder = new TestDataSeeder(DbContext, PasswordHasher);
    }

    public async Task InitializeAsync()
    {
        await DataSeeder.SeedAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}