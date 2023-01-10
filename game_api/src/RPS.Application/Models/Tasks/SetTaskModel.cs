using PRS.Application.Models;

namespace RPS.Application.Models.Task
{
    public class SetTaskModel
    {
        public bool IsOff { get; set; }
        public int TaskId { get; set; }
    }

    public class SetTaskResponseModel : BaseResponseModel {
        public bool IsOff { get; set; }
    }
}
