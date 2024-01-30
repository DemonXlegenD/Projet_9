using System;
using System.Collections.Generic;
using System.Linq;

namespace Csharp_Tpt
{
    // Fait littéralement la pire attaque contre, calcul les dégats de chaque attaque et prends la pire
    public class NoteUltime
    {
        private int ChoiceAttack;

        public NoteUltime(Pokemon PokemonInBattle,Pokemon PokemonEnemy) 
        {
            ChoiceAttack = MakeChoice(PokemonInBattle, PokemonEnemy);
        }

        private int MakeChoice(Pokemon pokemon, Pokemon pokemonEnemy)
        {
            List<int> damages = new List<int>();
            foreach (Attack i in pokemon.Moves)
            {
                if(i.GetCat() == "Physical" || i.GetCat() == "Special")
                {
                    if (i.GetPp() > 0)
                    {
                        damages.Add(Global.DamageCalculator(pokemon, pokemonEnemy, i, 1));
                    }
                    else
                    {
                        damages.Add(1000);
                    }
                }
                else
                {
                    damages.Add(1000);
                }
            }
            return damages.IndexOf(damages.Min());
        }

        public int GetChoice() { return ChoiceAttack; }

    }
}