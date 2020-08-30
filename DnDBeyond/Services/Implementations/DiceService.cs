using System;
namespace DnDBeyond.Services.Implementations
{
    public class DiceService : IDiceService
    {

        private readonly Random random;

        public DiceService()
        {
            random = new Random();
        }

        public int Roll(int dieValue)
        {
            return random.Next(1, dieValue + 1);
        }

        public int GetDieAverage(int hitDiceValue)
        {
            return (int)Math.Ceiling((hitDiceValue + 1) / 2.0);
        }

    }
}
