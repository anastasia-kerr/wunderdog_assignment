using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RPS.Application.Models.Game;
using RPS.Core.Enums;
using RPS.DataAccess.Persistence;
using RPS.IntegrationTests.Config;
using RPS.IntegrationTests.Helpers;
using RSP.IntegrationTests.Helpers;
using System.Net;

namespace RPS.IntegrationTests
{
    [TestFixture]
    public class GamesEndpointTests: BaseOneTimeSetup
    {
        string player1Nickname = "player1Nickname";
        string player1Identifier = "player1Identifier";
        string player2Nickname = "player2Nickname";
        string player2Identifier = "player2Identifier";
        int numberOfRounds = 2;
        int gameId = 1;
        [Test, Order(1)]
        public async Task Create_Should_Add_Game_In_Database()
        {
            // Arrange
            var context = Host.Services.GetRequiredService<DatabaseContext>();

            var createGame = Builder<SetLightModel>.CreateNew().With(g=>g.NumberOfRounds = numberOfRounds).Build();

            // Act
            var apiResponse = await Client.PostAsync("/api/games", new JsonContent(createGame));

            // Assert
            var response = await ResponseHelper.GetApiResultAsync<GetLightResponseModel>(apiResponse);
            gameId = response.Result.Id;

            var game = await context.Games.Where(u => u.Id == response.Result.Id).FirstOrDefaultAsync();
            CheckResponse.Succeeded(response);
            game.Should().NotBeNull();
            game.CreatedBy.Should().Be(createGame.Nickname);
        }

        [Test, Order(2)]
        public async Task Create_Should_Return_BadRequest_If_RoundNumber_Is_Not_Provided()
        {
            // Arrange
            var context = Host.Services.GetRequiredService<DatabaseContext>();

            var createGame = Builder<SetLightModel>.CreateNew().With(g=>g.NumberOfRounds = 0).Build();

            // Act
            var apiResponse = await Client.PostAsync("/api/games", new JsonContent(createGame));

            // Assert
            var response = await ResponseHelper.GetApiResultAsync<GetLightResponseModel>(apiResponse);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            CheckResponse.Failure(response);
        }


        [Test, Order(3)]
        public async Task Join_Should_Add_Player_And_Return_CurrentRound()
        {
            var apiResponse = await JoinCurrentRoundAsync(player1Nickname, player1Identifier);
            
            var response = await ResponseHelper.GetApiResultAsync<JoinRoundResponseModel>(apiResponse);
            response.Result.RoundNumber.Should().NotBe(0);

            CheckResponse.Succeeded(response);
        }

        [Test, Order(4)]
        public async Task Gesture_Should_Return_Success_If_Player_Joined_Game_and_CurrentRound_Is_Not_Null()
        {
            var apiResponse = await PlaceRoundGestureAsync(player1Nickname, player1Identifier, Gesture.Rock);

            var response = await ResponseHelper.GetApiResultAsync<PlaceRoundGestureResponseModel>(apiResponse);

            CheckResponse.Succeeded(response);
        }

        [Test, Order(5)]
        public async Task Gesture_Should_Return_BadRequest_If_Player_Not_In_Game()
        {
            var apiResponse = await PlaceRoundGestureAsync(player2Nickname, player2Identifier, Gesture.Rock);

            var response = await ResponseHelper.GetApiResultAsync<PlaceRoundGestureResponseModel>(apiResponse);

            CheckResponse.Failure(response);
        }

        [Test, Order(6)]
        public async Task GetRoundResult_Should_Return_BadRequest_If_Not_All_Players_In_Game()
        {
            var apiResponse = await Client.GetAsync($"/api/games/{gameId}/result/1");

            var response = await ResponseHelper.GetApiResultAsync<RoundResultModelResponseModel>(apiResponse);

            CheckResponse.Failure(response);
        }

        [Test, Order(7)]
        public async Task GetRoundResult_Should_Return_Draw_Result()
        {
            await JoinCurrentRoundAsync(player2Nickname, player2Identifier);
            await PlaceRoundGestureAsync(player2Nickname, player2Identifier, Gesture.Rock);

            var apiResponse = await Client.GetAsync($"/api/games/{gameId}/result/1");

            var response = await ResponseHelper.GetApiResultAsync<RoundResultModelResponseModel>(apiResponse);

            CheckResponse.Succeeded(response);
            response.Result.isDraw.Should().BeTrue();
        }

        [Test, Order(8)]
        public async Task GetCurrentPlayers_Should_Return_List_Of_Two_Names()
        {
            await JoinCurrentRoundAsync(player1Nickname, player1Identifier);
            await JoinCurrentRoundAsync(player2Nickname, player2Identifier);

            var apiResponse = await Client.GetAsync($"/api/games/{gameId}/players");

            var response = await ResponseHelper.GetApiResultAsync<GamePlayersResponseModel>(apiResponse);

            CheckResponse.Succeeded(response);
            response.Result.Players.Should().HaveCount(2);
        }

        [Test, Order(9)]
        public async Task Game_Should_Return_Winner_Name()
        {
            await PlaceRoundGestureAsync(player1Nickname, player1Identifier, Gesture.Rock);
            await PlaceRoundGestureAsync(player2Nickname, player2Identifier, Gesture.Scissors);

            var apiResponse = await Client.GetAsync($"/api/games/{gameId}/result");

            var response = await ResponseHelper.GetApiResultAsync<GameResultResponseModel>(apiResponse);

            CheckResponse.Succeeded(response);
            response.Result.WinnerNickname.Should().Be(player1Nickname);
        }


        private async Task<HttpResponseMessage> JoinCurrentRoundAsync(string nickname, string ientifier) {
            // Arrange
            var command = Builder<JoinRoundModel>.CreateNew()
                .With(p => p.PlayerNickname = nickname)
                .With(p => p.PlayerIdentifier = ientifier)
                .Build();

            // Act
            return await Client.PutAsync($"/api/games/{gameId}/join", new JsonContent(command));
        }

        private async Task<HttpResponseMessage> PlaceRoundGestureAsync(string nickname, string identifier, Gesture gestutre)
        {
            var command = Builder<PlaceRoundGestureModel>.CreateNew()
              .With(p => p.PlayerNickname = nickname)
              .With(p => p.PlayerIdentifier = identifier)
              .With(p => p.RoundGesture = gestutre)
              .Build();

            return await Client.PostAsync($"/api/games/{gameId}/gesture", new JsonContent(command));

        }
    }
}
