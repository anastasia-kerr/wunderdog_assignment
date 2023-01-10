using FizzWare.NBuilder;
using NSubstitute;
using RPS.Application.Models.Game;
using RPS.Application.Services;
using RPS.Core.Entities;
using FluentAssertions;
using System.Linq.Expressions;
using RPS.Application.Exceptions;
using RPS.Core.Exceptions;
using RPS.Core.Enums;

namespace RPS.UnitTests.Services;

public class PlayersServiceTests : BaseServiceTestConfiguration
{
    private readonly IPlayersService _playerPlayService;

    public PlayersServiceTests()
    {
        _playerPlayService = new PlayersService(_gameRepository);
    }
    [Fact]
    public async Task LeaveCurrentRound_Should_Remove_Player()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var firstPlayer = players.First();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<LeaveCurrentRoundModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = firstPlayer.Identifier)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        await _playerPlayService.LeaveCurrentRound(game.Id, command);

        //Assert
        await _gameRepository.Received().UpdateAsync(Arg.Any<Game>());
        round.Players.Count.Should().Be(1);
        round.Players.Find(p => p.Identifier == command.PlayerIdentifier).Should().BeNull();
    }

    [Fact]
    public async Task LeaveCurrentRound_Should_Reset_Satet_If_No_Players_Left()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(1).Build().ToList();
        var firstPlayer = players.First();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<LeaveCurrentRoundModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = firstPlayer.Identifier)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        await _playerPlayService.LeaveCurrentRound(game.Id, command);

        //Assert
        await _gameRepository.Received().UpdateAsync(Arg.Any<Game>());
        round.State.Should().Be(SystemState.Created);
    }

    [Fact]
    public async Task JoinCurrentRound_Should_Throw_If_More_Than_Two_Players()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<JoinRoundModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = "New Player Id")
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        Func<Task> callUpdateAsync = async () => await _playerPlayService.JoinCurrentRound(game.Id, command);
        //Assert

        await callUpdateAsync.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task JoinCurrentRound_Should_Add_Player()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(1).Build().ToList();

        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<JoinRoundModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = "New Player Id")
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _playerPlayService.JoinCurrentRound(game.Id, command);
        //Assert

        result.RoundNumber.Should().Be(1);
        players.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetCurrentGamePlayers_Should_Return_Nicknames()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();

        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();
        
        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);

        //Act
        var result = await _playerPlayService.GetCurrentGamePlayers(game.Id);
        //Assert

        result.Players.Should().Contain(players.Select(p=>p.Nickname));
        result.Players.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetCurrentGamePlayers_Should_Throw_If_Game_Is_Finished()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();

        var round = Builder<Round>.CreateNew().With(r => r.Players = players).With(r=>r.State= SystemState.Completed).Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);

        //Act
        Func<Task> callUpdateAsync = async () => await _playerPlayService.GetCurrentGamePlayers(game.Id);
        //Assert

        await callUpdateAsync.Should().ThrowAsync<GameCompleteException>();
    }
}
