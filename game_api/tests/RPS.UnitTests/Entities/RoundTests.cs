using FizzWare.NBuilder;
using FluentAssertions;
using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.UnitTests.Entities
{
    public class RoundTests
    {
        [Theory]
        [InlineData(2, true)]
        [InlineData(1, false)]

        public void HasMaximumAllowedPlayers_Should_Return_True_If_Two_Players_Joined(int playerCount, bool result)
        {
            var players = Builder<Player>.CreateListOfSize(playerCount).Build().ToList();

            var round = Builder<Round>.CreateNew().With(g=>g.Players = players).Build();

            // Assert
            round.HasMaximumAllowedPlayers.Should().Be(result);
        }

        [Fact]
        public void AllPlayersHaveMadeGestures_Should_Return_False_If_No_Gestures_Made()
        {
            var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
            players[0].Gesture = null;
            var round = Builder<Round>.CreateNew().With(g => g.Players = players).Build();

            // Assert
            round.AllPlayersHaveMadeGestures.Should().BeFalse();
        }
        [Fact]
        public void AllPlayersHaveMadeGestures_Should_Return_True_If_All_Gestures_Made()
        {
            var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
            var round = Builder<Round>.CreateNew().With(g => g.Players = players).Build();

            // Assert
            round.AllPlayersHaveMadeGestures.Should().BeTrue();
        }
    }
}
