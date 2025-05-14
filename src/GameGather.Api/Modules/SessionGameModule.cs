using GameGather.Application.Features.SessionGames.Commands.CreateSessionGames;
using GameGather.Application.Features.SessionGames.Commands.DeleteSessionGames;
using GameGather.Application.Features.SessionGames.Commands.EditSessionGames;
using GameGather.Application.Features.SessionGames.Queries.GetAllSessionGame;
using GameGather.Application.Features.SessionGames.Queries.GetSessionGame;
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
            }).RequireAuthorization()
            .WithTags("SessionGame");

            app.MapPost("/api/SessionGame/EditSessionGame", async (
                [FromBody] EditSessionGameCommnand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("SessionGame");

            app.MapPost("/api/SessionGame/DeleteSessionGame", async (
                [FromBody] DeleteSessionGameCommnand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("SessionGame");


            app.MapGet("/api/SessionGame/GetAllSessionGame", async (
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(new GetAllSessionGameQuery());

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            })
            .WithTags("SessionGame");

            app.MapGet("/api/SessionGame/GetSessionGame", async (
                [AsParameters] GetSessionGameQuery query, 
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(query);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("SessionGame");
        }
    }
}
