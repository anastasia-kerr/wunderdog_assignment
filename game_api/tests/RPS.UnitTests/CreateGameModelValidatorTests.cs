using FluentValidation.TestHelper;
using RPS.Application.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.UnitTests
{

    public class CreateGameModelValidatorTests
    {
        private readonly CreateGameModelValidator _sut;

        public CreateGameModelValidatorTests()
        {
            _sut = new CreateGameModelValidator();
        }
        [Fact]
        public void Validator_Should_Have_Error_When_N_Is_Empty()
        {
            // Arrange
            var createTodoItemModel = new CreateGameModel { NumberOfRounds = 0 };

            // Act
            var result = _sut.TestValidate(createTodoItemModel);

            // Assert
            result.ShouldHaveValidationErrorFor(cti => cti.NumberOfRounds);
        }

    }
}
