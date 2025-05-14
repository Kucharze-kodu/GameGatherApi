using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.SessionGames.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Application.Features.SessionGames.Queries.GetSessionGame
{
    public record GetSessionGameQuery(
        int IdSession
    ) : ICommand<GetSessionGameDto>;
}
