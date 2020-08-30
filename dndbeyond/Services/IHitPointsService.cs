using System.Threading.Tasks;
using dndbeyond.Models;

namespace dndbeyond.Services
{
    public interface IHitPointsService
    {
        int CalculateMaxHitPoints(Character character);
        Task<Character> UpdateMaxHitPoints(long id, int hitPoints);
        Task<Character> UpdateTemporaryHitPoints(long id, int temporaryHitPoints);
        Task<Character> DamageCharacter(long id, int damage, string damageType);
        Task<Character> HealCharacter(long id, int heal);
    }
}
