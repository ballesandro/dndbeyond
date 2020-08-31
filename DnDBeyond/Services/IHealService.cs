using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    /// <summary>
    /// Service that deals with healing a character.
    /// </summary>
    public interface IHealService
    {
        /// <summary>
        /// Changes a character's hit points to reflect healing taken.
        /// </summary>
        /// <param name="character">The character to heal.</param>
        /// <param name="heal">The amount of healing to take.</param>
        void HealCharacter(Character character, int heal);
    }
}
