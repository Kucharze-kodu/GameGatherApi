using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Utils.Outbox;
using Microsoft.EntityFrameworkCore;

namespace GameGather.Infrastructure.Persistance;

public class GameGatherDbContext : DbContext
{
    public GameGatherDbContext(DbContextOptions<GameGatherDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public DbSet<SessionGame> SessionGames { get; set; }
    public DbSet<SessionGameList> SessionGameLists { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostGame> PostGames { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(GameGatherDbContext).Assembly);
        base.OnModelCreating(modelBuilder);






    }
}