using System.Collections.Generic;
using System.Threading.Tasks;
using dndbeyond.Models;
using dndbeyond.Models.Enum;

namespace dndbeyond.Services
{
    public interface ICharactersService
    {
        Task<IEnumerable<Character>> GetCharacters();
        Task<Character> GetCharacter(long id);
        Task<Character> CreateCharacter(Character character, HitPointsMethod method);
    }
}
