using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToPokemon.Core
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonType Type { get; set; }
        public Pokemon PreviousEvolution { get; set; } = null;
        public Pokemon NextEvolution { get; set; } = null;

        public Pokemon()
        {
        }
    }
}
