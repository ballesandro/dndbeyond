using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    public interface IHealService
    {
        void HealCharacter(Character character, int heal);
    }
}
