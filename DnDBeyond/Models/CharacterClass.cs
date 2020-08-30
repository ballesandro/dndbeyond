namespace DnDBeyond.Models
{
    public class CharacterClass
    {
        public CharacterClass()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int HitDiceValue { get; set; }
        public int ClassLevel { get; set; }
    }
}
