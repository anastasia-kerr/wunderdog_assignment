using Microsoft.AspNetCore.Mvc;
using RPS.Application.Models;
using RPS.Application.Models.System;
using RPS.Application.Models.Task;
using RPS.Application.Services;

namespace RPS.API.Controllers;

public class SystemController : ApiController
{
    private readonly ITaskService _taskService;
    private readonly ISystemService _systemService;


    public SystemController(ITaskService taskService, ISystemService systemService)
    {
        _taskService = taskService;
        _systemService = systemService;
    }

    [HttpPut("task/{id}")]
    public async Task<IActionResult> SetTaskAsync(Guid id, bool isOff)
    {
        return Ok(ApiResult<SetTaskResponseModel>.Success(
       await _taskService.SetTask(new SetTaskModel() { IsOff = isOff, Id = id })));
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        return Ok(ApiResult<GetTasksResponseModel>.Success(
       await _systemService.GetTasks()));
    }
    [HttpGet]
    public async Task<IActionResult> GetCurrentState()
    {
        return Ok(ApiResult<SystemStatusResponseModel>.Success(
       await _systemService.GetStatus()));
    }

}
