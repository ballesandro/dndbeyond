using System;
namespace dndbeyond.Services.Implementations
{
    public class DiceService
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
    }
}
