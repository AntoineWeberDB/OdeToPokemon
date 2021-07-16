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
    public class EditModel : PageModel
    {
        private readonly IPokemonData pokemonData;

        public Pokemon Pokemon { get; set; }

        public EditModel(IPokemonData pokemonData)
        {
            this.pokemonData = pokemonData;
        }

        public IActionResult OnGet(int pokemonId)
        {
            Pokemon = pokemonData.GetPokemonById(pokemonId);

            if (Pokemon == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }
    }
}