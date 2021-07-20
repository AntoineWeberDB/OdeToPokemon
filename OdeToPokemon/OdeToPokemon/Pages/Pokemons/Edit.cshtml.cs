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
        
        public IList<SelectListItem> PokemonsListPrevious { get; set; }
        public IList<SelectListItem> PokemonsListNext { get; set; }
        public IList<SelectListItem> PokemonTypes { get; set; }

        public EditModel(IPokemonData pokemonData, IHtmlHelper htmlHelper)
        {
            this.pokemonData = pokemonData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? pokemonId)
        {
            TempData["newPokemon"] = false;
            if (pokemonId.HasValue)
            {
                Pokemon = pokemonData.GetPokemonById(pokemonId.Value);
                if(Pokemon.PreviousEvolution != null)
                {
                    PokemonPreviousId = Pokemon.PreviousEvolution.Id.ToString();
                } else
                {
                    PokemonPreviousId = "0";
                }
                if (Pokemon.NextEvolution != null)
                {
                    PokemonNextId = Pokemon.NextEvolution.Id.ToString();
                }
                else
                {
                    PokemonNextId = "0";
                }

            }

            EditPageSetup();

            if (!pokemonId.HasValue)
            {
                Pokemon = new Pokemon();
                TempData["newPokemon"] = true;
                return Page();
            }

            if (Pokemon == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["previousId"] = pokemonId;
            return Page();
        }

        public IActionResult OnPost()
        {
            
            Pokemon.PreviousEvolution = pokemonData.GetPokemonById(Int32.Parse(PokemonPreviousId));
            Pokemon.NextEvolution = pokemonData.GetPokemonById(Int32.Parse(PokemonNextId));

            Boolean newPokemon = false;
            if (TempData["newPokemon"] != null)
            {
                newPokemon = (Boolean)TempData["newPokemon"];
            }
            TempData["newPokemon"] = newPokemon;

            if (!ModelState.IsValid)
            {
                EditPageSetup();
                return Page();
            }
            
            if (!newPokemon)
            {
                
                if (pokemonData.IsPokemonIdValid(Pokemon.Id) || Pokemon.Id == (int)TempData["previousId"])
                {
                    TempData["Message"] = "Pokemon Updated ! ";
                    pokemonData.Update(Pokemon, (int)TempData["previousId"]);
                    pokemonData.Commit();
                    return RedirectToPage("./Detail", new { pokemonId = Pokemon.Id });
                } else
                {
                    TempData.Keep("previousId");
                    TempData["ErrorMessage"] = "Error : Pokemon Id Already Taken ! ";
                    EditPageSetup();
                    return Page();
                }
            }
                        
            Boolean added = pokemonData.Add(Pokemon);
            pokemonData.Commit();
            if (added)
            {
                TempData["Message"] = "Pokemon Added ! ";
                return RedirectToPage("./Detail", new { pokemonId = Pokemon.Id });
            } else
            {   
                TempData["ErrorMessage"] = "Error : Pokemon Id Already Taken ! ";
                TempData.Keep("previousId");
                EditPageSetup();
                return Page();
            }
        }

        public void EditPageSetup()
        {
            PokemonTypes = htmlHelper.GetEnumSelectList<PokemonType>().ToList();
            IEnumerable<Pokemon> AllPokemonsList = pokemonData.GetPokemonsByName("");

            PokemonsListPrevious = AllPokemonsList.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.Id.ToString(),
                                     Text = a.Id + " - " + a.Name
                                 }).ToList();

            PokemonsListPrevious.Insert(0, (new SelectListItem
            {
                Text = "-",
                Value = "0"
            }));



            PokemonsListNext = AllPokemonsList.Select(a =>
                               new SelectListItem
                               {
                                   Value = a.Id.ToString(),
                                   Text = a.Id + " - " + a.Name
                               }).ToList();

            PokemonsListNext.Insert(0, (new SelectListItem
            {
                Text = "-",
                Value = "0"
            }));

        }
    }
}