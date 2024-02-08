using System;
using System.Collections.Generic;
using System.Linq;
using NGlobal;

namespace NPokemon
{
    public class Newbie
    {
        private int ChoiceAttack;

        public Newbie(Pokemon PokemonInBattle, Pokemon PokemonEnemy)
        {
            ChoiceAttack = MakeChoice(PokemonInBattle, PokemonEnemy);
        }

        public static int MakeChoice(Pokemon pokemon, Pokemon pokemonEnemy)
        {
            List<float> typesMul = new List<float>();
            foreach (Attack i in pokemon.Moves)
            {
                if (i.GetPp() > 0)
                {
                    typesMul.Add(Global.Chart[(int)Global.TypeToIndex(i.GetType()), (int)Global.TypeToIndex(pokemonEnemy.GetTypes()[0])]);
                }
                else { typesMul.Add(-1); }
            }
            return typesMul.IndexOf(typesMul.Max());
        }

        public int GetChoice() { return ChoiceAttack; }

    }
}