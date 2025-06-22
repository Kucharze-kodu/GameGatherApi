using FluentAssertions;
using GameGather.Domain.Aggregates.SessionGames;
using GameGather.UnitTests.Utils.ConstantValue.SessionGameConstats;


namespace GameGather.Domain.UnitTests.Aggregates.SessionGames
{
    public class SessionGameTests
    {
        [Fact]
        public void Create_ShouldCreateSessionGame_WhenValidParametersAreProvided()
        {
            // Arrange


            // Act

            var sessionGame = SessionGame.Create(
                           name,
                           Constants.Description,
                           gameMasterId,
                           Constants.GameMasterName
                       );

            // Assert


        }
    }
}
