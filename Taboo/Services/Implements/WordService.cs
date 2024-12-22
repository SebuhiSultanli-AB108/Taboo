using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taboo.DAL;
using Taboo.DTOs.Word;
using Taboo.Entities;
using Taboo.Exceptions.Word;
using Taboo.Services.Abstracts;

namespace Taboo.Services.Implements;

public class WordService(TabooDbContext _context, IMapper _mapper) : IWordService
{
    public async Task<int> CreateAsync(WordCreateDTO dto)
    {
        if (await _context.Words.AnyAsync(x => x.LanguageCode == dto.LanguageCode && x.Text == dto.Text))
            throw new WordExistException();
        if (dto.BannedWords.Count() != 6)
            throw new InvalidBannedWordCountException();

        var bannedWords = dto.BannedWords.Select(x => new BannedWord { Text = x }).ToList();
        var data = _mapper.Map<Word>(dto);
        data.BannedWords = bannedWords;
        await _context.Words.AddAsync(data);
        await _context.SaveChangesAsync();
        return data.Id;
    }

    public async Task<IEnumerable<WordGetDTO>> GetAsync()
    {
        var word = await _context.Words.Include(x => x.BannedWords).ToListAsync();
        var data = _mapper.Map<IEnumerable<WordGetDTO>>(word);
        return data;
    }

    public async Task UpdateAsync(int id, WordUpdateDTO dto)
    {
        var data = await _getByIdAsync(id);
        data = _mapper.Map(dto, data);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var target = await _getByIdAsync(id);
        _context.Remove(target);
        await _context.SaveChangesAsync();
    }

    public async Task<WordGetDTO> GetByIdAsync(int id)
    {
        return _mapper.Map<WordGetDTO>(await _getByIdAsync(id));
    }

    async Task<Word> _getByIdAsync(int id)
    {
        var data = await _context.Words.FindAsync(id);
        if (data is null) throw new WordNotFoundException();
        return data;
    }
}