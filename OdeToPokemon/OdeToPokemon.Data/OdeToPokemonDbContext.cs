using Microsoft.EntityFrameworkCore;
using OdeToPokemon.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToPokemon.Data
{
    public class OdeToPokemonDbContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }

        public OdeToPokemonDbContext(DbContextOptions<OdeToPokemonDbContext> options)
            :base(options)
        { 
        }
    }

}
