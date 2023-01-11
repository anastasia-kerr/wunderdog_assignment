using RPS.Application.Models.System;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Interfaces;
using RPS.Core.Rules;

namespace RPS.Application.Services;

public class SystemService : ISystemService
{
    private readonly ITaskRepository _taskRepository;

    public SystemService(ITaskRepository gameRepository)
    {
        _taskRepository = gameRepository;
    }

    public async Task<SystemStatusResponseModel> GetStatus()
    {
        var allTasks = await _taskRepository.GetAllAsync(x => x == x);
        var redValidator = new RedStateValidator() { };
        if (redValidator.IsInState(allTasks))
        {
            return new SystemStatusResponseModel() { State = SystemState.Red };
        }
        var amberValidator = new AmberStateValidator() { };
        if (amberValidator.IsInState(allTasks))        {
            return new SystemStatusResponseModel() { State = SystemState.Amber };
        }
        var greenValidator = new GreenStateValidator() { };
        if (greenValidator.IsInState(allTasks))
        {
            return new SystemStatusResponseModel() { State = SystemState.Green };
        }

        var validators = new List<ISystemStateValidator> { new RedStateValidator(), new AmberStateValidator(), new GreenStateValidator()       };

        var state = validators.FirstOrDefault(validator => validator.IsInState(allTasks));

        return new SystemStatusResponseModel() { State = state!=null? state.State : SystemState.Unknown };
    }

    public async Task<GetTasksResponseModel> GetTasks()
    {
        return new GetTasksResponseModel() { Tasks = await _taskRepository.GetAllAsync(x => x == x) };
    }

}
