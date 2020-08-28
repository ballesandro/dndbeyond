using System;
using dndbeyond.Models;

namespace dndbeyond.Services.Implementations
{
    public class DamageService
    {
        public DamageService()
        {
        }

        public void DamageCharacter(Character character, int damage)
        {
            var overflow = character.TemporaryHitPoints - damage;
            character.TemporaryHitPoints = overflow < 0 ? 0 : overflow;
            character.CurrentHitPoints += overflow; // assuming we don't care about negative health
        }

    }
}
