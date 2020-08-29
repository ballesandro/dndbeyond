using System.Collections.Generic;

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
        public CharacterStats Stats { get; set; }
        public List<CharacterClass> Classes { get; set; } = new List<CharacterClass>();
        public List<CharacterDefense> Defenses { get; set; } = new List<CharacterDefense>();
    }
}
