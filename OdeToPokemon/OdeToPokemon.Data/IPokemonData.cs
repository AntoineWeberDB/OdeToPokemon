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
        void SetPokemonParents(Pokemon pokemon);
        Pokemon Update(Pokemon updatedPokemon,int previousId);
        Boolean Add(Pokemon newPokemon);
        Pokemon Delete(int id);
        Boolean IsPokemonIdValid(int id);
        int GetCountOfPokemons();
        int Commit();
    }

}
