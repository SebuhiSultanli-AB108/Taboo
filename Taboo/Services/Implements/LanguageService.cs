using AutoMapper;
using Taboo.DAL;
using Taboo.DTOs.Language;
using Taboo.Entities;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;

public class LanguageService(TabooDbContext _context, IMapper _mapper) : ILanguageService
{
    public async Task CreateAsync(LanguageDTO dto)
    {
        var data = _mapper.Map<Language>(dto);
        await _context.Languages.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<LanguageGetDTO> GetAsync()
    {
        return [];
    }
}
