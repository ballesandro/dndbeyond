using DnDBeyond.Models;

namespace DnDBeyond.Services
{
    public interface IDamageService
    {
        void DamageCharacter(Character character, int damage, string damageType);
    }
}
