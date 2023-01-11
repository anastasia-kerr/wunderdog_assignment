using RPS.Core.Entities;

namespace RPS.Application.Models.System
{
    public class GetTasksResponseModel
    {
        public IList<SystemTask> Tasks { get; set; }
    }
}
