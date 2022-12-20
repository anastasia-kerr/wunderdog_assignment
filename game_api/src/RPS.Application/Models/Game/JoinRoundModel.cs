using PRS.Application.Models;

namespace RPS.Application.Models.Game
{
    public class JoinRoundModel
    {
        public string PlayerNickname { get; set; }
        public string PlayerIdentifier { get; set; }

    }

    public class JoinRoundResponseModel : BaseResponseModel {
        public int RoundNumber { get; set; }
    }
}
