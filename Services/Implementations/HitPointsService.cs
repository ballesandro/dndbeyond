using System;
using System.Threading.Tasks;
using dndbeyond.Models;

namespace dndbeyond.Services
{
    public class HitPointsService : IHitPointsService
    {
        private readonly CharactersContext _context;

        public HitPointsService(CharactersContext context)
        {
            _context = context;
        }

        public async Task<Character> UpdateMaxHitPoints(long id, int hitPoints)
        {
            var character = await _context.Characters.FindAsync(id);
            if(character == null)
            {
                throw new ArgumentException("Cannot find character with id " + 1);
            }

            character.MaxHitPoints = hitPoints;
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new ArgumentException("Cannot find character with id " + 1);
            }

            character.TemporaryHitPoints = temporaryHitPoints;
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> DamageCharacter(long id, int damage)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new ArgumentException("Cannot find character with id " + 1);
            }

            DamageCharacter(character, damage);

            await _context.SaveChangesAsync();

            return character;
        }

        private void DamageCharacter(Character character, int damage)
        {
            var overflow = character.TemporaryHitPoints - damage;
            character.TemporaryHitPoints = overflow < 0 ? 0 : overflow;
            character.CurrentHitPoints += overflow; // assuming we don't care about negative health
        }
    }
}
