using System.Collections.Generic;
using System.Threading.Tasks;
using dndbeyond.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dndbeyond.Services
{
    public class CharactersService : ICharactersService
    {

        private readonly CharactersContext _context;

        public CharactersService(CharactersContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _context.Characters.ToListAsync();

        }

        public async Task<ActionResult<Character>> GetCharacter(long id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<int> CreateCharacter(Character character)
        {
            _context.Characters.Add(character);
            return await _context.SaveChangesAsync();
        }

    }
}
