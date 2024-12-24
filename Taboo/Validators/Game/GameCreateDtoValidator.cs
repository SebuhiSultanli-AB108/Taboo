using FluentValidation;
using Taboo.DTOs.Game;

namespace Taboo.Validators.Game;

public class GameCreateDtoValidator : AbstractValidator<GameCreateDTO>
{
    string maxLenErrMsg(int num) => $"Length of the property must be {num} or less!";
    string maxCountErrMsg(int num) => $"Count of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";

    public GameCreateDtoValidator()
    {
        RuleFor(x => x.LanguageCode)
            .NotNull().NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(2).WithMessage(maxLenErrMsg(2));
    }
}
