﻿using FluentValidation;
using Taboo.DTOs.Game;

namespace Taboo.Validators.Game;

public class GameUpdateDtoValidator : AbstractValidator<GameUpdateDTO>
{
    string maxLenErrMsg(int num) => $"Length of the property must be {num} or less!";
    string maxCountErrMsg(int num) => $"Count of the property must be {num} or less!";
    string nullErrMsg = "Property can not be null or empty!";

    public GameUpdateDtoValidator()
    {
        RuleFor(x => x.BannedWordCount)
            .NotNull().WithMessage(nullErrMsg)
            .LessThanOrEqualTo(6).WithMessage(maxCountErrMsg(6));
        RuleFor(x => x.FailCount)
            .NotNull().WithMessage(nullErrMsg)
            .LessThanOrEqualTo(3).WithMessage(maxCountErrMsg(6));
        RuleFor(x => x.Time)
            .NotNull().WithMessage(nullErrMsg);
        //TODO: Add time cap
        RuleFor(x => x.LanguageCode)
            .NotNull().NotEmpty().WithMessage(nullErrMsg)
            .MaximumLength(2).WithMessage(maxLenErrMsg(2));
    }
}
