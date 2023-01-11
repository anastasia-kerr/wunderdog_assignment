using FluentAssertions;
using RPS.Core.Entities;
using RPS.Core.Enums;
using RPS.Core.Rules;

namespace RPS.UnitTests.Entities
{
    public class TaskInWorkingStateRuleTest
    {
        [Theory]
        [InlineData(Importance.Important)]
        [InlineData(Importance.Essential)]
        [InlineData(Importance.Critical)]

        public void TaskInWorkingStateRule_Should_Return_True(Importance importance)
        {

            var rule = new TaskInWorkingStateRule(importance);

            rule.IsValid(new SystemTask()).Should().BeTrue();
        }

        [Theory]
        [InlineData(Importance.Important)]
        [InlineData(Importance.Essential)]
        [InlineData(Importance.Critical)]
        public void TaskInWorkingStateRule_Should_Return_False(Importance importance)
        {

            var rule = new TaskInWorkingStateRule(importance);

            rule.IsValid(new SystemTask() { Importance = importance, IsOff = true }).Should().BeFalse();
        }
    }
}
