using PRS.Application.Models;

namespace RPS.Application.Models.Game
{
    public class CreateGameModel
    {
        public string Nickname { get; set; }
        public int NumberOfRounds { get; set; }
    }

    public class CreateGameResponseModel : BaseResponseModel {
    }
}
