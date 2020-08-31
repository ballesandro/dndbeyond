using System.Collections.Generic;
using DnDBeyond.Models.Enum;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes a character and their inventory.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
        }

        public long Id { get; set;  }
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
        public HitPointsMethod HitPointsMethod { get; set; }
        public CharacterStats Stats { get; set; } = new CharacterStats();
        public List<CharacterClass> Classes { get; set; } = new List<CharacterClass>();
        public List<CharacterDefense> Defenses { get; set; } = new List<CharacterDefense>();
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
