using static GameGather.Api.Common.HttpResultsExtensions;

namespace GameGather.Api.Modules;

public static class TestModule
{
    public static void AddTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ping", async () =>
        {
            return Ok("pong");
        });
    }
}