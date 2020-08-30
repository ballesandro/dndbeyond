namespace DnDBeyond.Services
{
    public interface IDiceService
    {
        int Roll(int dieValue);
        int GetDieAverage(int hitDiceValue);
    }
}
