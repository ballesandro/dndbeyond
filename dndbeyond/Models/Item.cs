namespace dndbeyond.Models
{
    public class Item
    {
        public Item()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public Modifier Modifier { get; set; }
    }
}
