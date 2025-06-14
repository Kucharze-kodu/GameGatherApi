

namespace GameGather.Application.Contracts.PostGame
{
    public record GetAllPostGameResponse
    (
        int Id,
        string PostDescription,
        DateTime GameTime,
        DateTime DayPost
    );
}
