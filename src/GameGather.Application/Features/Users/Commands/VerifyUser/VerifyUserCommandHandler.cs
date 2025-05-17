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
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        // User not found
        if (user is null)
        {
            return Errors.User.NotFound;
        }
        
        var verificationToken = Guid.Parse(request.VerificationCode);
        
        // Invalid token or token expired or already used
        if (!user.Verify(verificationToken))
        {
            return Errors.User.InvalidToken;
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return "User verified successfully";
    }
}