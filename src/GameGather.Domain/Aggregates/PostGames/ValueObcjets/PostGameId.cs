
using GameGather.Domain.Common.Primitives;

namespace GameGather.Domain.Aggregates.PostGames.ValueObcjets
{
    public sealed class PostGameId : ValueObject
    {

        public int Value { get; }

        private PostGameId(int value)
        {
            Value = value;
        }

        public static PostGameId Create(int id) => new PostGameId(id);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
