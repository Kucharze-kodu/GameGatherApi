
using GameGather.Application.Features.PlayerManagers.Commands.AddPlayerManagers;
using GameGather.Application.Features.PlayerManagers.Commands.RemovePlayerManagers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static GameGather.Api.Common.HttpResultsExtensions;

namespace GameGather.Api.Modules
{
    public static class PlayerManagerModule
    {
        public static void AddPlayerManagerEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/Player/AddPlayerToSession", async (
                [FromBody] AddPlayerManagerCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            });

            app.MapPost("/api/Player/RemovePlayerFromSession", async (
                [FromBody] RemovePlayerManagerCommand command,
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
