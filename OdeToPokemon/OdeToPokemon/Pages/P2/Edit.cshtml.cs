using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.P2
{
    public class EditModel : PageModel
    {
        private readonly OdeToPokemon.Data.OdeToPokemonDbContext _context;

        public EditModel(OdeToPokemon.Data.OdeToPokemonDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pokemon Pokemon { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pokemon = await _context.Pokemons.FirstOrDefaultAsync(m => m.Name == id);

            if (Pokemon == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pokemon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonExists(Pokemon.Name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PokemonExists(string id)
        {
            return _context.Pokemons.Any(e => e.Name == id);
        }
    }
}
