using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    /// <summary>
    /// Service that deals with damaging a character.
    /// </summary>
    public interface IDamageService
    {
        /// <summary>
        /// Changes a character's hit points to reflect damage taken.
        /// </summary>
        /// <param name="character">The character to damage.</param>
        /// <param name="damage">The raw amount of damage to take.</param>
        /// <param name="damageType">The type of damage to take.</param>
        void DamageCharacter(Character character, int damage, string damageType);
    }
}
