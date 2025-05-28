using FluentValidation;
using GameGather.Application.Features.SessionGames.Commands.CreateSessionGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Application.Features.SessionGames.Commands.DeleteSessionGames
{
    public class DeleteSessionGameCommnandValidator : AbstractValidator<DeleteSessionGameCommnand>
    {
        public DeleteSessionGameCommnandValidator()
        {
            RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        }
    }
}
