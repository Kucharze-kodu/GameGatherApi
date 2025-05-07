using GameGather.Application.Features.SessionGames.Commands.CreateSessionGames;
using GameGather.Application.Features.SessionGames.Commands.DeleteSessionGames;
using GameGather.Application.Features.SessionGames.Commands.EditSessionGames;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static GameGather.Api.Common.HttpResultsExtensions;

namespace GameGather.Api.Modules
{
    public static class SessionGameModule
    {
        public static void AddSessionGameModuleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/SessionGame/CreateSessionGame", async (
                [FromBody] CreateSessionGameCommnand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            });

            app.MapPost("/api/SessionGame/EditSessionGame", async (
                [FromBody] EditSessionGameCommnand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            });


            app.MapPost("/api/SessionGame/DeleteSessionGame", async (
                [FromBody] DeleteSessionGameCommnand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            });
        }
    }
}
