using System.Text.Json.Serialization;
using DnDBeyond.Models.Enum;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes one character defense.
    /// </summary>
    public class CharacterDefense
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterDefense"/> class.
        /// </summary>
        public CharacterDefense()
        {
        }

        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public long CharacterId { get; set; }
        public string Type { get; set; }
        public DefenseDegree Defense { get; set; }
    }
}
