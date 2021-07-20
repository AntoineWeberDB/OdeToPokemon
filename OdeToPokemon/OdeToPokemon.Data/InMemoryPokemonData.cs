using OdeToPokemon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToPokemon.Data
{
    public class InMemoryPokemonData : IPokemonData
    {
        readonly List<Pokemon> pokemons;

        public InMemoryPokemonData()
        {
            pokemons = new List<Pokemon>() {
                new Pokemon { Id = 0, Name = "-", Type = PokemonType.Normal},
                new Pokemon { Id = 1, Name = "Bulbizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 2, Name = "Herbizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 3, Name = "Florizarre", Type = PokemonType.Grass},
                new Pokemon { Id = 4, Name = "Salameche", Type = PokemonType.Fire },
                new Pokemon { Id = 5, Name = "Reptincel", Type = PokemonType.Fire },
                new Pokemon { Id = 6, Name = "Dracofeu", Type = PokemonType.Fire },
                new Pokemon { Id = 7, Name = "Carapuce", Type = PokemonType.Water },
                new Pokemon { Id = 8, Name = "Carabaffe", Type = PokemonType.Water },
                new Pokemon { Id = 9, Name = "Tortank", Type = PokemonType.Water },
                new Pokemon { Id = 25, Name = "Pikachu", Type = PokemonType.Electric }
            };


            for (int i = 1; i < pokemons.Count; i++)
            {
                pokemons[i].PreviousEvolution = pokemons[0];
                pokemons[i].NextEvolution = pokemons[0];
            }

            pokemons[2].PreviousEvolution = pokemons[1];
            pokemons[2].NextEvolution = pokemons[3];

            pokemons[5].PreviousEvolution = pokemons[4];
            pokemons[5].NextEvolution = pokemons[6];

            pokemons[8].PreviousEvolution = pokemons[7];
            pokemons[8].NextEvolution = pokemons[9];

            SetPokemonParents(pokemons[2]);
            SetPokemonParents(pokemons[5]);
            SetPokemonParents(pokemons[8]);

        }

        public void SetPokemonParents(Pokemon pokemon)
        {
            if (pokemon.PreviousEvolution.Id != 0)
            {
                GetPokemonById(pokemon.Id).NextEvolution.PreviousEvolution = pokemons[0];
                Pokemon previous = GetPokemonById(pokemon.PreviousEvolution.Id);
                previous.NextEvolution = GetPokemonById(pokemon.Id);
            }
            if (pokemon.NextEvolution.Id != 0)
            {
                GetPokemonById(pokemon.Id).PreviousEvolution.NextEvolution = pokemons[0];
                Pokemon next = GetPokemonById(pokemon.NextEvolution.Id);
                next.PreviousEvolution = GetPokemonById(pokemon.Id);
            }
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

        public Boolean IsPokemonIdValid(int id)
        {
            if (id > 0 && id < 152)
            {
                Pokemon temp = GetPokemonById(id);
                if (temp == null)
                {
                    return true;
                }
            }

            return false;

        }

        public Boolean Add(Pokemon newPokemon)
        {
            if (IsPokemonIdValid(newPokemon.Id))
            {
                pokemons.Add(newPokemon);
                SetPokemonParents(newPokemon);
                return true;
            }
            return false;
        }


        public Pokemon Update(Pokemon updatedPokemon, int previousId)
        {
            var pokemon = pokemons.SingleOrDefault(p => p.Id == previousId);
            if (pokemon != null)
            {
                if ((IsPokemonIdValid(updatedPokemon.Id)) || (updatedPokemon.Id == previousId))
                {
                    pokemon.Id = updatedPokemon.Id;
                    pokemon.Name = updatedPokemon.Name;
                    pokemon.Type = updatedPokemon.Type;
                    SetPokemonParents(pokemon);
                    pokemon.PreviousEvolution = updatedPokemon.PreviousEvolution;
                    pokemon.NextEvolution = updatedPokemon.NextEvolution;
                    SetPokemonParents(pokemon);

                }
            }
            return pokemon;

        }

        public int Commit()
        {
            return 0;
        }

        public Pokemon Delete(int id)
        {
            var pokemon = pokemons.FirstOrDefault(r => r.Id == id);
            if(pokemon != null)
            {
                pokemons.Remove(pokemon);
            }

            return pokemon;
        }

        public int GetCountOfPokemons()
        {
            return pokemons.Count();
        }
    }
}
