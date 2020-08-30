using System.Threading.Tasks;
using dndbeyond.Models;
using dndbeyond.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dndbeyond.Controllers
{
    [Route("api/[controller]")]
    public class HitPointsController : Controller
    {
        private readonly ILogger<HitPointsController> _logger;
        private readonly IHitPointsService _hitPointsService;

        public HitPointsController(ILogger<HitPointsController> logger, IHitPointsService hitPointsService)
        {
            _logger = logger;
            _hitPointsService = hitPointsService;
        }

        // PUT: api/HitPoints/max
        [HttpPut("max")]
        public async Task<ActionResult<Character>> UpdateMaxHitPoints(long id, int hitPoints)
        {
            _logger.LogInformation("PUT /api/hitPoints/max for id " + id);
            return await _hitPointsService.UpdateMaxHitPoints(id, hitPoints);
        }

        // PUT: api/HitPoints/temporary
        [HttpPut("temporary")]
        public async Task<ActionResult<Character>> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            _logger.LogInformation("PUT /api/hitPoints/temporary for id " + id);
            return await _hitPointsService.UpdateTemporaryHitPoints(id, temporaryHitPoints);
        }

        // PUT: api/HitPoints/damage
        [HttpPut("damage")]
        public async Task<ActionResult<Character>> DamageCharacter(long id, int damage, string damageType)
        {
            _logger.LogInformation("PUT /api/hitPoints/damage for id " + id);
            return await _hitPointsService.DamageCharacter(id, damage, damageType);
        }

        // PUT: api/HitPoints/damage
        [HttpPut("heal")]
        public async Task<ActionResult<Character>> HealCharacter(long id, int heal)
        {
            _logger.LogInformation("PUT /api/hitPoints/heal for id " + id);
            return await _hitPointsService.HealCharacter(id, heal);
        }
    }
}
