using FluentValidation;
using GameGather.Application.Features.PostGames.Commands.CreatePostGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Application.Features.PostGames.Commands.EditPostGames
{
    public class EditPostGameCommandValidator : AbstractValidator<EditPostGameCommand>
    {
        public EditPostGameCommandValidator()
        {
            RuleFor(r => r.GameSessionId)
           .NotEmpty()
           .WithMessage("SessionId is required");

            RuleFor(r => r.PostDescription)
            .NotEmpty()
            .WithMessage("Description post is required");

            RuleFor(r => r.GameTime)
            .NotEmpty()
            .WithMessage("Time of the game is required");
        }
    }
}
