using System.Reflection;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;


namespace GameGather.UnitTests.Utils.Builders.DomainSessionGame
{
    public class SessionGameIdBuilder
    {
        private int _value = Constants.SessionGameId.Value;

        public SessionGameIdBuilder WithValue(int value)
        {
            _value = value;
            return this;
        }

        public SessionGameId Build()
        {
            var type = typeof(SessionGameId);

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new[] { typeof(int) },
                null
            );

            if (constructor == null)
            {
                throw new InvalidOperationException($"Constructor not found for {nameof(SessionGameId)}");
            }

            var sessionGameId = (SessionGameId)constructor.Invoke(new object[] { _value });

            type.GetField("<Value>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(sessionGameId, _value);

            return sessionGameId;
        }
    }
}
