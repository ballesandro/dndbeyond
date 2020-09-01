using System.Text.Json.Serialization;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes a character's class.
    /// </summary>
    public class CharacterClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterClass"/> class.
        /// </summary>
        public CharacterClass()
        {
        }

        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public long CharacterId { get; set; }
        public string Name { get; set; }
        public int HitDiceValue { get; set; }
        public int ClassLevel { get; set; }
    }
}
