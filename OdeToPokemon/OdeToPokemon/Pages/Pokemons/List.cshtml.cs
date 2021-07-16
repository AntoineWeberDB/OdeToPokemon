using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.Pokemons
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IPokemonData pokemonData;

        public string Message { get; set; }
        public IEnumerable<Pokemon> Pokemons { get; set; }
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config,IPokemonData pokemonData)
        {
            this.config = config;
            this.pokemonData = pokemonData;
        }


        public void OnGet()
        {   
            
            Message = config["Message"];
            Pokemons = pokemonData.GetPokemonsByName(SearchTerm);
        }
    }
}