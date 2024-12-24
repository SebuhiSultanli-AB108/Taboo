namespace Taboo.DTOs.Word;

public class WordForGameDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> BannedWords { get; set; }
}
