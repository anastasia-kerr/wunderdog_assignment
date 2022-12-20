using FluentValidation;

namespace RPS.Application.Models.Game
{
    public class CreateGameModelValidator : AbstractValidator<CreateGameModel>
    {
        public CreateGameModelValidator()
        {
            RuleFor(cti => cti.NumberOfRounds)
                .GreaterThan(0)
                .WithMessage("Must have at least one round");
        }
    }

}
