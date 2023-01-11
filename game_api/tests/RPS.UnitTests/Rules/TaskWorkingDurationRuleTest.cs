using FluentAssertions;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.UnitTests.Entities
{
    public class TaskWorkingDurationRuleTest
    {
        [Theory]
        [InlineData(Importance.Important, 30)]
        [InlineData(Importance.Essential, 60)]
        [InlineData(Importance.Critical, 60)]

        public void TaskIsInWorkingStateForDurationRule_Should_Return_True(Importance importance, int duration)
        {
            var lastUpdatedDate = DateTime.Now.AddSeconds(-duration);
            var task = new SystemTask() { Importance = importance, IsOff = false, LastStopped = lastUpdatedDate };

            var rule = new TaskIsInWorkingStateForDurationRule(importance, duration);

            rule.IsValid(task).Should().BeTrue();
        }
    }
}
