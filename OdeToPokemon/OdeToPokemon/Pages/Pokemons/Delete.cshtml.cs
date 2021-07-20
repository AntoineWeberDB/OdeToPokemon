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
    public class DeleteModel : PageModel
    {
        private readonly IPokemonData pokemonData;
               
        [ModelBinder]
        public Pokemon Pokemon { get; set; }
        public DeleteModel(IPokemonData pokemonData)
        {
            this.pokemonData = pokemonData;
        }
        public IActionResult OnGet(int pokemonId)
        {
            Pokemon = pokemonData.GetPokemonById(pokemonId);
            if(Pokemon == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int pokemonId)
        {
            var pokemon = pokemonData.Delete(pokemonId);
            pokemonData.Commit();
            if (pokemon != null)
            {
                TempData["Message"] = $"Le Pokémon {pokemon.Id} - {pokemon.Name} a bien été supprimé";
            }
            else
            {
                TempData["ErrorMessage"] = $"Désolé, nous n'avons pas trouvé de pokémon avec l'Id {pokemon.Id} ";
            }
            return RedirectToPage("./List");
        }
    }
}