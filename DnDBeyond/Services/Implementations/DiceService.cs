using System;

namespace DnDBeyond.Services.Implementations
{
    /// <inheritdoc/>
    public class DiceService : IDiceService
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceService"/> class.
        /// </summary>
        public DiceService()
        {
            random = new Random();
        }

        /// <inheritdoc/>
        public int Roll(int dieValue)
        {
            return random.Next(1, dieValue + 1);
        }

        /// <inheritdoc/>
        /// Calculates the expected value of a discrete uniform distribution.
        public int GetDieAverage(int hitDiceValue)
        {
            return (int)Math.Ceiling((hitDiceValue + 1) / 2.0);
        }
    }
}
