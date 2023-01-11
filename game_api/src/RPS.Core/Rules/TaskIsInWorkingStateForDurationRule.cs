using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.Core.Rules
{
    public class TaskIsInWorkingStateForDurationRule : ITaskValidationRule
    {
        private int _seconds;
        private Importance _taskImportance;

        public TaskIsInWorkingStateForDurationRule(Importance taskImportance, int seconds)
        {
            _seconds = seconds;
            _taskImportance = taskImportance;
        }
        public bool IsValid(SystemTask task)
        {
            if (task.Importance != _taskImportance) return true;
            if (task.LastStopped == null) return true;
            if (task.IsOff == false) return true;

            var nowDate = DateTime.Now;
            var lastValidDate = nowDate.AddSeconds(-_seconds);

            return DateTime.Compare((DateTime)task.LastStopped, lastValidDate) < 0;
        }
    }
}
