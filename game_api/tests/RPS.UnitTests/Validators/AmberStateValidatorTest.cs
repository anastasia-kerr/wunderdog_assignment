using FluentAssertions;
using RPS.Application.Services;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
namespace RPS.UnitTests.Validators
{
    public class AmberStateValidatorTest
    {
        SystemTask workingTask = new SystemTask()
        {
            Importance = Importance.Important
        };
        ISystemStateValidator validator = new AmberStateValidator();

        [Fact]
        public Task AmberStateValidatorTest_importantTaskNotInAWorkingStateFor30Seconds_Should_Return_True()
        {
            var importantTaskNotInAWorkingStateFor30Seconds = new SystemTask()
            {
                Importance = Importance.Important,
                IsOff = true,
                LastStopped = DateTime.UtcNow
            };
            var tasks = new List<SystemTask>() { workingTask, workingTask, workingTask, importantTaskNotInAWorkingStateFor30Seconds, importantTaskNotInAWorkingStateFor30Seconds };

            validator.IsInState(tasks).Should().BeTrue();
            return Task.CompletedTask;
        }

        [Fact]
        public Task AmberStateValidatorTest_essentialTaskNotWorking_Should_Return_True()
        {
            var essentialTask = new SystemTask()
            {
                Importance = Importance.Essential,
                IsOff = true,
                LastStopped = DateTime.UtcNow
            };
            var tasks = new List<SystemTask>() { workingTask, essentialTask };

            validator.IsInState(tasks).Should().BeTrue();
            return Task.CompletedTask;
        }

        [Fact]
        public Task AmberStateValidatorTest_allWorking_Should_Return_False()
        {
            var tasks = new List<SystemTask>() { workingTask, workingTask, workingTask, workingTask };

            validator.IsInState(tasks).Should().BeFalse();
            return Task.CompletedTask;
        }
    }
}
