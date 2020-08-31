using DnDBeyond.Models;
using DnDBeyond.Models.Enum;

namespace DnDBeyond.Services.Implementations
{
    /// <inheritdoc/>
    public class DamageService : IDamageService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DamageService"/> class.
        /// </summary>
        public DamageService()
        {
        }

        /// <inheritdoc/>
        /// Damage type and character resistances/immunities are taken into account when
        /// calculating the damage taken. Damage will first be applied to temporary hit points.
        /// Assumes a character's hit points can be negative.
        public void DamageCharacter(Character character, int damage, string damageType)
        {
            var modifier = GetDamageMod(character, damageType);
            damage = (int)(damage * modifier);

            if (damage <= character.TemporaryHitPoints)
            {
                character.TemporaryHitPoints -= damage;
            }
            else
            {
                var overflow = character.TemporaryHitPoints - damage;
                character.TemporaryHitPoints = 0;
                character.CurrentHitPoints += overflow;
            }
        }

        /// <summary>
        /// Gets damage modifier based on character's defenses.
        ///
        /// Assumes that all damage has a type, and that "normal" damage can also be modified.
        /// </summary>
        /// <param name="character">The character to find damage mod for.</param>
        /// <param name="damageType">The type of damage the character will take.</param>
        /// <returns>The damage modifier.</returns>
        private double GetDamageMod(Character character, string damageType)
        {
            foreach (CharacterDefense defense in character.Defenses)
            {
                if (damageType == defense.Type)
                {
                    switch (defense.Defense)
                    {
                        case DefenseDegree.Immunity:
                            return 0;
                        case DefenseDegree.Resistance:
                            return 0.5;
                        default:
                            return 1;
                    }
                }
            }

            return 1;
        }
    }
}
