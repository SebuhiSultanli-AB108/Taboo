using FluentValidation;
using Taboo.DTOs.Word;
using Taboo.Enums;

namespace Taboo.Validators.Word;

public class WordCreateDtoValidator : AbstractValidator<WordCreateDTO>
{
    string maxLenErrMsg(int num) => $"Length of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";

    public WordCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotNull().WithMessage(nullErrMsg)
            .MaximumLength(32).WithMessage(maxLenErrMsg(32));
        RuleFor(x => x.LanguageCode)
            .NotNull().NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(2).WithMessage(maxLenErrMsg(2));
        RuleFor(x => x.BannedWords)
            .NotNull().WithMessage(nullErrMsg)
            .Must(x => x.Count == (int)GameLevel.Hard).WithMessage($"{(int)GameLevel.Hard} Unique BannedWords needed!");
        RuleForEach(x => x.BannedWords)
            .NotNull().WithMessage(nullErrMsg)
            .MaximumLength(32).WithMessage(maxLenErrMsg(32));
    }
}