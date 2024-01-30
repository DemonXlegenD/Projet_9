using System;
using System.Collections.Generic;

namespace Csharp_Tpt
{
    // Fait juste une attaque aléatoire parmis celles que le pokemon a
    public class Child
    {
        private Pokemon pokemon;
        private int ChoiceAttack;

        public Child(Pokemon PokemonInBattle) 
        {
            pokemon = PokemonInBattle;
            ChoiceAttack = MakeChoice();
        }


        int  MakeChoice()
        {
            Random random = new Random();
            return random.Next(0,pokemon.Moves.Count-1);
        }

        int GetChoice()
        {
            return ChoiceAttack;
        }

    }
}