using Taboo.Enums;

namespace Taboo.DTOs.Game;

public class GameCreateDTO
{
    public GameLevel GameLevel { get; set; }
    public int BannedWordCount { get; set; }
    public int FailCount { get; set; }
    public int Seconds { get; set; }
    public string LanguageCode { get; set; }
}
