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
    public class IndexModel : PageModel
    {
        private readonly OdeToPokemon.Data.OdeToPokemonDbContext _context;

        public IndexModel(OdeToPokemon.Data.OdeToPokemonDbContext context)
        {
            _context = context;
        }

        public IList<Pokemon> Pokemon { get;set; }

        public async Task OnGetAsync()
        {
            Pokemon = await _context.Pokemons.ToListAsync();
        }
    }
}
