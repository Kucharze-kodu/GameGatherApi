using GameGather.Application.Features.Users.Commands.LoginUser;
using GameGather.Application.Features.Users.Commands.RegisterUser;
using GameGather.Application.Features.Users.Commands.ResendVerificationToken;
using GameGather.Application.Features.Users.Commands.VerifyUser;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using static GameGather.Api.Common.HttpResultsExtensions;

namespace GameGather.Api.Modules;

public static class AuthenticationModule
{
    public static void AddAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/register", async (
            [FromBody] RegisterUserCommand command,
            [FromServices] ISender sender) =>
        {
            var response = await sender.Send(command);

            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        });

        app.MapPost("/api/login", async (
            [FromBody] LoginUserCommand command,
            [FromServices] ISender sender) =>
        {
            var response = await sender.Send(command);

            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        });
        
        app.MapPost("/api/verify-email", async (
            [FromBody] VerifyUserCommand command,
            [FromServices] ISender sender) =>
        {
            var response = await sender.Send(command);

            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        });

        app.MapPost("/api/resend-verification-token", async(
            [FromBody] ResendVerificationTokenCommand command,
            [FromServices] ISender sender) =>
        {
            var response = await sender.Send(command);

            return response.Match(
                result => Ok(result),
                errors => Problem(errors));
        });
    }
}