using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.PostGames.Queries.DTOs;
using GameGather.Application.Features.PostGames.Queries.GetAllPostGame;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Common.Errors;



namespace GameGather.Application.Features.PostGames.Quries.GetAllPostGame
{
    public class GetAllPostGameQueryHandler : ICommandHandler<GetAllPostGameQuery, List<GetAllPostGameDto>>
    {
        private readonly IPostGameRepository _postGameRepository;
        private readonly IUserContext _userContext;

        public GetAllPostGameQueryHandler(IPostGameRepository postGameRepository,
            IUserContext userContext)
        {
            _postGameRepository=postGameRepository;
            _userContext=userContext;
        }

        public async Task<ErrorOr<List<GetAllPostGameDto>>> Handle(GetAllPostGameQuery request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.SessionGameId));
            var result = await _postGameRepository.GetAllPostGameSession(sessionGameId);

            if (result is null)
            {
                return Errors.PostGame.IsWrongData;
            }


            List<GetAllPostGameDto> listOfPostGame = result.Select(x => new GetAllPostGameDto
            {
                Id = Convert.ToInt32(x.Id.Value),
                PostDescription = x.PostDescription,
                DayPost = x.DayPost,
                GameTime = x.GameTime
            }).ToList();


            return listOfPostGame;
        }
    }
}
