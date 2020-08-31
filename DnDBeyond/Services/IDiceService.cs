namespace DnDBeyond.Services
{
    /// <summary>
    /// Service that performs methods for dice.
    /// </summary>
    public interface IDiceService
    {
        /// <summary>
        /// Simulates rolling a die with the given value.
        /// </summary>
        /// <param name="dieValue">The value of the die to roll.</param>
        /// <returns>An int in range [1, dieValue].</returns>
        int Roll(int dieValue);

        /// <summary>
        /// Finds the statistical average of a die with the given value.
        /// </summary>
        /// <param name="dieValue">The value of the die to average.</param>
        /// <returns>The average value of the die.</returns>
        int GetDieAverage(int dieValue);
    }
}
