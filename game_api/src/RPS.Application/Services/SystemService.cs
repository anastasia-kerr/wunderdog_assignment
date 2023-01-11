using RPS.Application.Models.System;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Interfaces;
using RPS.Core.Rules;
using System.Threading.Tasks;

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
        
        var validators = new List<ISystemStateValidator> { new RedStateValidator(), new AmberStateValidator(), new GreenStateValidator()       };

        var state = validators.FirstOrDefault(validator => validator.IsInState(allTasks));
        var stateString = Enum.GetName(typeof(SystemState), state != null ? state.State : SystemState.Unknown);
        return new SystemStatusResponseModel() { State = stateString };
    }

    public async Task<GetTasksResponseModel> GetTasks()
    {
        var allTasks = await _taskRepository.GetAllAsync(x => x == x);
        return new GetTasksResponseModel() { Tasks = allTasks.Select(task=> new SystemTaskModel() { 
         Id= task.Id,
         Title= task.Title,
         Importance = Enum.GetName(typeof(Importance), task.Importance),
          IsOff = task.IsOff,
          LastStopped = task.LastStopped
        }).OrderBy(x=>x.Title).ToList()};
    }

}
