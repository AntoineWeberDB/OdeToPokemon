using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.Pokemons
{
    public class EditModel : PageModel
    {
        private readonly IPokemonData pokemonData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Pokemon Pokemon { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PokemonPreviousId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PokemonNextId { get; set; }
        public IEnumerable<SelectListItem> PokemonsListPrevious { get; set; }
        public IEnumerable<SelectListItem> PokemonsListNext { get; set; }
        public IEnumerable<SelectListItem> PokemonTypes { get; set; }

        public EditModel(IPokemonData pokemonData, IHtmlHelper htmlHelper)
        {
            this.pokemonData = pokemonData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int pokemonId)
        {
            Pokemon = pokemonData.GetPokemonById(pokemonId);
            PokemonPreviousId = Pokemon.PreviousEvolution.Id.ToString();
            PokemonNextId = Pokemon.NextEvolution.Id.ToString();
            


            PokemonTypes = htmlHelper.GetEnumSelectList<PokemonType>();
            IEnumerable<Pokemon> AllPokemonsList = pokemonData.GetPokemonsByName("");

            PokemonsListPrevious = AllPokemonsList.Select(a =>
                                 new SelectListItem
                                 {   Value = a.Id.ToString(),
                                     Text = a.Id + " - " + a.Name
                                 }).ToList();
            

            PokemonsListNext = AllPokemonsList.Select(a =>
                               new SelectListItem
                               {
                                   Value = a.Id.ToString(),
                                   Text = a.Id + " - " + a.Name
                               }).ToList();


            if (Pokemon == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            



            Pokemon.PreviousEvolution = pokemonData.GetPokemonById(Int32.Parse(PokemonPreviousId));
            Pokemon.NextEvolution = pokemonData.GetPokemonById(Int32.Parse(PokemonNextId));

            if (ModelState.IsValid) { 

                pokemonData.Update(Pokemon);
                pokemonData.Commit();

                return RedirectToPage("./List");

            } else
            {
                PokemonTypes = htmlHelper.GetEnumSelectList<PokemonType>();
                IEnumerable<Pokemon> AllPokemonsList = pokemonData.GetPokemonsByName("");

                PokemonsListPrevious = AllPokemonsList.Select(a =>
                                     new SelectListItem
                                     {
                                         Value = a.Id.ToString(),
                                         Text = a.Id + " - " + a.Name
                                     }).ToList();

                PokemonsListNext = AllPokemonsList.Select(a =>
                                   new SelectListItem
                                   {
                                       Value = a.Id.ToString(),
                                       Text = a.Id + " - " + a.Name
                                   }).ToList();
                return Page();
            }




           
        }
    }
}