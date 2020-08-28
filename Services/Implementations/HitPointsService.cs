using System;
using System.Threading.Tasks;
using dndbeyond.Models;
using dndbeyond.Services.Implementations;

namespace dndbeyond.Services
{
    public class HitPointsService : IHitPointsService
    {
        private readonly CharactersContext _context;
        private readonly ICharactersService _charactersService;
        private readonly DamageService _damageService;

        public HitPointsService(CharactersContext context, ICharactersService charactersService, DamageService damageService)
        {
            _context = context;
            _charactersService = charactersService;
            _damageService = damageService;
        }

        public async Task<Character> UpdateMaxHitPoints(long id, int hitPoints)
        {
            var character = await _charactersService.GetCharacter(id);

            character.MaxHitPoints = hitPoints;
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            var character = await _charactersService.GetCharacter(id);

            character.TemporaryHitPoints = temporaryHitPoints;
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> DamageCharacter(long id, int damage)
        {
            var character = await _charactersService.GetCharacter(id);

            _damageService.DamageCharacter(character, damage);
            await _context.SaveChangesAsync();

            return character;
        }

    }
}
