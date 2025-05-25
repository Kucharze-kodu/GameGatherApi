

namespace GameGather.Application.Features.PostGames.Queries.DTOs
{
    public class GetAllPostGameDto
    {
        public int Id { get; set; }
        public string PostDescription { get; set; }
        public DateTime GameTime { get; set; }
        public DateTime DayPost { get; set; }
    }
}
