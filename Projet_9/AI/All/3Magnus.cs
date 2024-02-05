// Pv chaque pokemon en face, moves au fur et à mesure, les types des pokemons au fur et à mesure, les potentiels moves qu’il a.
// Magnus : Il sait jouer au mieux avec toutes les infos qu’il a.

using System;

namespace NPokemon
{
    // Regarde si le mec a l'avantage en face et évalue entre faire une attaque efficace ou changer pour un meilleur type (qui n'a pas de faiblesse contre ses attaques)
    public class Magnus
    {
        private int ChoiceAttack;

        public Magnus(Pokemon InBattleSelf) 
        { 
            ChoiceAttack = MakeChoice(InBattleSelf); 
        }

        private int MakeChoice(Pokemon PokemonInBattle)
        {
            Random random = new Random();
            return random.Next(0, PokemonInBattle.Moves.Count - 1);
        }

        public int GetChoice() { return ChoiceAttack; }

    }
}