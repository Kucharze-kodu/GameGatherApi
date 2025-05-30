﻿using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Primitives;


namespace GameGather.Domain.Aggregates.Comments
{
    public sealed class Comment : AggregateRoot<CommentId>
    {
        public UserId UserId { get; private set; }
        public SessionGameId SessionGameId { get; private set; }
        public string Text { get; private set; }
        public DateTime DateComment { get; private set; }

        public Comment(CommentId id) : base(id)
        {
        }

        private Comment(
            CommentId id,
            UserId userId,
            SessionGameId sessionGameId,
            string text,
            DateTime dateComment) : base(id)
        {
            UserId = userId;
            SessionGameId = sessionGameId;
            Text = text;
            DateComment = dateComment;
        }


        public Comment Create(
            UserId userId,
            SessionGameId sessionGameId,
            string text)
        {
            var comment = new Comment(
                default,
                userId,
                sessionGameId,
                text,
                DateTime.Today
                );

            return comment;
        }

        public Comment Load(
            CommentId id,
            UserId userId,
            SessionGameId sessionGameId,
            string text,
            DateTime dateComment)
        {
            Id = id;
            UserId = userId;
            SessionGameId = sessionGameId;
            Text = text;
            DateComment = dateComment;

            return this;
        }
    }
}