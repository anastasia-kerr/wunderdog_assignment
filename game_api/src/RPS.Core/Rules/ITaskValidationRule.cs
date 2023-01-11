using RPS.Core.Entities;

namespace RPS.Core.Rules
{
    public interface ITaskValidationRule
    {
        bool IsValid(SystemTask task);

    }
}
