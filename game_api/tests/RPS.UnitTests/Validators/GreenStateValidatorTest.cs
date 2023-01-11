using FluentAssertions;
using RPS.Application.Services;
using RPS.Application.Validators;
using RPS.Core.Entities;
using RPS.Core.Enums;
namespace RPS.UnitTests.Validators
{
    public class GreenStateValidatorTest
    {
        SystemTask workingTask = new SystemTask()
        {
            Importance = Importance.Important
        };
        ISystemStateValidator validator = new GreenStateValidator();

        [Fact]
        public Task GreenStateValidator_importantTaskNotInAWorkingStateFor30Seconds_Should_Return_True()
        {
            var workingInTheLast10Minutes = new SystemTask()
            {
                Importance = Importance.Important,
                IsOff = false,
                LastStopped = DateTime.UtcNow.AddMinutes(-10),
            };
            var tasks = new List<SystemTask>() { workingTask, workingTask, workingTask, workingInTheLast10Minutes, workingInTheLast10Minutes };

            validator.IsInState(tasks).Should().BeTrue();
            return Task.CompletedTask;
        }
    }
}
