using System;
using System.Threading.Tasks;
using dndbeyond.Models;
using dndbeyond.Models.Enum;
using dndbeyond.Services.Implementations;

namespace dndbeyond.Services
{
    public class HitPointsService : IHitPointsService
    {
        private readonly CharactersContext _context;
        private readonly ICharactersService _charactersService;
        private readonly IDiceService _diceService;
        private readonly DamageService _damageService;

        public HitPointsService()
        {
        }

        public HitPointsService(CharactersContext context, ICharactersService charactersService, IDiceService diceService, DamageService damageService)
        {
            _context = context;
            _charactersService = charactersService;
            _diceService = diceService;
            _damageService = damageService;
        }

        public int CalculateMaxHitPoints(Character character, HitPointsMethod method)
        {
            switch(method)
            {
                case HitPointsMethod.Average:
                    return CalculateMaxHitPointsAverage(character);
                case HitPointsMethod.Random:
                    return CalculateMaxHitPointsRandom(character);
                default:
                    throw new ArgumentException("Unknown hit points method.");
            }
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

            character.TemporaryHitPoints = Math.Max(temporaryHitPoints, character.TemporaryHitPoints);
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task<Character> DamageCharacter(long id, int damage, string damageType)
        {
            var character = await _charactersService.GetCharacter(id);

            _damageService.DamageCharacter(character, damage, damageType);
            await _context.SaveChangesAsync();

            return character;
        }

        internal int CalculateMaxHitPointsAverage(Character character)
        {

            var maxHitPoints = 0;

            foreach (CharacterClass characterClass in character.Classes)
            {
                var average = getDieAverage(characterClass.HitDiceValue);
                maxHitPoints += average * characterClass.ClassLevel;
            }

            var conMod = CalculateConMod(character);
            maxHitPoints += character.Level * conMod;

            return maxHitPoints;
        }

        internal int CalculateMaxHitPointsRandom(Character character)
        {
            var maxHitPoints = 0;

            foreach (CharacterClass characterClass in character.Classes)
            {
                for(int i = 0; i < characterClass.ClassLevel; i++)
                {
                    maxHitPoints += _diceService.Roll(characterClass.HitDiceValue);
                }
            }

            var conMod = CalculateConMod(character);
            maxHitPoints += character.Level * conMod;

            return maxHitPoints;
        }

        internal int getDieAverage(int hitDiceValue)
        {
            return (int) Math.Ceiling((hitDiceValue + 1) / 2.0);
        }

        internal int CalculateConMod(Character character)
        {
            var con = character.Stats.Constitution;

            // todo: items

            var mod = (con - 10) / 2.0;
            if(mod >= 0)
            {
                return (int)Math.Ceiling(mod);
            }
            return (int)Math.Floor(mod);
        }
    }
}
