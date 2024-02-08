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
        private string _name;
        private Vector2i _position { get; set; }
        private List<Pokemon> pokemons;
        public Trainer(string name) {
            _name = name;
            _position = new Vector2i();
            pokemons = new List<Pokemon>();
        }

        void AddPokemon(Pokemon pokemon)
        {
            pokemons.Add(pokemon);
        }

    }
}
