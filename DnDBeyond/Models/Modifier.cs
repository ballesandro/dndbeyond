using System.Text.Json.Serialization;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes a modifier.
    ///
    /// For example, an item can have a modifier that changes a stat.
    /// Currently, only items that modify constitution are useful.
    /// </summary>
    public class Modifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Modifier"/> class.
        /// </summary>
        public Modifier()
        {
        }

        [JsonIgnore]
        public long Id { get; set; }
        public string AffectedObject { get; set; }
        public string AffectedValue { get; set; }
        public int Value { get; set; }
    }
}
