using FluentAssertions;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.UnitTests.Entities
{
    public class TaskHasNotStoppedForDurationRuleTest
    {
        [Theory]
        [InlineData(Importance.Important, 30)]
        [InlineData(Importance.Essential, 60)]
        [InlineData(Importance.Critical, 60)]
        public void TaskHasNotStoppedForDurationRule_Should_Return_True(Importance importance, int duration)
        {
            var lastUpdatedDate = DateTime.Now.AddSeconds(-duration);
            var task = new SystemTask() { Importance = importance, IsOff = false, LastStopped = lastUpdatedDate };

            var rule = new TaskHasNotStoppedForDurationRule(importance, duration);

            rule.IsValid(task).Should().BeTrue();
        }

        [Theory]
        [InlineData(Importance.Important, 30)]
        [InlineData(Importance.Essential, 60)]
        [InlineData(Importance.Critical, 60)]
        public void TaskHasNotStoppedForDurationRule_Should_Return_False(Importance importance, int duration)
        {
            var lastUpdatedDate = DateTime.Now.AddSeconds(-duration);
            var task = new SystemTask() { Importance = importance, IsOff = true, LastStopped = lastUpdatedDate };

            var rule = new TaskHasNotStoppedForDurationRule(importance, duration);

            rule.IsValid(task).Should().BeFalse();
        }

        [Theory]
        [InlineData(Importance.Important, 30)]
        [InlineData(Importance.Essential, 60)]
        [InlineData(Importance.Critical, 60)]
        public void TaskHasNotStoppedForDurationRule_Stopped_In_The_Last_Period_Should_Return_False(Importance importance, int duration)
        {
            var lastUpdatedDate = DateTime.Now.AddSeconds(-(duration-1));
            var task = new SystemTask() { Importance = importance, IsOff = false, LastStopped = lastUpdatedDate };

            var rule = new TaskHasNotStoppedForDurationRule(importance, duration);

            rule.IsValid(task).Should().BeFalse();
        }
    }
}
