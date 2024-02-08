using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;
using NPokemon;

namespace NTrainer
{
    public class Trainer
    {

        public string Id { get; set; }
        public string Name;
        public Vector2i Position { get; set; }
        public List<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public Trainer(string id, string name, Vector2i position, List<Pokemon> pokemons)
        {
            Id = id;
            Name = name;
            Position = position;
            Pokemons = pokemons;
        }

        void AddPokemon(Pokemon pokemon)
        {
            Pokemons.Add(pokemon);
        }

    }
}
