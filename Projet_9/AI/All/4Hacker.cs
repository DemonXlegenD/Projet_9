//Hacker : se refait la meme list que le joueur d�en face, fait les meilleurs choix,
//en sachant litt�ralement toutes les stats de tout le monde, tous les moves etc�

using System;
using System.Collections.Generic;
using System.Windows;
using NGlobal;

namespace NPokemon
{

    // Equilibrage des points

    // Changer de pokemon, utiliser des moves, utiliser un item
    // � voir si garder le fait d'utiliser un item

    // Faire un algo qui peut checker les actions sur plusieurs tours ->
    // Faire un truc qui permet de faire des tours

    public class Hacker
    {
        private enum states{
            SELECT, // State where he decides what to do between change pokemon or attack
            MOVES, // Choose what attack to do
            CHANGE // Choose what pokemon to change with
        }


        // Choisit entre attaquer ou select,
        // Si attack mais meurt doit choisir sinon fait juste l'attaque de la sienne
        // Si choisit et meurt, doit choisir un pokemon � anvoyer

        private List<Pokemon> pokemonsSelf;
        private List<Pokemon> pokemonsEnemy;
        private Pokemon pokemonInBattleSelf;
        private Pokemon pokemonInBattleEnemy;
        private bool[] boolArray = new bool[] { true, false };
        private string[] States = new string[] { "ATTACK", "CHANGE" };
        private List<Tuple<Pokemon, Attack>> choices = new List<Tuple<Pokemon, Attack>>(); // Liste des choix fait durant l'exploration (prends que en compte l'attaque et le pokemon pour l'instant)
        private int branchId = 0; // L'id pour chaque branche


        int SearchLenght = 5;

        // Si un pokemon est en vie +5 points, si un pokemon ennemie est mort +5 points,
        // Si un pokemon a perdu des hp -1 point, si un pokemon ennemie a perdu des hp +1 point
        // Si pokemon ennemie pas perdu hp -1 point
        // Si un pokemon meurt -5 points
        // Si il peut prendre une attaque booste -1 point, si il peut prendre max des attaques pas boost�es +2 points

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

        private int Evaluation(List<Pokemon> pokemonsEnemyTest, List<Pokemon> pokemonsSelfTest, List<Pokemon> ListPokemonSelf, List<Pokemon> ListPokemonEnemy, Pokemon PokemonSelf, Pokemon PokemonEnemy)
        {
            int Points = 0;

            int i = 0;
            foreach (Pokemon pokemon in pokemonsSelfTest)
            {
                if (pokemon.IsAlive()) { Points += 20; }
                else { Points -= 20; }

                if (pokemon.GetHp() == ListPokemonSelf[i].GetHp()) { Points++; }
                else { Points--; }

                i++;
            }
            i = 0;
            foreach (Pokemon pokemon in pokemonsEnemyTest)
            {
                if (pokemon.IsAlive()) { Points -= 20; }
                else { Points += 20; }

                if (pokemon.GetHp() == ListPokemonEnemy[i].GetHp()) { Points--; }
                else { Points++; }

                i++;
            }

            foreach (Attack x in PokemonEnemy.Moves)
            {
                if (x.GetCat() == "Physical" || x.GetCat() == "Special")
                {
                    if (Global.Chart[(int)Global.TypeToIndex(x.GetType()), (int)Global.TypeToIndex(PokemonEnemy.GetTypes()[0])] > 1)
                    {
                        Points += 5;
                        //MessageBox.Show("Attaque efficace, P:" + Points);
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
                        //MessageBox.Show("Attaque inefficace, P:"+Points);
                        i++;
                    }
                }
            }
            if (i == PokemonEnemy.Moves.Count - 1) { Points += 4; }
            i = 0;

            return Points;
        }

        private bool DoChoice(string CHOICE, Pokemon P1 = null, Pokemon P2 = null, Attack A1 = null, Attack A2 = null)
        {
            // Return true si il le pokemon n'�tait pas mort, return false si il a d�t changer de pokemon
            if (CHOICE == "ATTACK")
            {
                // fait l'attaque, si meurt doit changer de pokemon
                return true;
            }
            else if (CHOICE == "CHANGE")
            {
                // Change, tank l'attaque, si meurt apr�s attaque doit changer pokemon
                return true;
            }
            return false;
            // Si false, il fait un foreach sur chaque pokemon restant (cr�ant beaucoup de nouvelles branches)
        }

        private void DoAttack(Pokemon PAtt, Pokemon PDef, Attack Aatt,bool success,bool critical)
        {
            AttackMove(PAtt, PDef, Aatt, success, critical);
            Aatt.UseAttack();
        }

