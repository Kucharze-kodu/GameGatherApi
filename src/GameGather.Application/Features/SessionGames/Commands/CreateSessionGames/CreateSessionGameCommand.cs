﻿using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;

namespace GameGather.Application.Features.SessionGames.Commands.CreateSessionGames
{
    public record CreateSessionGameCommand(
        string Name,
        string Description

        ) : ICommand<SessionGameResponse>;
}
