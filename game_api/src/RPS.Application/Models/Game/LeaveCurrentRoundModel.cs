using PRS.Application.Models;

namespace RPS.Application.Models.Game
{
    public class LeaveCurrentRoundModel
    {
        public string PlayerIdentifier { get; set; }
    }

    public class LeaveCurrentRoundResponseModel : BaseResponseModel {
        public int RoundNumber { get; set; }
    }
}
