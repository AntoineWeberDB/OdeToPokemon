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

        public IActionResult OnGet(int PokemonId,InMemoryPokemonData pokemonData)
        {
            Pokemon = pokemonData.GetPokemonById(PokemonId);
            if(Pokemon == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}