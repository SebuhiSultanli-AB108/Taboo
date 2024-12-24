﻿using Taboo.Enums;

namespace Taboo.DTOs.Game;

public class GameCreateDTO
{
    public GameLevels GameLevel { get; set; }
    public int FailCount { get; set; }
    public int SkipCount { get; set; }
    public int Seconds { get; set; }
    public string LanguageCode { get; set; }
}
