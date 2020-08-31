using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.DB;
using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    /// <inheritdoc/>
    public class CharactersService : ICharactersService
    {
        private readonly CharactersRepository _repo;
        private readonly IHitPointsService _hpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharactersService"/> class.
        /// </summary>
        /// <param name="repo">The character repository to use.</param>
        /// <param name="hpService">An implementation of hit points service.</param>
        public CharactersService(CharactersRepository repo, IHitPointsService hpService)
        {
            _repo = repo;
            _hpService = hpService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _repo.GetAll();
        }

        /// <inheritdoc/>
        public async Task<Character> GetCharacter(long id)
        {
            return await _repo.GetById(id);
        }

        /// <inheritdoc/>
        /// Maximum hit points are determined during creation. If "hitPointsMethod" is supplied, you can
        /// specify "average" or "random" for the type of generation to use, otherwise it will default to average.
        public async Task<Character> CreateCharacter(Character character)
        {
            var maxHitPoints = _hpService.CalculateMaxHitPoints(character);
            character.MaxHitPoints = maxHitPoints;
            character.CurrentHitPoints = maxHitPoints;

            await _repo.Add(character);

            return character;
        }
    }
}
