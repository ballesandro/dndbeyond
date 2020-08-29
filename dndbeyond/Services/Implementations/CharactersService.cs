using System.Collections.Generic;
using System.Threading.Tasks;
using dndbeyond.Models;
using dndbeyond.Models.Enum;
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

        public async Task<Character> GetCharacter(long id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<Character> CreateCharacter(Character character, HitPointsMethod method)
        {
            var hpService = new HitPointsService();
            hpService.CalculateMaxHitPoints(character, method);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return character;
        }

    }
}
