using Taboo.DTOs.Language;

namespace Taboo.Services.Abstracts;

public interface ILanguageService
{
    Task<IEnumerable<LanguageDTO>> GetAsync();
    Task CreateAsync(LanguageDTO dto);
    Task<bool> UpdateAsync(string code, LanguageDTO dto);
    Task<bool> DeleteAsync(string code);
}
