using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.Application.Validators
{
    public interface ISystemStateValidator
    {
        SystemState State { get;}
        bool IsInState(IList<SystemTask> tasks);
    }
}
