using FluentValidation;
using Taboo.DTOs.Word;

namespace Taboo.Validators.Word;

public class WordUpdateDtoValidator : AbstractValidator<WordUpdateDTO>
{
    string maxLenErrMsg(int num) => $"Length of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";

    public WordUpdateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotNull().WithMessage(nullErrMsg)
            .MaximumLength(32).WithMessage(maxLenErrMsg(32));
        RuleFor(x => x.LanguageCode)
            .NotNull().NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(2).WithMessage(maxLenErrMsg(2));
        RuleForEach(x => x.BannedWords)
            .NotNull().NotEmpty().WithMessage(nullErrMsg);
    }
}