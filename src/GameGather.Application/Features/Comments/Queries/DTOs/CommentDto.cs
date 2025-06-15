using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.Application.Features.Comments.Queries.DTOs
{
    public class CommentDto
    {
        public CommentId Id { get; set; }
        public UserId UserId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime DateComment { get; set; }
    }
}