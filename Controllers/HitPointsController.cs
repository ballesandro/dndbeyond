using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dndbeyond.Controllers
{
    [Route("api/[controller]")]
    public class HitPointsController : Controller
    {
        // PUT: api/HitPoints/max
        [HttpPut("max")]
        public async Task<IActionResult> updateMaxHitPoints(long id, int hitPoints)
        {
            return NoContent();
        }

        // PUT: api/HitPoints/temporary
        [HttpPut("temporary")]
        public async Task<IActionResult> updateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            return NoContent();
        }

        // PUT: api/HitPoints/damage
        [HttpPut("damage")]
        public async Task<IActionResult> damageCharacter(long id, int damage)
        {
            return NoContent();
        }
    }
}
