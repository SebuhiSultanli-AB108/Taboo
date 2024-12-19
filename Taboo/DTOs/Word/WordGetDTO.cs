namespace Taboo.DTOs.Word;

public class WordGetDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string LanguageCode { get; set; }
    public ICollection<string> BannedWords { get; set; }
}
