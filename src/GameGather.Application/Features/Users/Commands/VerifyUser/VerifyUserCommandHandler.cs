using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Persistance;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.Users.Commands.VerifyUser;

public class VerifyUserCommandHandler : ICommandHandler<VerifyUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        
        // User not found
        if (user is null)
        {
            return Errors.User.NotFound;
        }
        
        // Invalid token or token expired
        if (!user.Verify(request.Token))
        {
            return Errors.User.InvalidToken;
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return "User verified successfully";
    }
}