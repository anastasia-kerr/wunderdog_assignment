using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.Core.Rules
{
    public class TaskInWorkingStateRule : ITaskValidationRule
    {
        private Importance _taskImportance;

        public TaskInWorkingStateRule(Importance taskImportance)
        {
            _taskImportance = taskImportance;
        }

        public bool IsValid(SystemTask task)
        {
            if (task.Importance != _taskImportance) return true;

            return task.IsOff == false;
        }
    }
}
