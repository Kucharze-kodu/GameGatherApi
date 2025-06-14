using GameGather.Application.Features.PostGames.Commands.CreatePostGames;
using GameGather.Application.Features.PostGames.Commands.DeletePostGames;
using GameGather.Application.Features.PostGames.Commands.EditPostGames;
using GameGather.Application.Features.PostGames.Queries.GetAllPostGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static GameGather.Api.Common.HttpResultsExtensions;



namespace GameGather.Api.Modules
{
    public static class PostGameModule
    {
        public static void AddPostGameEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/post-game", async (
                [FromBody] CreatePostGameCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("PostGame"); ;


            app.MapPut("/api/post-game", async (
                [FromBody] EditPostGameCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("PostGame");


            app.MapDelete("/api/post-game", async (
                [FromBody] DeletePostGameCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("PostGame");


            app.MapGet("/api/post-game", async (
            [AsParameters] GetAllPostGameQuery query,
            [FromServices] ISender sender) =>
            {
                var response = await sender.Send(query);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("PostGame");

        }
    }
}
