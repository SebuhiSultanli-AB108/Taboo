using Taboo.DTOs.Language;

namespace Taboo.Services.Abstracts;

public interface ILanguageService
{
    Task<IEnumerable<LanguageGetDTO>> GetAsync();
    Task<LanguageGetDTO> GetByCodeAsync(string code);
    Task<string> CreateAsync(LanguageCreateDTO dto);
    Task UpdateAsync(string code, LanguageUpdateDTO dto);
    Task DeleteAsync(string code);
}
