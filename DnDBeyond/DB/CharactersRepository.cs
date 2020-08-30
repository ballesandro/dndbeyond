using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.Models;
using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.DB
{
    public class CharactersRepository
    {
        private readonly CharactersContext _context;

        public CharactersRepository(CharactersContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _context.Characters.ToListAsync();
        }

        public async Task<Character> GetById(long id)
        {
            return await _context.Characters.FindAsync(id);
        }

        public async Task<int> Add(Character character)
        {
            _context.Characters.Add(character);
            return await _context.SaveChangesAsync();
        }
                    
        public async Task<int> Update(Character character)
        {
            _context.Characters.Update(character);
            return await _context.SaveChangesAsync();
        }
    }
}
