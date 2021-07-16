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
        Pokemon GetPokemonById(int id);
    }

    public class InMemoryPokemonData : IPokemonData
    {
        readonly List<Pokemon> pokemons;

        public InMemoryPokemonData()
        {
            pokemons = new List<Pokemon>(){
                new Pokemon { Id = 1, Name = "Bulbizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 2, Name = "Herbizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 3, Name = "Florizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 4, Name = "Salameche", Type = PokemonType.Fire },
                new Pokemon { Id = 5, Name = "Reptincel", Type = PokemonType.Fire },
                new Pokemon { Id = 6, Name = "Dracofeu", Type = PokemonType.Fire },
                new Pokemon { Id = 7, Name = "Carapuce", Type = PokemonType.Water },
                new Pokemon { Id = 8, Name = "Carabaffe", Type = PokemonType.Water },
                new Pokemon { Id = 9, Name = "Tortank", Type = PokemonType.Water },
                new Pokemon { Id = 10, Name = "Pikachu", Type = PokemonType.Electric }
            };

            pokemons[0].NextEvolution = pokemons[1];
            pokemons[1].PreviousEvolution = pokemons[0];

            pokemons[1].NextEvolution = pokemons[2];
            pokemons[2].PreviousEvolution = pokemons[1];

            pokemons[3].NextEvolution = pokemons[4];
            pokemons[4].PreviousEvolution = pokemons[3];

            pokemons[4].NextEvolution = pokemons[5];
            pokemons[5].PreviousEvolution = pokemons[4];

            pokemons[7].NextEvolution = pokemons[6];
            pokemons[6].PreviousEvolution = pokemons[7];

            pokemons[8].NextEvolution = pokemons[7];
            pokemons[7].PreviousEvolution = pokemons[8];
            
        }

        public IEnumerable<Pokemon> GetPokemonsByName(string name = null)
        {
            return from p in pokemons
                   where string.IsNullOrEmpty(name) || p.Name.StartsWith(name)
                   orderby p.Id
                   select p;
                   
        }
        public Pokemon GetPokemonById(int id)
        {
            return pokemons.SingleOrDefault(p => p.Id == id);  
        }
    }
}
