using Microsoft.AspNetCore.Mvc;
using RPS.Application.Models;
using RPS.Application.Models.Game;
using RPS.Application.Services;

namespace RPS.API.Controllers;

public class GamesController : ApiController
{
    private readonly IGamePlayService _gamePlayService;
    private readonly IPlayersService _playersService;


    public GamesController(IGamePlayService gamePlayService, IPlayersService playersService)
    {
        _gamePlayService = gamePlayService;
        _playersService = playersService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateGameModel createTodoListModel)
    {
        return Ok(ApiResult<CreateGameResponseModel>.Success(
            await _gamePlayService.CreateGameAsync(createTodoListModel)));
    }

    [HttpPost("{id}/gesture")]
    public async Task<IActionResult> PlaceGestureAsync(int id, PlaceRoundGestureModel command)
    {
        return Ok(ApiResult<PlaceRoundGestureResponseModel>.Success(
            await _gamePlayService.PlaceRoundGesture(id, command)));
    }

    [HttpPut("{id}/join")]
    public async Task<IActionResult> JoinRoundAsync(int id, JoinRoundModel joinRoundModel)
    {
        return Ok(ApiResult<JoinRoundResponseModel>.Success(
       await _playersService.JoinCurrentRound(id, joinRoundModel)));
    }

    [HttpPut("{id}/leave")]
    public async Task<IActionResult> LeaveCurrentRoundAsync(int id, LeaveCurrentRoundModel command)
    {
        return Ok(ApiResult<LeaveCurrentRoundResponseModel>.Success(
       await _playersService.LeaveCurrentRound(id, command)));
    }

    [HttpGet("{id}/players")]
    public async Task<IActionResult> GetCurrentPlayers(int id)
    {
        return Ok(ApiResult<GamePlayersResponseModel>.Success(
       await _playersService.GetCurrentGamePlayers(id)));
    }

    [HttpGet("{id}/result/{roundNumber}")]
    public async Task<IActionResult> GetRoundResult(int id, int roundNumber)
    {
        return Ok(ApiResult<RoundResultModelResponseModel>.Success(
       await _gamePlayService.GetRoundResult(id, roundNumber)));
    }

    [HttpGet("{id}/result")]
    public async Task<IActionResult> GetGameResult(int id)
    {
        return Ok(ApiResult<GameResultResponseModel>.Success(
       await _gamePlayService.GetGameResult(id)));
    }
}
