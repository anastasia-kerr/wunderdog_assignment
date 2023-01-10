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

public class GamePlayServiceTests : BaseServiceTestConfiguration
{
    private readonly ITaskService _gamePlayService;

    public GamePlayServiceTests()
    {
        _gamePlayService = new TaskService(_gameRepository, Mapper);
    }

    [Fact]
    public async Task CreateGameAsync_Should_Return_New_Game_Id()
    {
        //Arrange
        var command = Builder<SetLightModel>.CreateNew().Build();
        var game = Mapper.Map<Game>(command);
        game.Id = 1;

        _gameRepository.AddAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.CreateGameAsync(command);

        //Assert
        result.Id.Should().Be(game.Id);
        await _gameRepository.Received().AddAsync(Arg.Any<Game>());

    }

    [Fact]
    public async Task CreateGameAsync_Should_Make_Correct_Number_Of_Rounds()
    {
        //Arrange
        var command = Builder<SetLightModel>.CreateNew().Build();
        command.NumberOfRounds = 22;
        var game = Mapper.Map<Game>(command);

        _gameRepository.AddAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.CreateGameAsync(command);

        //Assert
        result.Id.Should().Be(game.Id);
        await _gameRepository.Received().AddAsync(Arg.Is<Game>(x=>x.Rounds.Count == command.NumberOfRounds));
    }
    [Fact]
    public async Task PlaceRoundGesture_Should_Assign_Gesture_to_Player()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var firstPlayer = players.First();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<PlaceRoundGestureModel>
            .CreateNew()
            .With(x=>x.PlayerIdentifier = firstPlayer.Identifier)
            .With(x => x.PlayerNickname = firstPlayer.Nickname)
            .With(x => x.RoundGesture = Gesture.Scissors)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();

        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.PlaceRoundGesture(game.Id, command);

