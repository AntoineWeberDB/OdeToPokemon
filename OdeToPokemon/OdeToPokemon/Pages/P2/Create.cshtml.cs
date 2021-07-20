using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.P2
{
    public class CreateModel : PageModel
    {
        private readonly OdeToPokemon.Data.OdeToPokemonDbContext _context;

        public CreateModel(OdeToPokemon.Data.OdeToPokemonDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pokemon Pokemon { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pokemons.Add(Pokemon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}