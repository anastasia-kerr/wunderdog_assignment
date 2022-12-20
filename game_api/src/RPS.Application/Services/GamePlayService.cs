using AutoMapper;
using RPS.Application.Exceptions;
using RPS.Application.Models.Game;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Exceptions;
using RPS.Core.Interfaces;
using RPS.Core.Services;

namespace RPS.Application.Services;

public class GamePlayService : IGamePlayService
{
    private readonly IMapper _mapper;
    private readonly IGameRepository _gameRepository;

    public GamePlayService(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<CreateGameResponseModel> CreateGameAsync(CreateGameModel createGameModel)
    {
        var gameToAdd = _mapper.Map<Game>(createGameModel);

        for (var i = 1; i <= createGameModel.NumberOfRounds; i++)
        {
            gameToAdd.Rounds.Add(new Round() { RoundNumber = i });
        }
        var game = await _gameRepository.AddAsync(gameToAdd);

        return new CreateGameResponseModel
        {
            Id = game.Id
        };
    }
    public async Task<PlaceRoundGestureResponseModel> PlaceRoundGesture(int gameId, PlaceRoundGestureModel command)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id == gameId);

        if (game.IsGameComplete) { throw new GameCompleteException(gameId); }

        var round = game.CurrentRound;

        var player = round.Players.Find(g => g.Identifier == command.PlayerIdentifier);

        if (player == null)
        {
            throw new NotFoundException("There is not such a player in the game, you must join the game first");
        }

        player.Gesture = command.RoundGesture;

        if (round.AllPlayersHaveMadeGestures)
        {
            round.State = RoundState.Completed;
        }
        await _gameRepository.UpdateAsync(game);

        return new PlaceRoundGestureResponseModel
        {
            Id = game.Id
        };
    }
    public async Task<GameResultResponseModel> GetGameResult(int gameId)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id == gameId);
             
        if(!game.IsGameComplete)
        {
            throw new GameIsNotCompleteException(gameId);
        }

        var roundWinners = game.Rounds.Where(r => r.Winner != null).Select(r=>r.Winner);

        if (!roundWinners.Any())
        {
            return new GameResultResponseModel
            {
                Id = game.Id,
                WinnerNickname = string.Empty
            };
        }
        var winningStats = roundWinners.GroupBy(p => p.Nickname).Select(m => new {nickname = m.Key , winningRounds = m.Count()});
        var maxNumberOfWinningRounds = winningStats.Max(p => p.winningRounds);
        var winners = winningStats.Where(x => x.winningRounds == maxNumberOfWinningRounds);
        var isDraw = winners.Count() > 1;

        return new GameResultResponseModel
        {
            Id = game.Id,
            IsDraw = isDraw,
            WinnerNickname = isDraw? string.Empty : winners.First().nickname
        };
    }
    public async Task<RoundResultModelResponseModel> GetRoundResult(int gameId, int roundNumber)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id == gameId);

        var round = game.Rounds.First(r => r.RoundNumber == roundNumber);

        if (round == null)
        {
            throw new NotFoundException("Game does not have this number of rounds");
        }
 
        if (!round.AllPlayersHaveMadeGestures){
            throw new BadRequestException("Not All players have added their gestures");
        }

        if (round.Winner == null)
        {
            return new RoundResultModelResponseModel()
            {
                Id = game.Id,
                isLastRound = game.CurrentRound == null,
                isDraw = true
            };
        }
        var loser = round.Players.Find(p => p != round.Winner);

        return new RoundResultModelResponseModel()
        {
            Id = game.Id,
            isLastRound = game.CurrentRound == null,
            isDraw = round.Winner == null,
            LoserGesture = loser != null? loser.Gesture:null,
            LoserNickname = loser!=null? loser.Nickname:null,
            WinnerGesture = round.Winner != null? round.Winner.Gesture:null,
            WinnerNickname = round.Winner != null? round.Winner.Nickname:null
        };

    }
}
