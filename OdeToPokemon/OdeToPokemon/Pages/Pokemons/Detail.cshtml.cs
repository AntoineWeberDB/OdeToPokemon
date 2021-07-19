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
        private readonly IPokemonData pokemonData;

        [TempData]
        public string Message { get; set; }

        public DetailModel(IPokemonData pokemonData)
        {
            this.pokemonData = pokemonData;
        }

        public IActionResult OnGet(int PokemonId)
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