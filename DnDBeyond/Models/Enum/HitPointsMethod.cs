namespace DnDBeyond.Models.Enum
{
    /// <summary>
    /// Represents the different ways that a character's hit points pool can be generated.
    /// </summary>
    public enum HitPointsMethod
    {
        /// <summary>
        /// Uses the average of a class's hit die value.
        /// </summary>
        Average,

        /// <summary>
        /// "Rolls" a class's hit die to find the value.
        /// </summary>
        Random
    }
}
