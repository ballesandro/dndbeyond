using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.Models
{
    /// <summary>
    /// An extension of DbContext for the Character class.
    /// </summary>
    public class CharactersContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharactersContext"/> class.
        /// </summary>
        public CharactersContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharactersContext"/> class.
        /// </summary>
        /// <param name="options">Any options to use for DbContext.</param>
        public CharactersContext(DbContextOptions<CharactersContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
    }
}