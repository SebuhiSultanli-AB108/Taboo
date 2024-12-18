using Taboo.DTOs.Language;

namespace Taboo.Services.Abstracts;

public interface ILanguageService
{
    IEnumerable<LanguageGetDTO> GetAsync();
    Task CreateAsync(LanguageDTO dto);
}
