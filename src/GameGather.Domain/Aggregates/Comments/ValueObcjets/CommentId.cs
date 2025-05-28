using GameGather.Domain.Common.Primitives;

namespace GameGather.Domain.Aggregates.Comments.ValueObcjets
{
    public sealed class CommentId : ValueObject
    {
        public int Value { get; }

        private CommentId(int value) 
        {
            Value = value;
        }

        public static CommentId Create(int id) => new CommentId(id);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
