using GameGather.Application.Features.Comments.Commands.CreateComments;
using GameGather.Application.Features.Comments.Commands.DeleteComments;
using GameGather.Application.Features.Comments.Commands.EditComments;
using GameGather.Application.Features.Comments.Queries.GetAllComments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using static GameGather.Api.Common.HttpResultsExtensions;



namespace GameGather.Api.Modules
{
    public static class CommentModule
    {
        public static void AddCommentEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/comment", async (
               [FromBody] CreateCommentCommand command,
               [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
           .WithTags("Comment"); ;


            app.MapPut("/api/comment", async (
                [FromBody] EditCommentCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("Comment");


            app.MapDelete("/api/comment", async (
                [FromBody] DeleteCommentCommand command,
                [FromServices] ISender sender) =>
            {
                var response = await sender.Send(command);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("Comment");


            app.MapGet("/api/comment", async (
            [AsParameters] GetAllCommentQuery query,
            [FromServices] ISender sender) =>
            {
                var response = await sender.Send(query);

                return response.Match(
                    result => Ok(result),
                    errors => Problem(errors));
            }).RequireAuthorization()
            .WithTags("Comment");
        }
    }
}