using FizzWare.NBuilder;
using FluentAssertions;
using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.UnitTests.Entities
{
    public class GameTest
    {
        [Fact]
        public void CurrentRound_Should_Return_First_Round()
        {
            var rounds = Builder<Round>.CreateListOfSize(3).Build().ToList();

            var game = Builder<Game>.CreateNew().With(g=>g.Rounds = rounds).Build();

            // Assert
            game.CurrentRound.Should().Be(rounds.First());
        }

        [Fact]
        public void CurrentRound_Should_Return_Null_If_All_Rounds_Complete()
        {
            var rounds = Builder<Round>.CreateListOfSize(3).Build().ToList();
            rounds.ForEach(r => r.State = SystemState.Completed);

            var game = Builder<Game>.CreateNew().With(g => g.Rounds = rounds).Build();

            // Assert
            game.CurrentRound.Should().BeNull();
        }

        [Fact]
        public void IsGameComplete_Should_Return_True_If_All_Rounds_Complete()
        {
            var rounds = Builder<Round>.CreateListOfSize(3).Build().ToList();
            rounds.ForEach(r => r.State = SystemState.Completed);

            var game = Builder<Game>.CreateNew().With(g => g.Rounds = rounds).Build();

            // Assert
            game.IsGameComplete.Should().BeTrue();
        }

        [Fact]
        public void IsGameComplete_Should_Return_False_If_Has_Activet_Round()
        {
            var rounds = Builder<Round>.CreateListOfSize(3).Build().ToList();

            var game = Builder<Game>.CreateNew().With(g => g.Rounds = rounds).Build();

            // Assert
            game.IsGameComplete.Should().BeFalse();
        }
    }
}
