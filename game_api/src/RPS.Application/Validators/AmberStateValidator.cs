using RPS.Application.Models.System;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.Application.Services;

public class AmberStateValidator : ISystemStateValidator
{
    public SystemState State => SystemState.Amber;

    public bool IsInState(IList<SystemTask> tasks)
    {
        var rule = new TaskIsInWorkingStateForDurationRule(Importance.Important, 30);

        if (tasks.Any(t => !rule.IsValid(t)))
        {
            return true;
        }

        var importantRule = new TaskInWorkingStateRule(Importance.Important);

        if (tasks.Count(t => !importantRule.IsValid(t)) >= 2)
        {
            return true;
        }

        var essentialRule = new TaskInWorkingStateRule(Importance.Essential);

        if (tasks.Any(t => !essentialRule.IsValid(t)))
        {
            return true;
        }

        return false;
    }
}
