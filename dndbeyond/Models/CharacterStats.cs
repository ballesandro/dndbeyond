namespace dndbeyond.Models
{
    public class CharacterStats
    {
        public CharacterStats()
        {
        }

        public long Id { get; set; }

        // This assumes that every character has the same type of stats.
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}
