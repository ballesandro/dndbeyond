using System.Collections.Generic;
using System.Threading.Tasks;
using dndbeyond.Models;
using Microsoft.AspNetCore.Mvc;

namespace dndbeyond.Services
{
    public interface ICharactersService
    {
        Task<IEnumerable<Character>> GetCharacters();
        Task<ActionResult<Character>> GetCharacter(long id);
        Task<int> PostCharacter(Character character);
    }
}
