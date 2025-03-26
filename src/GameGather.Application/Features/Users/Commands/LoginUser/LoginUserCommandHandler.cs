using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Users;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.Users.Commands.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Errors.User.InvalidCredentials;
        }

        // Todo: Fix to check hashed password
        if (user.Password.Value != request.Password)
        {
            return Errors.User.InvalidCredentials;
        }
        
        // Todo: Check if password is expired

        var token = _jwtProvider.GenerateToken(user);

        return new LoginUserResponse(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            token.Token,
            token.ExpiresOnUtc);

    }
}