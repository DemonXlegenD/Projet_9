//Hacker : se refait la meme list que le joueur d’en face, fait les meilleurs choix,
//en sachant littéralement toutes les stats de tout le monde, tous les moves etc…

using System;
using System.Collections.Generic;

namespace NPokemon
{

    // Changer de pokemon, utiliser des moves, utiliser un item
    // à voir si garder le fait d'utiliser un item

    // Faire un algo qui peut checker les actions sur plusieurs tours ->
    // Faire un truc qui permet de faire des tours

    public class Hacker
    {
        private enum states{
            SELECT, // State where he decides what to do between change pokemon or attack
            MOVES, // Choose what attack to do
            CHANGE // Choose what pokemon to change with
        }


        private List<Pokemon> pokemonsSelf;
        private List<Pokemon> pokemonsEnemy;
        private Pokemon pokemonInBattleSelf;
        private Pokemon pokemonInBattleEnemy;

        int SearchLenght = 5;

        // Si un pokemon est en vie +5 points, si un pokemon ennemie est mort +5 points,
        // Si un pokemon a perdu des hp -1 point, si un pokemon ennemie a perdu des hp +1 point
        // Si pokemon ennemie pas perdu hp -1 point
        // Si un pokemon meurt -5 points
        // Si il peut prendre une attaque booste -1 point, si il peut prendre max des attaques pas boostées +2 points

        private Pokemon GetPokemonWithString(string name,List<Pokemon> PokemonList)
        {
            foreach (var pokemon in PokemonList)
            {
                if (pokemon.GetName() == name)
                {
                    return pokemon;
                }
            }
            return null;
        }

        private Pokemon ChangePokemonInBattle(List<Pokemon> List,Pokemon PokemonEnemy,Attack AttackEnemy,int x)
        {
            List[x].TakeDamage(Global.DamageCalculator(List[x], PokemonEnemy, AttackEnemy, 1));
            return List[x];
        }

        private int Evaluation(
            List<Pokemon> pokemonsEnemyTest, 
            List<Pokemon> pokemonsSelfTest, 
            List<Pokemon> ListPokemonSelf, 
            List<Pokemon> ListPokemonEnemy, 
            Pokemon PokemonSelf, 
            Pokemon PokemonEnemy
            )
        {
            int Points = 0;

            // Les +1 etc doit etre chiffres random pour que ça fasse une IA qui doit etre forte, après faut faire des generations etc...

            // Si un pokemon est en vie +5 points, si un pokemon ennemie est mort +5 points,
            // Si un pokemon a perdu des hp -1 point, si un pokemon ennemie a perdu des hp +1 point
            // Si pokemon ennemie pas perdu hp -1 point
            // Si un pokemon meurt -5 points
            // Si il peut prendre une attaque booste -1 point, si il peut prendre max des attaques pas boostées +2 points

            int i = 0;
            foreach (Pokemon pokemon in pokemonsSelfTest)
            {
                if (pokemon.IsAlive()) { Points += 5; }
                else { Points -= 5; }

                if (pokemon.GetHp() == ListPokemonSelf[i].GetHp()) { Points++; } 
                else { Points --; }

                i++;
            }
            i = 0;
            foreach (Pokemon pokemon in pokemonsEnemy)
            {
                if (pokemon.IsAlive()) { Points -= 5; }
                else { Points += 5; }

                if (pokemon.GetHp() == ListPokemonSelf[i].GetHp()) { Points--; }
                else { Points++; }

                i++;
            }

            foreach (Attack x in PokemonEnemy.Moves)
            {
                if ( x.GetCat() == "Physical" || x.GetCat() == "Special" )
                {
                    if (Global.Chart[(int)Global.TypeToIndex(x.GetType()), (int)Global.TypeToIndex(PokemonEnemy.GetTypes()[0])] > 1)
                    {
                        Points -= 2;
                    }
                }
            }
            i = 0;
            foreach (Attack x in PokemonEnemy.Moves)
            {
                if (x.GetCat() == "Physical" || x.GetCat() == "Special")
                {
                    if (Global.Chart[(int)Global.TypeToIndex(x.GetType()), (int)Global.TypeToIndex(PokemonEnemy.GetTypes()[0])] >= 1)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            if (i == PokemonEnemy.Moves.Count - 1) { Points+= 4; }
            i = 0;

            return Points;
        }

        private void DoMove(Pokemon PAtt, Pokemon PDef, Attack Aatt)
        {

            Aatt.UseAttack();
        }

        private void AttackMove(Pokemon Att, Pokemon Def, Attack att) 
        {
            if (Global.SuccessAcc(att.GetAcc()))
            {
                int Damage = Global.DamageCalculator(Att, Def, att, 1);
                Def.TakeDamage(Damage);
            }
        }

        private void CheckDeath(List<Pokemon> ListSelf,List<Pokemon> ListEnemy,Pokemon PokemonSelf, Pokemon PokemonEnemy)
        {
            if (!PokemonSelf.IsAlive() && CheckWin(ListSelf,ListEnemy) == 0)
            {
                // Make the choice to send who he thinks
            }
            if (!PokemonEnemy.IsAlive() && CheckWin(ListSelf, ListEnemy) == 0)
            {
                // The id of the path ? ou random ou best pokemon contre
                // Change to the pokemon he choose
            }
        }

        private bool CheckTeam(List<Pokemon> List)
        {
            foreach(Pokemon x in List)
            {
                if (x.IsAlive()) { return false; }
            }
            return true;
        }

        private int CheckWin(List<Pokemon> ListSelf, List<Pokemon> ListEnemy)
        {
            if (CheckTeam(ListSelf)) { return -1; }
            if (CheckTeam(ListEnemy)) { return 1; }
            return 0;
        }


        public Hacker(List<Pokemon> PokemonListSelf, 
            List<Pokemon> PokemonListEnemy,
            string PokemonInBattleSelf, 
            string PokemonInBattleEnemy
        ) 
        {
            pokemonsSelf = new List<Pokemon>(PokemonListSelf);
            pokemonsEnemy = new List<Pokemon>(PokemonListEnemy);

            pokemonInBattleSelf = GetPokemonWithString(PokemonInBattleSelf,PokemonListSelf);
            pokemonInBattleEnemy = GetPokemonWithString(PokemonInBattleEnemy, PokemonListEnemy);

            // Actions :
            // Change Pokemon -> Take Damage on the new pokemon
            // Attack -> (Do damage and Take damage) or (Take damage and Do damage) 
            // Check between attacks if he's dead, if yes, has to change for a pokemon, then starts an other round

            List<Pokemon> pokemonsSelfTest = new List<Pokemon>(PokemonListSelf);
            List<Pokemon> pokemonsEnemyTest = new List<Pokemon>(PokemonListEnemy);



        }

    }
}