using FluentAssertions;
using RPS.Application.Services;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
namespace RPS.UnitTests.Validators
{
    public class RedStateValidatorTest
    {
        SystemTask workingTask = new SystemTask()
        {
            Importance = Importance.Important
        };

        ISystemStateValidator validator = new RedStateValidator();

        [Fact]
        public void RedStateValidatorTest_crititalNotWorkingState_Should_Return_True()
        {
            var critial = new SystemTask()
            {
                Importance = Importance.Critical,
                IsOff = true,
                LastStopped = DateTime.UtcNow
            };
            var tasks = new List<SystemTask>() { workingTask, workingTask, workingTask, critial, critial };

            validator.IsInState(tasks).Should().BeTrue();

        }

        [Fact]
        public void RedStateValidator_2essentialTaskNotWorking_Should_Return_True()
        {
            var essentialTask = new SystemTask()
            {
                Importance = Importance.Essential,
                IsOff = true,
                LastStopped = DateTime.UtcNow
            };
            var tasks = new List<SystemTask>() { workingTask, essentialTask, essentialTask };

            validator.IsInState(tasks).Should().BeTrue();

        }

        [Fact]
        public void RedStateValidator_allTasksAreWorking_Should_Return_False()
        {
            var tasks = new List<SystemTask>() { workingTask, workingTask, workingTask };

            validator.IsInState(tasks).Should().BeFalse();

        }
    }
}
