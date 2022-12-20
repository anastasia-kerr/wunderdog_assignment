using PRS.Application.Models;
using RPS.Core.Enums;

namespace RPS.Application.Models.Game
{
    public class PlaceRoundGestureModel
    {
        public string PlayerNickname { get; set; }
        public string PlayerIdentifier { get; set; }

        public Gesture RoundGesture { get; set; }
    }

    public class PlaceRoundGestureResponseModel : BaseResponseModel {
    }
}
