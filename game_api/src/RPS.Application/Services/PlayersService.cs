using RPS.Application.Exceptions;
using RPS.Application.Models.Game;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Exceptions;
using RPS.Core.Interfaces;

namespace RPS.Application.Services;

public class PlayersService : IPlayersService
{
    private readonly IGameRepository _gameRepository;

    public PlayersService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public async Task<LeaveCurrentRoundResponseModel> LeaveCurrentRound(int id, LeaveCurrentRoundModel command)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id == id);

        var response = new LeaveCurrentRoundResponseModel
        {
            Id = game.Id
        };
        if (game.IsGameComplete)
        {
            return response;

        }
        var round = game.CurrentRound;

        round.Players.RemoveAll(p => p.Identifier == command.PlayerIdentifier);
        if (round.Players.Count == 0)
        {
            round.State = RoundState.Created;
        }
        await _gameRepository.UpdateAsync(game);

        return response;
    }

    public async Task<JoinRoundResponseModel> JoinCurrentRound(int id, JoinRoundModel command)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id == id);
        if (game.IsGameComplete) { throw new GameCompleteException(id); }

        var round = game.CurrentRound;

        if (round.Players.Any(p => p.Identifier == command.PlayerIdentifier))
        {
            return new JoinRoundResponseModel
            {
                Id = game.Id,
                RoundNumber = round.RoundNumber
            };
        }

        if (round.HasMaximumAllowedPlayers)
        {
            throw new BadRequestException("Maximum number of players reached");
        }

        round.State = RoundState.Started;
        round.Players.Add(new Player() { Nickname = command.PlayerNickname, Identifier = command.PlayerIdentifier });

        await _gameRepository.UpdateAsync(game);

        return new JoinRoundResponseModel
        {
            Id = game.Id,
            RoundNumber = round.RoundNumber
        };
    }

   public async Task<GamePlayersResponseModel> GetCurrentGamePlayers(int gameId)
{
        var game = await _gameRepository.GetFirstAsync(g => g.Id == gameId);

        if (game.IsGameComplete) { throw new GameCompleteException(gameId); }

        var round = game.CurrentRound;

        return new GamePlayersResponseModel()
        {
            Id = gameId,
            Players = round.Players.Select(p => p.Nickname).ToList()
        };

    }
}
