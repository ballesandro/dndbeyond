using Microsoft.EntityFrameworkCore;
namespace dndbeyond.Models
{
    public class CharactersContext : DbContext
    {
        public CharactersContext()
        {
        }

        public CharactersContext(DbContextOptions<CharactersContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
    }
}