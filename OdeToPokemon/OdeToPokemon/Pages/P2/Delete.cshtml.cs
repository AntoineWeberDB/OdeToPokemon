using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Pages.P2
{
    public class DeleteModel : PageModel
    {
        private readonly OdeToPokemon.Data.OdeToPokemonDbContext _context;

        public DeleteModel(OdeToPokemon.Data.OdeToPokemonDbContext context)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pokemon = await _context.Pokemons.FindAsync(id);

            if (Pokemon != null)
            {
                _context.Pokemons.Remove(Pokemon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
