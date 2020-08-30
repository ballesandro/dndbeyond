using dndbeyond.Models;

namespace dndbeyond.Services
{
    public interface IHealService
    {
        void HealCharacter(Character character, int heal);
    }
}
