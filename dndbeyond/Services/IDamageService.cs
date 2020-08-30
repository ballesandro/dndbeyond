using dndbeyond.Models;

namespace dndbeyond.Services
{
    public interface IDamageService
    {
        void DamageCharacter(Character character, int damage, string damageType);
    }
}
