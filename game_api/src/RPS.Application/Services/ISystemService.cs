using RPS.Application.Models.System;

namespace RPS.Application.Services;
public interface ISystemService
{
    Task<SystemStatusResponseModel> GetStatus();
}
