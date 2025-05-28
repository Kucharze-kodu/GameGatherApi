using GameGather.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Domain.Aggregates.SessionGames.ValueObcjects
{
    public sealed class SessionGameId : ValueObject
    {
        public int Value { get; }

        private SessionGameId(int value)
        {
            Value = value;
        }

        public static SessionGameId Create(int id) => new SessionGameId(id);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

