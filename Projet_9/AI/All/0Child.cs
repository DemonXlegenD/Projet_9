using System;
using System.Collections.Generic;
using NPokemon;

namespace NPokemon
{
    // Fait juste une attaque aléatoire parmis celles que le pokemon a
    public class Child
    {
        private int ChoiceAttack;

        public Child(Pokemon PokemonInBattle) {ChoiceAttack = MakeChoice(PokemonInBattle);}

        public static int  MakeChoice(Pokemon PokemonInBattle)
        {
            Random random = new Random();
            return random.Next(0, PokemonInBattle.Moves.Count-1);
}

        public int GetChoice(){return ChoiceAttack;}

    }
}