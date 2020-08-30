using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    public interface ICharactersService
    {
        Task<IEnumerable<Character>> GetCharacters();
        Task<Character> GetCharacter(long id);
        Task<Character> CreateCharacter(Character character);
    }
}
