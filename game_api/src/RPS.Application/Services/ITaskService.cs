using RPS.Application.Models.Task;

namespace RPS.Application.Services;
public interface ITaskService
{
    Task<SetTaskResponseModel> SetTask(SetTaskModel command);

}
