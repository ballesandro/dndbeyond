using System.Text.Json.Serialization;

namespace DnDBeyond.Models
{
    /// <summary>
    /// A class that describes an item.
    ///
    /// An item can also have a modifier.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
        }

        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public Modifier Modifier { get; set; }
    }
}
