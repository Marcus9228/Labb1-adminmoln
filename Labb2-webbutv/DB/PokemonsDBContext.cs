using Microsoft.EntityFrameworkCore;
using Labb2_webbutv.Models;

namespace Labb2_webbutv.DB
{
    public class PokemonsDBContext : DbContext
    {
        public PokemonsDBContext(DbContextOptions<PokemonsDBContext> options) : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().ToTable("_Pokemons");
            modelBuilder.Entity<Item>().ToTable("_Items");
        }
    }
}
