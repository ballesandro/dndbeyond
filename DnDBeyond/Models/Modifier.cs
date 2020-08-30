namespace DnDBeyond.Models
{
    public class Modifier
    {
        public Modifier()
        {
        }

        public long Id { get; set; }
        public string AffectedObject { get; set; }
        public string AffectedValue { get; set; }
        public int Value { get; set; }
    }
}
