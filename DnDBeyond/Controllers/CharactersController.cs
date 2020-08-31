using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.Models;
using DnDBeyond.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DnDBeyond.Controllers
{
    /// <summary>
    /// Controller for CRUD operations for characters.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ILogger<CharactersController> _logger;
        private readonly ICharactersService _charactersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharactersController"/> class.
        /// </summary>
        /// <param name="logger">Logger for the class.</param>
        /// <param name="charactersService">Service to perform character logic.</param>
        public CharactersController(ILogger<CharactersController> logger, ICharactersService charactersService)
        {
            _logger = logger;
            _charactersService = charactersService;
        }

        /// <summary>
        /// Get all characters.
        /// </summary>
        /// <returns>A list of characters.</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            _logger.LogInformation("GET /api/characters");

            return await _charactersService.GetCharacters();
        }

        /// <summary>
        /// Get a character by its id.
        /// </summary>
        /// <param name="id">Character's unique identifier.</param>
        /// <returns>A character with given id.</returns>
        /// <response code="200">Returns the character with given id.</response>
        /// <response code="404">If a character with that id cannot be found.</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
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

        /// <summary>
        /// Create a new character.
        /// </summary>
        /// <param name="character">JSON representation of a character.</param>
        /// <returns>The newly created character, including id and hit points.</returns>
        [HttpPost]
        public async Task<ActionResult<Character>> CreateCharacter(Character character)
        {
            _logger.LogInformation("POST /api/characters");

            character = await _charactersService.CreateCharacter(character);
            return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
        }
    }
}
