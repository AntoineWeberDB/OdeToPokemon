using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToPokemon.Core;
using OdeToPokemon.Data;

namespace OdeToPokemon.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly OdeToPokemonDbContext _context;

        public PokemonsController(OdeToPokemonDbContext context)
        {
            _context = context;
        }

        // GET: api/Pokemons
        [HttpGet]
        public IEnumerable<Pokemon> GetPokemons()
        {
            return _context.Pokemons;
        }

        // GET: api/Pokemons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemon([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemon = await _context.Pokemons.FindAsync(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        // PUT: api/Pokemons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon([FromRoute] string id, [FromBody] Pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pokemon.Name)
            {
                return BadRequest();
            }

            _context.Entry(pokemon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pokemons
        [HttpPost]
        public async Task<IActionResult> PostPokemon([FromBody] Pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokemon", new { id = pokemon.Name }, pokemon);
        }

        // DELETE: api/Pokemons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();

            return Ok(pokemon);
        }

        private bool PokemonExists(string id)
        {
            return _context.Pokemons.Any(e => e.Name == id);
        }
    }
}