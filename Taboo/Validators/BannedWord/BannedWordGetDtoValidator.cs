using FluentValidation;
using Taboo.DTOs.BannedWord;

namespace Taboo.Validators.BannedWord;

public class BannedWordGetDtoValidator : AbstractValidator<BannedWordGetDTO>
{
    string maxCountErrMsg(int num) => $"Count of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";

    public BannedWordGetDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotNull().WithMessage(nullErrMsg)
            .MaximumLength(32).WithMessage(maxCountErrMsg(32));

    }
}
