using System;
using dndbeyond.Models;

namespace dndbeyond.Services.Implementations
{
    public class HealService : IHealService
    {
        public HealService()
        {
        }

        public void HealCharacter(Character character, int heal)
        {
            var health = character.CurrentHitPoints + heal;
            character.CurrentHitPoints = Math.Min(character.MaxHitPoints, health);
        }
    }
}
