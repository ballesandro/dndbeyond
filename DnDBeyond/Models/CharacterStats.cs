using System.Text.Json.Serialization;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes a character's stats.
    ///
    /// Assumes that every character has the same type of stats.
    /// </summary>
    public class CharacterStats
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterStats"/> class.
        /// </summary>
        public CharacterStats()
        {
        }

        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public long CharacterId { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}
