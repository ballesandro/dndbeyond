using System.Threading.Tasks;
using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    /// <summary>
    /// Service that deals with modifying hit points.
    /// </summary>
    public interface IHitPointsService
    {
        /// <summary>
        /// Calculates the maximum hit points a character could have.
        /// </summary>
        /// <param name="character">The character to find maximum hit points for.</param>
        /// <returns>The maximum hit points a character could have.</returns>
        int CalculateMaxHitPoints(Character character);

        /// <summary>
        /// Updates a character's temporary hit points.
        /// </summary>
        /// <param name="id">The id of the character to update.</param>
        /// <param name="temporaryHitPoints">The amount of temporary hit points to give.</param>
        /// <returns>The character, with updated hit points.</returns>
        Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints);

        /// <summary>
        /// Updates a character's hit points based on damage taken.
        /// </summary>
        /// <param name="id">The id of the character to update.</param>
        /// <param name="damage">The raw amount of damage taken.</param>
        /// <param name="damageType">The type of damage taken.</param>
        /// <returns>The character, with updated hit points.</returns>
        Task<Character> DamageCharacter(long id, int damage, string damageType);
        Task<Character> HealCharacter(long id, int heal);
    }
}
