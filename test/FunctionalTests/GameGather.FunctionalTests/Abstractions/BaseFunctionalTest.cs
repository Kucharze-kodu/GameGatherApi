using GameGather.Application.Utils;
using GameGather.Infrastructure.Persistance;
using GameGather.IntegrationTests.TestUtils.Seeders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GameGather.FunctionalTests.Abstractions;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient HttpClient; 
    protected readonly ISender Sender; 
    protected readonly GameGatherDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;
    protected readonly IPasswordHasher PasswordHasher;
    
    protected BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        _scope = factory
            .Services
            .CreateScope();
        HttpClient = factory.CreateClient();
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