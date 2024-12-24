using Taboo.DTOs.Word;
using Taboo.Enums;

namespace Taboo.DTOs.Game;

public class GameStatusDto
{
    public byte Success { get; set; }
    public byte Wrong { get; set; }
    public byte Pass { get; set; }
    public byte MaxPassCount { get; set; }
    public string LanguageCode { get; set; }
    public GameLevels Difficulty { get; set; } = GameLevels.Hard;
    public Stack<WordForGameDTO> Words { get; set; }
    public List<int> UsedWordIds { get; set; }
}