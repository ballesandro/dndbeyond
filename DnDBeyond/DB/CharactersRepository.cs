using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.Models;
using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.DB
{
    /// <summary>
    /// A convenience wrapper for DbContext methods that can be used by services.
    /// </summary>
    public class CharactersRepository
    {
        private readonly CharactersContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharactersRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext to use.</param>
        public CharactersRepository(CharactersContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously gets all characters.
        /// </summary>
        /// <returns>A list of all characters.</returns>
        public async Task<IEnumerable<Character>> GetAll()
        {
            return await _context.Characters.ToListAsync();
        }

        /// <summary>
        /// Asynchronously finds a character with the given id.
        /// </summary>
        /// <param name="id">The id of the character to find.</param>
        /// <returns>A character with the given id, or null.</returns>
        public async Task<Character> GetById(long id)
        {
            return await _context.Characters.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously adds a character to the database. If it is not supplied,
        /// an id will be automatically generated.
        /// </summary>
        /// <param name="character">The character to add.</param>
        /// <returns>0 if not successful, or 1 otherwise.</returns>
        public async Task<int> Add(Character character)
        {
            _context.Characters.Add(character);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously saves updates to an existing character.
        /// </summary>
        /// <param name="character">The character to update.</param>
        /// <returns>0 if not successful, or 1 otherwise.</returns>
        public async Task<int> Update(Character character)
        {
            _context.Characters.Update(character);
            return await _context.SaveChangesAsync();
        }
    }
}
