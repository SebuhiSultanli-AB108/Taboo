using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IEnumerable<LanguageDTO>> GetAsync()
    {
        var languages = await _context.Languages.ToListAsync();
        return languages.Select(x => _mapper.Map<LanguageDTO>(x)).ToList();
    }
    public async Task<bool> UpdateAsync(string code, LanguageDTO dto)
    {
        #region alinmadi bu :(
        //var language = await _context.Languages.FindAsync(code);
        //if (language is null) return false;
        //language = _mapper.Map<Language>(dto);
        //_context.Languages.Update(language);
        #endregion
        #region Lazy way ◝(ᵔᵕᵔ)◜
        await DeleteAsync(code);
        await CreateAsync(dto);
        #endregion
        return true;
    }
    public async Task<bool> DeleteAsync(string code)
    {
        var target = await _context.Languages.FindAsync(code);
        if (target is null) return false;
        _context.Remove(target);
        await _context.SaveChangesAsync();
        return true;
    }
}