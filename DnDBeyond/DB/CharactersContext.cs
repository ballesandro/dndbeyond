using DnDBeyond.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<CharacterDefense> CharacterDefenses { get; set; }
        public DbSet<CharacterStats> CharacterStats { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("characters");
            modelBuilder.Entity<CharacterClass>().ToTable("classes");
            modelBuilder.Entity<CharacterDefense>().ToTable("defenses");
            modelBuilder.Entity<CharacterStats>().ToTable("stats");
            modelBuilder.Entity<Item>().ToTable("items");
            modelBuilder.Entity<Modifier>().ToTable("modifiers");
        }
    }
}