using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    /// <summary>
    /// Service that deals with characters.
    /// </summary>
    public interface ICharactersService
    {
        /// <summary>
        /// Asynchronously returns a list of all characters.
        /// </summary>
        /// <returns>A list of all characters.</returns>
        Task<IEnumerable<Character>> GetCharacters();

        /// <summary>
        /// Asynchronously gets a character with given id.
        /// </summary>
        /// <param name="id">The character's unique identifier.</param>
        /// <returns>A character with given id, or null.</returns>
        Task<Character> GetCharacter(long id);

        /// <summary>
        /// Performs any creation logic for character, and saves it to the database.
        /// </summary>
        /// <param name="character">The character to add.</param>
        /// <returns>The character, with updated hit points and id.</returns>
        Task<Character> CreateCharacter(Character character);
    }
}
