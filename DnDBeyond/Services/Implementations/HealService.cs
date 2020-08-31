using System;
using DnDBeyond.Models;

namespace DnDBeyond.Services.Implementations
{
    /// <inheritdoc/>
    public class HealService : IHealService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealService"/> class.
        /// </summary>
        public HealService()
        {
        }

        /// <inheritdoc/>
        /// Basic healing for now. It will not replenish temporary hit points or
        /// heal above maximum hit points.
        public void HealCharacter(Character character, int heal)
        {
            var health = character.CurrentHitPoints + heal;
            character.CurrentHitPoints = Math.Min(character.MaxHitPoints, health);
        }
    }
}
