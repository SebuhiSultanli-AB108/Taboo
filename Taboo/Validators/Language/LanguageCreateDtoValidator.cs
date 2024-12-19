using FluentValidation;
using Taboo.DTOs.Language;

namespace Taboo.Validators.Language;

public class LanguageCreateDtoValidator : AbstractValidator<LanguageCreateDTO>
{
    string maxLenErrMsg(int num) => $"Length of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";
    public LanguageCreateDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(2).WithMessage(maxLenErrMsg(2));
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(32).WithMessage(maxLenErrMsg(32));
        RuleFor(x => x.IconUrl)
            .NotNull()
            .NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(256).WithMessage(maxLenErrMsg(256))
            .Matches("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$");
    }
}
