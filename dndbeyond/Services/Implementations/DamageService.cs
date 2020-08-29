using System;
using dndbeyond.Models;

namespace dndbeyond.Services.Implementations
{
    public class DamageService
    {
        public const string RESISTANCE = "resistance";
        public const string IMMUNITY = "immunity";

        public DamageService()
        {
        }

        /**
         * Damages a character across temporary and current hit points, if applicable.
         * 
         * Assumes a character can have negative hit points.
         */
        public void DamageCharacter(Character character, int damage, string damageType)
        {
            var modifier = getDamageMod(character, damageType);
            damage = (int)(damage * modifier);

            if(damage <= character.TemporaryHitPoints)
            {
                character.TemporaryHitPoints -= damage;
            } else
            {
                var overflow = character.TemporaryHitPoints - damage;
                character.TemporaryHitPoints = 0;
                character.CurrentHitPoints += overflow;
            }
        }

        /**
         * Gets damage modifier based on character's defenses.
         * 
         * Assumes that all damage has a type, and that "normal" damage can also be modified.
         */
        private double getDamageMod(Character character, string damageType)
        {
            var modifier = 1.0;
            foreach (CharacterDefense defense in character.Defenses)
            {
                if (damageType == defense.Type)
                {
                    if(IMMUNITY == defense.Defense)
                    {
                        modifier = 0;
                    } else if(RESISTANCE == defense.Defense)
                    {
                        modifier = 0.5;
                    }
                    break;
                }
            }

            return modifier;
        }

    }
}
