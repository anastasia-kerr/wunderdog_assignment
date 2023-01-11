using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.Application.Services;

public class GreenStateValidator : ISystemStateValidator
{
    public SystemState State => SystemState.Green;

    public bool IsInState(IList<SystemTask> tasks)
    {
        var rules = new List<ITaskValidationRule> {
            new TaskHasNotStoppedForDurationRule(Importance.Important, 60),
             new TaskHasNotStoppedForDurationRule(Importance.Essential, 60),
          new TaskHasNotStoppedForDurationRule(Importance.Critical, 60)
        };

        if (rules.All(rule => tasks.All(t => rule.IsValid(t))))
        {
            return true;
        }
        return false;
    }
}
