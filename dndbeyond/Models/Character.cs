using System.Collections.Generic;
using dndbeyond.Models.Enum;

namespace dndbeyond.Models
{
    public class Character
    {
        public Character()
        {
        }

        public long Id { get; set;  }
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }
        public HitPointsMethod hitPointsMethod { get; set; }
        public CharacterStats Stats { get; set; }
        public List<CharacterClass> Classes { get; set; } = new List<CharacterClass>();
        public List<CharacterDefense> Defenses { get; set; } = new List<CharacterDefense>();
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
