using PRS.Application.Models;

namespace RPS.Application.Models.Game
{
    public class GameResultResponseModel : BaseResponseModel {
        public string WinnerNickname { get; set; }
        public bool IsDraw { get; set; }
    }
}
