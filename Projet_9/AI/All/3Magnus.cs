// Pv chaque pokemon en face, moves au fur et à mesure, les types des pokemons au fur et à mesure, les potentiels moves qu’il a.
// Magnus : Il sait jouer au mieux avec toutes les infos qu’il a.

using NGlobal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace NPokemon
{
    // Regarde si le mec a l'avantage en face et évalue entre faire une attaque efficace ou changer pour un meilleur type (qui n'a pas de faiblesse contre ses attaques)
    public static class Magnus
    {
        private static int ChoiceAttack;

        private static int AttackTo(Pokemon PokemonInBattleSelf,Pokemon PokemonInBattleEnemy)
        {
            int x = 0;
            foreach (Attack a in PokemonInBattleSelf.Moves)
            {
                if (a.GetCat() == "Physical" || a.GetCat() == "Special")
                {
                    float typeMul = 0;
                    int i = 0;
                    foreach (string types in PokemonInBattleEnemy.Types)
                    {
                        typeMul += Global.Chart[(int)Global.TypeToIndex(a.GetType()), (int)Global.TypeToIndex(types)];
                        i++;
                    }
                    if (i > 1 && typeMul > 2)
                    {
                        return 1 + x;//+ X (index of the attack // Attack with this move 
                    }
                    else if (i <= 1 && typeMul > 1)
                    {
                        return 1 + x;//+ X (index of the attack // Attack with this move 
                    }
                }
                x++;
            }
            return 0;
        }

        private static int ChangeTo(List<Pokemon> PokemonListSelf,
            List<Pokemon> PokemonListEnemy,
            Pokemon PokemonInBattleSelf,
            Pokemon PokemonInBattleEnemy)
        {
            foreach (Attack a in PokemonInBattleEnemy.Moves)
            {
                if (a.GetCat() == "Physical" || a.GetCat() == "Special")
                {
                    if (Global.Chart[(int)Global.TypeToIndex(a.GetType()), (int)Global.TypeToIndex(PokemonInBattleSelf.GetTypes()[0])] > 1)
                    {
                        int y = 0;
                        foreach (Pokemon p in PokemonListSelf)
                        {
                            if (Global.Chart[(int)Global.TypeToIndex(a.GetType()), (int)Global.TypeToIndex(p.GetTypes()[0])] < 1 && p != PokemonInBattleSelf)
                            {
                                return -1 - y;
                            }
                            y++;
                        }
                    }
                }
            }
            return -1;
        }

        public static int MakeChoice(
            List<Pokemon> PokemonListSelf,
            List<Pokemon> PokemonListEnemy,
            Pokemon PokemonInBattleSelf,
            Pokemon PokemonInBattleEnemy
            )
        {
            AttackTo(PokemonInBattleSelf, PokemonInBattleEnemy);
            ChangeTo(PokemonListSelf,PokemonListEnemy,PokemonInBattleSelf,PokemonInBattleEnemy);

            if (AttackTo(PokemonInBattleSelf, PokemonInBattleEnemy) > 0 )
            {
                return AttackTo(PokemonInBattleSelf, PokemonInBattleEnemy);
            }
            if (AttackTo(PokemonInBattleSelf, PokemonInBattleEnemy) == 0 && ChangeTo(PokemonListSelf, PokemonListEnemy, PokemonInBattleSelf, PokemonInBattleEnemy) < -1)
            {
                return ChangeTo(PokemonListSelf, PokemonListEnemy, PokemonInBattleSelf, PokemonInBattleEnemy);
            }

            return 0;

        }

    }
}