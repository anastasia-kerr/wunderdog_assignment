using PRS.Application.Models;

namespace RPS.Application.Models.Game
{
    public class GamePlayersResponseModel : BaseResponseModel {
        public IList<string> Players { get; set; }
    }
}
