using RPS.Application.Models.Game;
namespace RPS.Application.Services;
public interface IPlayersService
{
    Task<JoinRoundResponseModel> JoinCurrentRound(int gameId, JoinRoundModel command);
    Task<LeaveCurrentRoundResponseModel> LeaveCurrentRound(int gameId, LeaveCurrentRoundModel command);
    Task<GamePlayersResponseModel> GetCurrentGamePlayers(int gameId);

}
