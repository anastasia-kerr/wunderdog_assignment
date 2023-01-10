using AutoMapper;
using RPS.Application.Exceptions;
using RPS.Application.Models.Game;
using RPS.Application.Models.Task;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Exceptions;
using RPS.Core.Interfaces;
using RPS.Core.Services;

namespace RPS.Application.Services;

public class TaskService : ITaskService
{
    private readonly IMapper _mapper;
    private readonly IGameRepository _taskRepository;

    public TaskService(IGameRepository gameRepository, IMapper mapper)
    {
        _taskRepository = gameRepository;
        _mapper = mapper;
    }
    public async Task<SetTaskResponseModel> SetTask(SetTaskModel command)
    {
        var task = await _taskRepository.GetFirstAsync(g => g.Id == command.TaskId);

        await _taskRepository.UpdateAsync(task);

        return new SetTaskResponseModel
        {
            TaskId = task.Id,
            IsOff = command.IsOff
        };
    }
}
