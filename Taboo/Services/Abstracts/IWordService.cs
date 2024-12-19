using Taboo.DTOs.Word;

namespace Taboo.Services.Abstracts;

public interface IWordService
{
    Task<int> CreateAsync(WordCreateDTO dto);
    Task<IEnumerable<WordGetDTO>> GetAsync();
    Task UpdateAsync(int id, WordUpdateDTO dto);
    Task DeleteAsync(int id);
    Task<WordGetDTO> GetByIdAsync(int id);
}
