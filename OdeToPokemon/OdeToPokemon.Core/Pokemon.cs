using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToPokemon.Core
{
    public class Pokemon
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public PokemonType Type { get; set; }
        public Pokemon PreviousEvolution { get; set; } = null;
        public Pokemon NextEvolution { get; set; } = null;

        public Pokemon()
        {
        }
    }
}
