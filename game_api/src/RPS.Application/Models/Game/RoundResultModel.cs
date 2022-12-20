using PRS.Application.Models;
using RPS.Core.Enums;

namespace RPS.Application.Models.Game
{
    public class RoundResultModelResponseModel : BaseResponseModel {
        public string? WinnerNickname { get; set; }
        public bool isDraw { set; get; }

        public bool isLastRound { set; get; }

        public Gesture? WinnerGesture { get; set; }

        public string? LoserNickname { get; set; }

        public Gesture?  LoserGesture { get; set; }
    }
}
