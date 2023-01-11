using RPS.Application.Models.Task;
using RPS.Core.Interfaces;

namespace RPS.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository gameRepository)
    {
        _taskRepository = gameRepository;
    }
    public async Task<SetTaskResponseModel> SetTask(SetTaskModel command)
    {
        var task = await _taskRepository.GetFirstAsync(g => g.Id == command.Id);
        task.IsOff = command.IsOff;

        if (command.IsOff)
        {
            task.LastStopped = DateTime.Now;
        }

        await _taskRepository.UpdateAsync(task);

        return new SetTaskResponseModel
        {
            IsOff = command.IsOff
        };
    }
}
