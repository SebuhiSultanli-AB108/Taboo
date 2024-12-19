namespace Taboo.DTOs.Game;

public class GameUpdateDTO
{
    public int BannedWordCount { get; set; }
    public int FailCount { get; set; }
    public TimeSpan Time { get; set; }
    public int Score { get; set; }
    public int SuccessAnswer { get; set; }
    public int WrongAnswer { get; set; }
    public string LanguageCode { get; set; }
}
