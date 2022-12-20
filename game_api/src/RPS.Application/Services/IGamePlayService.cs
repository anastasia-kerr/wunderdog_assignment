using RPS.Application.Models.Game;
namespace RPS.Application.Services;
public interface IGamePlayService
{
    Task<CreateGameResponseModel> CreateGameAsync(CreateGameModel command);
    Task<PlaceRoundGestureResponseModel> PlaceRoundGesture(int id,  PlaceRoundGestureModel command);
    Task<RoundResultModelResponseModel> GetRoundResult(int gameId, int roundNumber);
    Task<GameResultResponseModel> GetGameResult(int gameId);

}
