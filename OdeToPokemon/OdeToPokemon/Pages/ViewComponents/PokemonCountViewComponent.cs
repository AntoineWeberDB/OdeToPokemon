using Microsoft.AspNetCore.Mvc;
using OdeToPokemon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToPokemon.Pages.ViewComponents
{
    public class PokemonCountViewComponent : ViewComponent
    {
        private readonly IPokemonData pokemonData;
        
        public PokemonCountViewComponent(IPokemonData pokemonData)
        {
            this.pokemonData = pokemonData;

        }

        public IViewComponentResult Invoke()
        {
            var count = pokemonData.GetCountOfPokemons();
            return View(count);
        }

    }
}
