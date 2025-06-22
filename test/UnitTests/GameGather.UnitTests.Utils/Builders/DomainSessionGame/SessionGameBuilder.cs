using System.Reflection;
using System.Runtime.Serialization;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.UnitTests.Utils.Builders.DomainUsers;
using GameGather.Domain.Aggregates.SessionGames;

namespace GameGather.UnitTests.Utils.Builders.DomainSessionGame;

public class SessionGameBuilder
{
    private SessionGameId _sessionGameId = new SessionGameIdBuilder()
        .WithValue(Constants.SessionGame.SessionGameIdValue)
        .Build();
    private string _name = Constants.SessionGame.Name;
    private string _description = Constants.SessionGame.Description;
    private UserId _gameMasterId = new UserIdBuilder()
        .WithValue(Constants.SessionGame.GameMasterIdValue)
        .Build();
    private string _gameMasterName = Constants.SessionGame.GameMasterName;

    public SessionGameBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SessionGameBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public SessionGameBuilder WithGameMasterId(UserId gameMasterId)
    {
        _gameMasterId = gameMasterId;
        return this;
    }

    public SessionGameBuilder WithGameMasterName(string gameMasterName)
    {
        _gameMasterName = gameMasterName;
        return this;
    }

    public SessionGame EmptyObject()
    {
        return (SessionGame)FormatterServices.GetUninitializedObject(typeof(SessionGame));
    }

    public SessionGame Build()
    {
        var type = typeof(SessionGame);

        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[]
            {
                typeof(SessionGameId),
                typeof(string),
                typeof(string),
                typeof(UserId),
                typeof(string)
            },
            null
        );

        if (constructor == null)
        {
            throw new InvalidOperationException($"Constructor not found for {nameof(SessionGame)}");
        }

        var instance = (SessionGame)constructor.Invoke(new object[]
        {
            _sessionGameId,
            _name,
            _description,
            _gameMasterId,
            _gameMasterName
        });

        type.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(instance, _sessionGameId);
        type.GetField("<Name>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(instance, _name);
        type.GetField("<Description>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(instance, _description);
        type.GetField("<GameMasterId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(instance, _gameMasterId);
        type.GetField("<GameMasterName>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(instance, _gameMasterName);

        return instance;
    }
}
