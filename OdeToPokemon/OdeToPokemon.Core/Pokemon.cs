using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OdeToPokemon.Core
{
    public class Pokemon
    {
        [Required]
        [Range(1,151,ErrorMessage ="Please enter a valid Pokedex ID (1-151)")]
        public int Id { get; set; }
        [Key]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public PokemonType Type { get; set; }
        [Display(Name = "Previous Evolution")]
        [JsonIgnore]
        public Pokemon PreviousEvolution { get; set; } = null;
        [Display(Name = "Next Evolution")]
        //[JsonIgnore]
        public Pokemon NextEvolution { get; set; } = null;

        public Pokemon()
        {
        }
    }
}
