using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using GameGather.Domain.Aggregates.Users;
using GameGather.UnitTests.Utils;

namespace GameGather.Domain.UnitTests.Aggregates.Users.TestUtils;

public class UserAssertions : ReferenceTypeAssertions<User, UserAssertions>
{
    
    public UserAssertions(User instance) : base(instance) {}
    
    protected override string Identifier => "UserAssertion";
    
    private const int MaxDifferenceInMinutes = 5;
    
    
    public AndConstraint<UserAssertions> BeValidAfterCreation(string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(user => user != null)
            .FailWith("{context:User} should not be null after creation")
            .Then
            .ForCondition(user => user!.FirstName == Constants.User.FirstName)
            .FailWith("{context:User} should have a valid FirstName after creation")
            .Then
            .ForCondition(user => user.LastName == Constants.User.LastName)
            .FailWith("{context:User} should have a valid LastName after creation")
            .Then
            .ForCondition(user => user.Email == Constants.User.Email)
            .FailWith("{context:User} should have a valid Email after creation")
            .Then
            .ForCondition(user => user.Password.Value == Constants.User.PasswordValue)
            .FailWith("{context:User} should have a valid Password after creation")
            .Then
            .ForCondition(user => user.Birthday == Constants.User.Birthday)
            .FailWith("{context:User} should have a valid Birthday after creation")
            .Then
            .ForCondition(user => Math.Abs((user!.CreatedOnUtc - DateTime.UtcNow).TotalMinutes) <= MaxDifferenceInMinutes)
            .FailWith("{context:User} should have a valid CreatedOnUtc after creation")
            .Then
            .ForCondition(user => user.VerifiedOnUtc == null)
            .FailWith("{context:User} should not have a VerifiedOnUtc after creation")
            .Then
            .ForCondition(user => Math.Abs((user!.LastModifiedOnUtc - DateTime.UtcNow).TotalMinutes) <= MaxDifferenceInMinutes)
            .FailWith("{context:User} should have a valid LastModifiedOnUtc after creation")
            .Then
            .ForCondition(user => user.VerificationToken != null)
            .FailWith("{context:User} should have a valid VerificationToken after creation")
            .Then
            .ForCondition(user => user.ResetPasswordToken == null)
            .FailWith("{context:User} should not have a ResetPasswordToken after creation")
            .Then
            .ForCondition(user => user.Ban == null)
            .FailWith("{context:User} should not have a Ban after creation")
            .Then
            .ForCondition(user => user.Role == Constants.User.Role)
            .FailWith("{context:User} should have a valid Role after creation");

        return new AndConstraint<UserAssertions>(this);
    }
}