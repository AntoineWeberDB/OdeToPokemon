using OdeToPokemon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToPokemon.Data
{
    public interface IPokemonData
    {
        IEnumerable<Pokemon> GetPokemonsByName(string name);
    }

    public class InMemoryPokemonData : IPokemonData
    {
        readonly List<Pokemon> pokemons;

        public InMemoryPokemonData()
        {
            pokemons = new List<Pokemon>(){
                new Pokemon { Id = 1, Name = "Bulbizarre", Type = PokemonType.Grass },
                new Pokemon { Id = 4, Name = "Salameche", Type = PokemonType.Fire },
                new Pokemon { Id = 7, Name = "Carapuce", Type = PokemonType.Water }
            };
        }

        public IEnumerable<Pokemon> GetPokemonsByName(string name = null)
        {
            return from p in pokemons
                   where string.IsNullOrEmpty(name) || p.Name.StartsWith(name)
                   orderby p.Name
                   select p;
                   
        }
    }
}
