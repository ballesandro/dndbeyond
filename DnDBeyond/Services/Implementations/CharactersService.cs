using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.DB;
using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    public class CharactersService : ICharactersService
    {

        private readonly CharactersRepository _repo;
        private readonly IHitPointsService _hpService;

        public CharactersService(CharactersRepository repo, IHitPointsService hpService)
        {
            _repo = repo;
            _hpService = hpService;
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _repo.GetAll();
        }

        public async Task<Character> GetCharacter(long id)
        {
            return await _repo.GetById(id);
        }

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
