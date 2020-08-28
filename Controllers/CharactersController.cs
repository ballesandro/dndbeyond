using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dndbeyond.Models;
using Microsoft.Extensions.Logging;
using dndbeyond.Services;

namespace dndbeyond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ILogger<CharactersController> _logger;

        private readonly ICharactersService _charactersService;

        public CharactersController(ILogger<CharactersController> logger, CharactersContext context, ICharactersService charactersService)
        {
            _logger = logger;
            _charactersService = charactersService;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _charactersService.GetCharacters();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(long id)
        {
            var character = await _charactersService.GetCharacter(id);
        
            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // POST: api/Characters
        [HttpPost]
        public async Task<ActionResult<Character>> createCharacter(Character character)
        {
            await _charactersService.CreateCharacter(character);

            return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
        }

    }
}
