using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.Pokemons
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IPokemonData pokemonData;
        private readonly ILogger<ListModel> logger;

        public string Message { get; set; }
        public IEnumerable<Pokemon> Pokemons { get; set; }
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config,IPokemonData pokemonData,ILogger<ListModel> logger)
        {
            this.config = config;
            this.pokemonData = pokemonData;
            this.logger = logger;
        }


        public void OnGet()
        {
            logger.LogError("Executing ListModel");
            Message = config["Message"];
            Pokemons = pokemonData.GetPokemonsByName(SearchTerm);
        }
    }
}