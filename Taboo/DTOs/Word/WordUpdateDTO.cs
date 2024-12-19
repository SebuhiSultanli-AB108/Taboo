namespace Taboo.DTOs.Word;

public class WordUpdateDTO
{
    public string Text { get; set; }
    public string LanguageCode { get; set; }
    public ICollection<string> BannedWords { get; set; }
}
