using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.Pokemons
{
    public class DetailModel : PageModel
    {
        public Pokemon Pokemon { get; set; }
        public string PreviousEvolution { get; set; }
        public string NextEvolution { get; set; }

        public void OnGet(int PokemonId,InMemoryPokemonData pokemonData)
        {
            Pokemon = pokemonData.GetPokemonById(PokemonId);
            
            PreviousEvolution = "-" ;
            if (Pokemon.PreviousEvolution != null)
            {
                PreviousEvolution = Pokemon.PreviousEvolution.Name;
            }
            NextEvolution = "-" ;
            if (Pokemon.NextEvolution != null)
            {
                NextEvolution = Pokemon.NextEvolution.Name;
            } 
        }
    }
}