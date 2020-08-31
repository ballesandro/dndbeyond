using System;
using System.Threading.Tasks;
using DnDBeyond.DB;
using DnDBeyond.Models;
using DnDBeyond.Models.Enum;

namespace DnDBeyond.Services
{
    /// <inheritdoc/>
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

        /// <inheritdoc/>
        /// Either a random or average method can be used to find the maximum.
        public int CalculateMaxHitPoints(Character character)
        {
            switch (character.HitPointsMethod)
            {
                case HitPointsMethod.Random:
                    return CalculateMaxHitPointsRandom(character);
                case HitPointsMethod.Average:
                default:
                    return CalculateMaxHitPointsAverage(character);
            }
        }

        /// <inheritdoc/>
        /// The temporary hit points are not cummulative; instead, the maximum of either the
        /// existing temporary hit points or the given value will be taken.
        public async Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints)
        {
            var character = await _repo.GetById(id);
            character.TemporaryHitPoints = Math.Max(temporaryHitPoints, character.TemporaryHitPoints);

            await _repo.Update(character);
            return character;
        }

        /// <inheritdoc/>
        public async Task<Character> DamageCharacter(long id, int damage, string damageType)
        {
            var character = await _repo.GetById(id);
            _damageService.DamageCharacter(character, damage, damageType);

            await _repo.Update(character);
            return character;
        }

        /// <inheritdoc/>
        public async Task<Character> HealCharacter(long id, int heal)
        {
            var character = await _repo.GetById(id);
            _healService.HealCharacter(character, heal);

            await _repo.Update(character);
            return character;
        }

        /// <summary>
        /// Uses the average of each hit dice to find a character's maximum hit points.
        /// </summary>
        /// <param name="character">The character to find maximum hit points for.</param>
        /// <returns>The maximum hit points the character can have.</returns>
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

        /// <summary>
        /// Randomly "rolls" each hit dice to find a character's maximum hit points.
        /// </summary>
        /// <param name="character">The character to find maximum hit points for.</param>
        /// <returns>The maximum hit points the character can have.</returns>
        internal int CalculateMaxHitPointsRandom(Character character)
        {
            var maxHitPoints = 0;

            foreach (CharacterClass characterClass in character.Classes)
            {
                for (int i = 0; i < characterClass.ClassLevel; i++)
                {
                    maxHitPoints += _diceService.Roll(characterClass.HitDiceValue);
                }
            }

            var conMod = CalculateConMod(character);
            maxHitPoints += character.Level * conMod;

            return maxHitPoints;
        }

        /// <summary>
        /// Calculates a character's con mod from stats and items.
        /// </summary>
        /// <param name="character">The character to find the con mod for.</param>
        /// <returns>The character's con mod.</returns>
        /// todo: This could be moved into a stats service eventually.
        internal int CalculateConMod(Character character)
        {
            var con = character.Stats.Constitution;

            //foreach (Item item in character.Items)
            //{
            //    var modifier = item.Modifier;
            //    if (modifier.AffectedObject == "stats" && modifier.AffectedValue == "constitution")
            //    {
            //        con += modifier.Value;
            //    }
            //}

            var mod = (con - 10) / 2.0;
            if (mod >= 0)
            {
                return (int)Math.Ceiling(mod);
            }

            return (int)Math.Floor(mod);
        }
    }
}
