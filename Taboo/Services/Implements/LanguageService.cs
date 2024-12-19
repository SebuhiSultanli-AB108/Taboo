using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Language;
using Taboo.Entities;
using Taboo.Exceptions.Language;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;

public class LanguageService(TabooDbContext _context, IMapper _mapper) : ILanguageService
{
    public async Task<string> CreateAsync(LanguageCreateDTO dto)
    {
        var data = _mapper.Map<Language>(dto);
        if (await _context.Languages.AnyAsync(x => x.Code == dto.Code))
            throw new LanguageExistException();
        await _context.Languages.AddAsync(data);
        await _context.SaveChangesAsync();
        return data.Code;
    }

    public async Task<IEnumerable<LanguageGetDTO>> GetAsync()
    {
        var languages = await _context.Languages.ToListAsync();
        return _mapper.Map<IEnumerable<LanguageGetDTO>>(languages);
    }

    public async Task UpdateAsync(string code, LanguageUpdateDTO dto)
    {
        var data = await _getByCodeAsync(code);
        data = _mapper.Map(dto, data);
        data.Icon = dto.IconUrl;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var target = await _getByCodeAsync(code);
        _context.Remove(target);
        await _context.SaveChangesAsync();
    }

    public async Task<LanguageGetDTO> GetByCodeAsync(string code)
    {
        return _mapper.Map<LanguageGetDTO>(await _getByCodeAsync(code));
    }

    async Task<Language> _getByCodeAsync(string code)
    {
        var data = await _context.Languages.FindAsync(code);
        if (data is null) throw new LanguageNotFoundException();
        return data;
    }
}