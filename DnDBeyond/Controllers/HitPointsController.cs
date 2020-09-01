using System;
using System.Threading.Tasks;
using DnDBeyond.Models;
using DnDBeyond.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DnDBeyond.Controllers
{
    /// <summary>
    /// Controller for performing hit point operations, including:
    ///     setting temporary hit points, healing, damaging.
    /// </summary>
    [Route("api/[controller]")]
    public class HitPointsController : Controller
    {
        private readonly ILogger<HitPointsController> _logger;
        private readonly IHitPointsService _hitPointsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HitPointsController"/> class.
        /// </summary>
        /// <param name="logger">Logger for the class.</param>
        /// <param name="hitPointsService">Service to perform hit points logic.</param>
        public HitPointsController(ILogger<HitPointsController> logger, IHitPointsService hitPointsService)
        {
            _logger = logger;
            _hitPointsService = hitPointsService;
        }

        /// <summary>
        /// Updates a character's temporary hit points.
        /// </summary>
        /// <param name="id">Character's unique identifier.</param>
        /// <param name="temporaryHitPoints">The amount of temporary hit points to use.</param>
        /// <returns>The character, with updated temporary hit points.</returns>
        /// <response code="200">Returns the updated character.</response>
        /// <response code="404">If a character with given id cannot be found.</response>
        [HttpPut("temporary")]
        [Produces("application/json")]
        public async Task<ActionResult<Character>> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            _logger.LogInformation("PUT /api/hitPoints/temporary for id " + id);

            try
            {
                return await _hitPointsService.UpdateTemporaryHitPoints(id, temporaryHitPoints);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Damages a character.
        /// </summary>
        /// <param name="id">Character's unique identifier.</param>
        /// <param name="damage">The raw amount of damage to do.</param>
        /// <param name="damageType">The type of the damage (e.g., fire).</param>
        /// <returns>The character, with updated hit points.</returns>
        /// <response code="200">Returns the updated character.</response>
        /// <response code="404">If a character with given id cannot be found.</response>
        [HttpPut("damage")]
        [Produces("application/json")]
        public async Task<ActionResult<Character>> DamageCharacter(long id, int damage, string damageType)
        {
            _logger.LogInformation("PUT /api/hitPoints/damage for id " + id);

            try
            {
                return await _hitPointsService.DamageCharacter(id, damage, damageType);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Heals a character.
        /// </summary>
        /// <param name="id">Character's unique identifier.</param>
        /// <param name="heal">The amount of healing to do.</param>
        /// <returns>The character, with updated hit points.</returns>
        /// <response code="200">Returns the updated character.</response>
        /// <response code="404">If a character with given id cannot be found.</response>
        [HttpPut("heal")]
        [Produces("application/json")]
        public async Task<ActionResult<Character>> HealCharacter(long id, int heal)
        {
            _logger.LogInformation("PUT /api/hitPoints/heal for id " + id);

            try
            {
                return await _hitPointsService.HealCharacter(id, heal);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