        private void AttackMove(Pokemon Att, Pokemon Def, Attack att,bool success,bool critical) 
        {
            if (success)
            {
                if (critical)
                {
                    int Damage = Global.DamageCalculator(Att, Def, att, 2);
                    Def.TakeDamage(Damage);
                }
                else
                {
                    int Damage = Global.DamageCalculator(Att, Def, att, 1);
                    Def.TakeDamage(Damage);
                }
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

            //Console.WriteLine(MiniMax(pokemonsSelfTest, pokemonsEnemyTest,5,true));
            MessageBox.Show(Evaluation(pokemonsEnemyTest, pokemonsSelfTest, pokemonsSelf, pokemonsEnemy, pokemonInBattleSelf, pokemonInBattleEnemy).ToString());
        }


        private int MiniMax(List<Pokemon> pokemonsEnemyTest, List<Pokemon> pokemonsSelfTest,Pokemon InBattleSelf,Pokemon InBattleEnemy, int depth, bool maximizingAI) 
        {
            if (depth == 0 || CheckWin(pokemonsSelfTest, pokemonsEnemyTest) != 0)
            {
                return Evaluation(pokemonsEnemyTest, pokemonsSelfTest, pokemonsSelf, pokemonsEnemy, InBattleSelf, InBattleEnemy);
            }
            // Ici c'est pour avoir la meilleur valeur, dans le else c'est le contraire
            if (maximizingAI)
            {
                int maxEval = int.MinValue;
                foreach(string i in States)
                {
                    if (i == "ATTACK") {
                        foreach (Attack move in InBattleSelf.Moves) {
                            // foreach boolarray pour prendre les sc�narios avec des critiques, avec fail
                            // Simuler l'attaque

                            // Si battle self meurt, change de pokemon
                            // Si battle enemie meurt, change de pokemon et foreach dessus

                            // appel r�cursif

                            // Refaire les listes etc sans avoir touch� 
                            // Remettre la liste et les pokemons dans l'�tat avant l'action
                        }
                    }
                    else if (i == "CHANGE")
                    {
                        foreach (Pokemon x in pokemonsSelfTest)
                        {
                            if (x != InBattleSelf && x.IsAlive())
                            {

                                // Faire le changement ici avec les d�gats

                                // foreach de boolarray pour les d�gats pour les sc�narios avec critiques, avec fail

                                // Si meurt, change de pokemon, avec un vivant 

                                // appel r�cursif

                                // Remettre la liste et les pokemons dans l'�tat avant l'action
                            }
                            // Choisir un pokemon et prendre les d�gats -> si le pokemon meurt, change de pokemon, donc foreach pokemon alive
                        }
                    }
                }
            }

            return 0;
        }


    }
}




/*        private int MiniMax(List<Pokemon> pokemonsEnemyTest, List<Pokemon> pokemonsSelfTest, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || CheckWin(pokemonsSelfTest, pokemonsEnemyTest) != 0)
            {
                // Si la profondeur maximale est atteinte ou si le jeu est termin�, retournez l'�valuation de la position actuelle.
                return Evaluation(pokemonsEnemyTest, pokemonsSelfTest, pokemonsSelf, pokemonsEnemy, pokemonInBattleSelf, pokemonInBattleEnemy);
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Pokemon selfPokemon in pokemonsSelfTest)
                {
                    foreach (Attack move in selfPokemon.Moves)
                    {
                        // Pour chaque mouvement possible de notre �quipe
                        // Simulation de l'action
                        Pokemon enemy = pokemonsEnemyTest[new Random().Next(pokemonsEnemyTest.Count)]; // Choix d'un Pok�mon ennemi au hasard pour simplifier
                        int damageDealt = Global.DamageCalculator(selfPokemon, enemy, move, 1);
                        enemy.TakeDamage(damageDealt);

                        // Appel r�cursif � MiniMax avec le prochain �tat du jeu et la profondeur r�duite
                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, depth - 1, false);

                        // Annuler l'action effectu�e pour revenir � l'�tat pr�c�dent
                        enemy.SetHp(enemy.GetMaxHp()); // Restaure les HP du Pok�mon ennemi

                        // Mise � jour de maxEval si n�cessaire
                        maxEval = Math.Max(maxEval, eval);
                    }
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Pokemon enemyPokemon in pokemonsEnemyTest)
                {
                    foreach (Attack move in enemyPokemon.Moves)
                    {
                        // Pour chaque mouvement possible de l'�quipe adverse
                        // Simulation de l'action
                        Pokemon self = pokemonsSelfTest[new Random().Next(pokemonsSelfTest.Count)]; // Choix d'un Pok�mon alli� au hasard pour simplifier
                        int damageDealt = Global.DamageCalculator(enemyPokemon, self, move, 1);
                        self.TakeDamage(damageDealt);

                        // Appel r�cursif � MiniMax avec le prochain �tat du jeu et la profondeur r�duite
                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, depth - 1, true);

                        // Annuler l'action effectu�e pour revenir � l'�tat pr�c�dent
                        self.SetHp(self.GetMaxHp()); // Restaure les HP du Pok�mon alli�

                        // Mise � jour de minEval si n�cessaire
                        minEval = Math.Min(minEval, eval);
                    }
                }
                return minEval;
            }
        }*/
