namespace GameGather.Api.Common;

public static class HttpResultsExtensions
{
    public static IResult Ok(object value)
    {
        return Results.Ok(value);
    }
}