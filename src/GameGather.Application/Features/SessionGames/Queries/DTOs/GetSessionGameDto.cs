

namespace GameGather.Application.Features.SessionGames.Queries.DTOs
{
    public class GetSessionGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameMasterName { get; set; }

        public List<string> PlayerName { get; set; }
    }
}
