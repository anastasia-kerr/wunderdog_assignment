using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.Application.Services;

public class RedStateValidator : ISystemStateValidator
{
    public SystemState State => SystemState.Red;

    public bool IsInState(IList<SystemTask> tasks)
    {
        var crititcalRule = new TaskInWorkingStateRule(Importance.Critical);
        if (tasks.Any(t => !crititcalRule.IsValid(t)))
        {
            return true;
        }

        var essentialRule = new TaskIsInWorkingStateForDurationRule(Importance.Essential, 30);

        if (tasks.Count(t => !essentialRule.IsValid(t)) >= 2)
        {
            return true;
        }
        var importantRule = new TaskIsInWorkingStateForDurationRule(Importance.Important, 60);

        if (tasks.Count(t => !importantRule.IsValid(t)) >= 3)
        {
            return true;
        }


        return false;
    }
}
