namespace dndbeyond.Models
{
    public class Character
    {
        public Character()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
    }
}
