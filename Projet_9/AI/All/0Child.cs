using System;
using System.Collections.Generic;

namespace Csharp_Tpt
{
    // Fait juste une attaque al�atoire parmis celles que le pokemon a
    public class Child
    {
        private int ChoiceAttack;

        public Child(Pokemon PokemonInBattle) 
        {
            ChoiceAttack = MakeChoice(PokemonInBattle);
        }


        private int  MakeChoice(Pokemon PokemonInBattle)
        {
            Random random = new Random();
            return random.Next(0,PokemonInBattle.Moves.Count-1);
        }

        public int GetChoice()
        {
            return ChoiceAttack;
        }

    }
}