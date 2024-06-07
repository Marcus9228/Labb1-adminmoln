

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2_webbutv.Models
{
    [Table("_Pokemons")]
    public class Pokemon
    {
        [Key]
        public int id { get; set; }
        public int PokemonID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Experience { get; set; }
        public List<Item> Items { get; set; }
    }
}
