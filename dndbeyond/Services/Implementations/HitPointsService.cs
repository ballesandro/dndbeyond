using System;
using System.Threading.Tasks;
using dndbeyond.DB;
using dndbeyond.Models;
using dndbeyond.Models.Enum;

namespace dndbeyond.Services
{
    public class HitPointsService : IHitPointsService
    {
        private readonly CharactersRepository _repo;
        private readonly IDiceService _diceService;
        private readonly IDamageService _damageService;
        private readonly IHealService _healService;

        public HitPointsService(CharactersRepository repo, IDiceService diceService, IDamageService damageService, IHealService healService)
        {
            _repo = repo;
            _diceService = diceService;
            _damageService = damageService;
            _healService = healService;
        }

        public int CalculateMaxHitPoints(Character character)
        {
            switch(character.hitPointsMethod)
            {
                case HitPointsMethod.Random:
                    return CalculateMaxHitPointsRandom(character);
                case HitPointsMethod.Average:
                default:
                    return CalculateMaxHitPointsAverage(character);
            }
        }

        public async Task<Character> UpdateMaxHitPoints(long id, int hitPoints)
        {
            var character = await _repo.GetById(id);
            character.MaxHitPoints = hitPoints;

            await _repo.Update(character);
            return character;
        }

        public async Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            var character = await _repo.GetById(id);
            character.TemporaryHitPoints = Math.Max(temporaryHitPoints, character.TemporaryHitPoints);

            await _repo.Update(character);
            return character;
        }

        public async Task<Character> DamageCharacter(long id, int damage, string damageType)
        {
            var character = await _repo.GetById(id);
            _damageService.DamageCharacter(character, damage, damageType);

            await _repo.Update(character);
            return character;
        }

        public async Task<Character> HealCharacter(long id, int heal)
        {
            var character = await _repo.GetById(id);
            _healService.HealCharacter(character, heal);

            await _repo.Update(character);
            return character;
        }

        internal int CalculateMaxHitPointsAverage(Character character)
        {

            var maxHitPoints = 0;

            foreach (CharacterClass characterClass in character.Classes)
            {
                var average = _diceService.GetDieAverage(characterClass.HitDiceValue);
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

        internal int CalculateConMod(Character character)
        {
            var con = character.Stats.Constitution;

            foreach (Item item in character.Items)
            {
                var modifier = item.Modifier;
                if(modifier.AffectedObject == "stats" && modifier.AffectedValue == "constitution")
                {
                    con += modifier.Value;
                }
            }

            var mod = (con - 10) / 2.0;
            if(mod >= 0)
            {
                return (int)Math.Ceiling(mod);
            }
            return (int)Math.Floor(mod);
        }
    }
}
