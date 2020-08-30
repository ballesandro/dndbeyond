using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DnDBeyond.Models;
using Microsoft.Extensions.Logging;
using DnDBeyond.Services;

namespace DnDBeyond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ILogger<CharactersController> _logger;
        private readonly ICharactersService _charactersService;

        public CharactersController(ILogger<CharactersController> logger, ICharactersService charactersService)
        {
            _logger = logger;
            _charactersService = charactersService;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            _logger.LogInformation("GET /api/characters");
            return await _charactersService.GetCharacters();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(long id)
        {
            _logger.LogInformation("GET /api/characters/" + id);
            var character = await _charactersService.GetCharacter(id);
        
            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // POST: api/Characters
        [HttpPost]
        public async Task<ActionResult<Character>> CreateCharacter(Character character)
        {
            _logger.LogInformation("POST /api/characters");
            character = await _charactersService.CreateCharacter(character);

            return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
        }

    }
}