        //Assert
        result.TaskId.Should().Be(game.Id);
         await _gameRepository.Received().UpdateAsync(Arg.Any<Game>());
        round.Players.Find(x => x.Identifier == command.PlayerIdentifier).Gesture.Should().Be(command.RoundGesture);
    }

    [Fact]
    public async Task PlaceRoundGesture_Should_Set_Round_To_Complete_If_All_Users_Made_Gestures()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var firstPlayer = players.First();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<PlaceRoundGestureModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = firstPlayer.Identifier)
            .With(x => x.PlayerNickname = firstPlayer.Nickname)
            .With(x => x.RoundGesture = Core.Enums.Gesture.Scissors)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.PlaceRoundGesture(game.Id, command);

        //Assert
        round.State.Should().Be(Core.Enums.SystemState.Completed);
    }
    [Fact]
    public async Task PlaceRoundGesture_Not_All_Players_Made_Gesture_Should_Not_Set_Round_To_Complete_()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var firstPlayer = players.First();
        var secondPlayer = players[1];
        secondPlayer.Gesture = null;

        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<PlaceRoundGestureModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = firstPlayer.Identifier)
            .With(x => x.PlayerNickname = firstPlayer.Nickname)
            .With(x => x.RoundGesture = Core.Enums.Gesture.Scissors)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.PlaceRoundGesture(game.Id, command);

        //Assert
        round.State.Should().NotBe(Core.Enums.SystemState.Completed);
    }

    [Fact]
    public async Task PlaceRoundGesture_Should_Throw_If_Player_Is_Not_In_The_Game()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        var firstPlayer = players.First();
        var round = Builder<Round>.CreateNew().With(r => r.Players = players).Build();

        var command = Builder<PlaceRoundGestureModel>
            .CreateNew()
            .With(x => x.PlayerIdentifier = "Not a correct id")
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        Func<Task> callUpdateAsync = async () => await _gamePlayService.PlaceRoundGesture(game.Id, command);

        //Assert

        await callUpdateAsync.Should().ThrowAsync<NotFoundException>();
       }

    [Fact]
    public async Task GetGameResult_Should_Throw_If_Game_Is_Not_Complete()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        players.First().Gesture = null;
        var round = Builder<Round>.CreateNew().With(r=>r.State = Core.Enums.SystemState.Started).With(r => r.Players = players).Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        Func<Task> callUpdateAsync = async () => await _gamePlayService.GetGameResult(game.Id);

        //Assert

        await callUpdateAsync.Should().ThrowAsync<GameIsNotCompleteException>();
    }

    [Fact]
    public async Task GetGameResult_Should_Return_Winner()
    {
        //Arrange
        var winner = Builder<Player>.CreateNew().With(p => p.Nickname = "winner").With(p => p.Gesture = Gesture.Paper).Build();
        var loser = Builder<Player>.CreateNew().With(p => p.Nickname = "loser").With(p => p.Gesture = Gesture.Rock).Build();

        var round1 = Builder<Round>.CreateNew()
            .With(r => r.State = Core.Enums.SystemState.Completed)
            .With(r => r.Players = new List<Player> { winner, loser })
            .Build();
        var round2 = Builder<Round>.CreateNew()
                                   .With(r => r.State = Core.Enums.SystemState.Completed)
                                   .With(r => r.Players = new List<Player> { winner, loser })
                                   .Build();
        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round1, round2 })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.GetGameResult(game.Id);

        //Assert

        result.WinnerNickname.Should().Be(winner.Nickname);
    }

    [Fact]
    public async Task GetGameResult_Should_Return_Nothing_noone_is_winner()
    {
        //Arrange
        var player1 = Builder<Player>.CreateNew().With(p => p.Nickname = "player1").With(p => p.Gesture = Gesture.Paper).Build();
        var player2 = Builder<Player>.CreateNew().With(p => p.Nickname = "player2").With(p => p.Gesture = Gesture.Paper).Build();

        var round = Builder<Round>.CreateNew()
            .With(r => r.State = Core.Enums.SystemState.Completed)
            .With(r => r.Players = new List<Player> { player1, player2 })
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.GetGameResult(game.Id);

        //Assert

        result.WinnerNickname.Should().BeEmpty();
    }

    [Fact]
    public async Task GetRoundResult_Should_Return_Winners_And_Losers()
    {
        //Arrange
        var winner = Builder<Player>.CreateNew().With(p => p.Nickname = "winner").With(p => p.Gesture = Gesture.Paper).Build();
        var loser = Builder<Player>.CreateNew().With(p => p.Nickname = "loser").With(p => p.Gesture = Gesture.Rock).Build();

        var round = Builder<Round>.CreateNew()
            .With(r => r.State = SystemState.Completed)
            .With(r => r.Players = new List<Player> { winner, loser })
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.GetRoundResult(game.Id, 1);

        //Assert

        result.WinnerNickname.Should().Be(winner.Nickname);
        result.WinnerGesture.Should().Be(winner.Gesture);
        result.LoserNickname.Should().Be(loser.Nickname);
        result.LoserGesture.Should().Be(loser.Gesture);
        result.isDraw.Should().BeFalse();
    }

    [Fact]
    public async Task GetRoundResult_Should_Return_Draw_No_Winners()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        players.ForEach(p => p.Gesture = Gesture.Rock);

        var round = Builder<Round>.CreateNew()
            .With(r => r.State = SystemState.Completed)
            .With(r => r.Players = players)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.GetRoundResult(game.Id, 1);

        //Assert

        result.WinnerNickname.Should().BeNull();
        result.WinnerGesture.Should().BeNull();
        result.LoserNickname.Should().BeNull();
        result.LoserGesture.Should().BeNull();
        result.isDraw.Should().BeTrue();
    }
    [Fact]
    public async Task GetRoundResult_Should_Throw_If_Not_All_Players_Made_Gesture()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();
        players.ForEach(p => p.Gesture = null);

        var round = Builder<Round>.CreateNew()
            .With(r => r.State = SystemState.Completed)
            .With(r => r.Players = players)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        Func<Task> callUpdateAsync = async () => await _gamePlayService.GetRoundResult(game.Id, 1);

        //Assert

        await callUpdateAsync.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task GetRoundResult_Should_Return_If_Last_Round()
    {
        //Arrange
        var players = Builder<Player>.CreateListOfSize(2).Build().ToList();

        var round = Builder<Round>.CreateNew()
            .With(r => r.State = SystemState.Completed)
            .With(r => r.Players = players)
            .Build();

        var game = Builder<Game>.CreateNew()
         .With(tl => tl.Rounds = new List<Round> { round })
         .Build();
        _gameRepository.GetFirstAsync(Arg.Any<Expression<Func<Game, bool>>>()).Returns(game);
        _gameRepository.UpdateAsync(Arg.Any<Game>()).Returns(game);

        //Act
        var result = await _gamePlayService.GetRoundResult(game.Id, 1);

        //Assert

        result.isLastRound.Should().BeTrue();
    }
}
