using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OdeToPokemon.Core;
using Microsoft.EntityFrameworkCore;

namespace OdeToPokemon.Data
{

    public class SqlPokemonData : IPokemonData
    {
        private readonly OdeToPokemonDbContext db;

        public SqlPokemonData(OdeToPokemonDbContext db)
        {
            this.db = db;
            if (GetPokemonsByName("").Count()==0)
            {
                InitializePokemonDb();
            }
        }

        public void InitializePokemonDb()
        {
         
            List<Pokemon> pokemons = new List<Pokemon>() {
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

            //db.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT bdo.Pokemons ON;");
            foreach (Pokemon p in pokemons)
            {
                this.db.Add(p);
            }
            
            db.SaveChanges();

            pokemons[1].PreviousEvolution = pokemons[0];
            pokemons[1].NextEvolution = pokemons[2];

            pokemons[4].PreviousEvolution = pokemons[3];
            pokemons[4].NextEvolution = pokemons[5];

            pokemons[7].PreviousEvolution = pokemons[6];
            pokemons[7].NextEvolution = pokemons[8];
            

            SetPokemonParents(pokemons[1]);
            SetPokemonParents(pokemons[4]);
            SetPokemonParents(pokemons[7]);

            db.SaveChanges();



        }

        public bool Add(Pokemon newPokemon)
        {
            if (IsPokemonIdValid(newPokemon.Id))
            {
                db.Add(newPokemon);
                return true;
            }
            return false;           
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Pokemon Delete(int id)
        {
            var pokemon = GetPokemonById(id);
            if (pokemon != null)
            {
                db.Remove(pokemon);
            }
            return pokemon;
        }

        public Pokemon GetPokemonById(int id)
        {
            var query = from r in db.Pokemons
                        where r.Id == id
                        select r;
            return query.FirstOrDefault();
        }

        public IEnumerable<Pokemon> GetPokemonsByName(string name)
        {
            var query = from r in db.Pokemons
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Id
                        select r;
            return query;
        }

        public bool IsPokemonIdValid(int id)
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

        public bool IsPokemonNameValid(string name)
        {
           
            Pokemon temp = GetPokemonsByName(name).FirstOrDefault();
            if (temp == null)
            {
                return true;
            }
            return false;
        }

        public void SetPokemonParents(Pokemon pokemon)
        {
            if (pokemon.PreviousEvolution != null)
            {   
                if(pokemon.NextEvolution != null)
                {
                    GetPokemonById(pokemon.Id).NextEvolution.PreviousEvolution = null;
                    Pokemon previous = GetPokemonById(pokemon.PreviousEvolution.Id);
                    previous.NextEvolution = GetPokemonById(pokemon.Id);
                }
               
            }

            db.SaveChanges();
            if (pokemon.NextEvolution != null)
            {
                if (pokemon.PreviousEvolution != null)
                {
                    GetPokemonById(pokemon.Id).PreviousEvolution.NextEvolution = null;
                    Pokemon next = GetPokemonById(pokemon.NextEvolution.Id);
                    next.PreviousEvolution = GetPokemonById(pokemon.Id);
                }
            }
            db.SaveChanges();
        }

        public Pokemon Update(Pokemon updatedPokemon, int previousId)
        {
            var pokemon = GetPokemonById(previousId);
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

            //Good method using Entity :
            //var entity = db.Pokemons.Attach(updatedPokemon);
            //entity.State = EntityState.Modified;
            //return updatedPokemon;
        }
    }
}
