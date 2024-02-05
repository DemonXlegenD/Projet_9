//Hacker : se refait la meme list que le joueur d�en face, fait les meilleurs choix,
//en sachant litt�ralement toutes les stats de tout le monde, tous les moves etc�

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using NGlobal;
using static NPokemon.Hacker;

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
        private bool[] boolArray = new bool[] { false,true};
        private string[] States = new string[] { "ATTACK", "CHANGE" };
        private List<Tuple<Pokemon, Attack>> choices = new List<Tuple<Pokemon, Attack>>(); // Liste des choix fait durant l'exploration (prends que en compte l'attaque et le pokemon pour l'instant)
        private int branchId = 0; // L'id pour chaque branche


        int SearchLenght = 3; // En 3 tours tue tout le monde

        // Si un pokemon est en vie +5 points, si un pokemon ennemie est mort +5 points,
        // Si un pokemon a perdu des hp -1 point, si un pokemon ennemie a perdu des hp +1 point
        // Si pokemon ennemie pas perdu hp -1 point
        // Si un pokemon meurt -5 points
        // Si il peut prendre une attaque booste -1 point, si il peut prendre max des attaques pas boost�es +2 points

        public Hacker(List<Pokemon> PokemonListSelf, 
            List<Pokemon> PokemonListEnemy,
            string PokemonInBattleSelf, 
            string PokemonInBattleEnemy,
            bool MaximizingAI = true
        ) 
        {
            pokemonsSelf = new List<Pokemon>(PokemonListSelf);
            pokemonsEnemy = new List<Pokemon>(PokemonListEnemy);

            // Actions :
            // Change Pokemon -> Take Damage on the new pokemon
            // Attack -> (Do damage and Take damage) or (Take damage and Do damage) 
            // Check between attacks if he's dead, if yes, has to change for a pokemon, then starts an other round

            List<Pokemon> pokemonsSelfTest = new List<Pokemon>(PokemonListSelf);
            List<Pokemon> pokemonsEnemyTest = new List<Pokemon>(PokemonListEnemy);

            pokemonInBattleSelf = FirstPokemon(pokemonsSelfTest);
            pokemonInBattleEnemy = FirstPokemon(pokemonsEnemyTest);

            //Console.WriteLine(MiniMax(pokemonsSelfTest, pokemonsEnemyTest,5,true));
            //List<string> chosenActions = new List<string>();

            List<Branch>  branches = new List<Branch>();
            Branch initialBranch = new Branch(0);
            branches.Add(initialBranch);


            // Le pokemon de l'IA fait plus de 1 attaque par branche ? 
            MessageBox.Show(MiniMax(pokemonsEnemyTest, pokemonsSelfTest, pokemonInBattleSelf, pokemonInBattleEnemy, SearchLenght, MaximizingAI, 0, branches).ToString());
            // Print the chosen actions
            /*            MessageBox.Show("Chosen Actions:");
                        foreach (string action in chosenActions)
                        {
                            MessageBox.Show(action);
                        }*/
            int maxEval = int.MinValue;
            foreach(Branch branch in branches)
            {
                maxEval = Math.Max(maxEval, branch.EvalValue);
            }
            foreach(Branch branch in branches)
            {
                if(branch.EvalValue == maxEval)
                {
                    foreach(string action in branch.Actions)
                    {
                        MessageBox.Show(action);
                    }
                }
            }
        }

        private Pokemon FirstPokemon(List<Pokemon> List)
        {
            foreach (Pokemon poke in List)
            {
                if (poke.IsAlive())
                {
                    return poke;
                }
            }
            return List[0];
        }

        private Pokemon GetPokemonWithString(string name, List<Pokemon> PokemonList)
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

        private Pokemon ChangePokemonInBattle(List<Pokemon> List, Pokemon pokemon, Attack attack, int x)
        {
            List[x].TakeDamage(Global.DamageCalculator(List[x], pokemon, attack, 1));
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

        private void DoAttack(Pokemon PAtt, Pokemon PDef, Attack Aatt, bool success, bool critical)
        {
            AttackMove(PAtt, PDef, Aatt, success, critical);
            Aatt.UseAttack();
        }

        private void AttackMove(Pokemon Att, Pokemon Def, Attack att, bool success, bool critical)
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

        private void RestoreHpAttack(Pokemon Att, Pokemon Def, Attack att, bool success, bool critical)
        {
            if (success)
            {
                if (critical)
                {
                    int Damage = Global.DamageCalculator(Att, Def, att, 2);
                    Def.Hp += Damage;
                }
                else
                {
                    int Damage = Global.DamageCalculator(Att, Def, att, 1);
                    Def.Hp += Damage;
                }
            }
        }

        private void CheckDeath(List<Pokemon> ListSelf, List<Pokemon> ListEnemy, Pokemon PokemonSelf, Pokemon PokemonEnemy)
        {
            if (!PokemonSelf.IsAlive() && CheckWin(ListSelf, ListEnemy) == 0)
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
            foreach (Pokemon x in List)
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

        private void ResetList(List<Pokemon> L1, List<Pokemon> L1Reset, List<Pokemon> L2, List<Pokemon> L2Reset)
        {
            L1 = L1Reset;
            L2 = L2Reset;
        }

        private void ResetUniqueList(List<Pokemon> L1, List<Pokemon> L1Reset)
        {
            L1 = L1Reset;
        }

        // Mise � jour de maxEval si n�cessaire
        //maxEval = Math.Max(maxEval, eval);


        // Tue les 5 pokemons en 2 tours ??????
        // Enfaite le meilleur chemin sera le cas, o� le player change en boucle, comment �viter �a ?

        public class Branch
        {
            public int ID { get; set; }
            public List<string> Actions { get; set; }
            public int EvalValue { get; set; }

            public Branch(int id)
            {
                ID = id;
                Actions = new List<string>();
                EvalValue = 0; // Set the initial evaluation value as needed
            }
        }

        private int MiniMax(List<Pokemon> pokemonsEnemyTest, List<Pokemon> pokemonsSelfTest,Pokemon InBattleSelf,Pokemon InBattleEnemy, int depth, bool maximizingAI, int branchID, List<Branch> branches) 
        {
            if (depth <= 0 || CheckWin(pokemonsSelfTest, pokemonsEnemyTest) != 0)
            {
                if(CheckWin(pokemonsSelfTest, pokemonsEnemyTest) != 0)
                {
                    branches[branchID].EvalValue = Evaluation(pokemonsEnemyTest, pokemonsSelfTest, pokemonsSelf, pokemonsEnemy, InBattleSelf, InBattleEnemy) + 10000;
                    return branches[branchID].EvalValue;
                }
                branches[branchID].EvalValue = Evaluation(pokemonsEnemyTest, pokemonsSelfTest, pokemonsSelf, pokemonsEnemy, InBattleSelf, InBattleEnemy);
                return branches[branchID].EvalValue;
            }
            // Ici c'est pour avoir la meilleur valeur, dans le else c'est le contraire
            if (maximizingAI)
            {
                int maxEval = int.MinValue;
                List<Pokemon> enemylistnew = new List<Pokemon>(pokemonsEnemyTest);
                List<Pokemon> selflistnew = new List<Pokemon>(pokemonsSelfTest);
                foreach (string i in States) // IA STATE
                {
                    if (i == "ATTACK") {
                        foreach (Attack move in InBattleSelf.Moves) { // foreach Move of the AI
                            //chosenActions.Add($"AI_ATTACK_{move.GetName()}");
                            // Foreach de states si le mec en face change de pokemon
                            foreach (string j in States) // PLAYER STATE
                            {
                                if (i == "ATTACK")
                                {
                                    List<Pokemon> LS1 = new List<Pokemon>(pokemonsSelfTest);
                                    List<Pokemon> LE1 = new List<Pokemon>(pokemonsEnemyTest);
                                    foreach (Attack attack in InBattleEnemy.Moves)
                                    {
                                        if (InBattleEnemy.Speed > InBattleSelf.Speed)
                                        {
                                            foreach (bool k in boolArray) // Fail acc ou pas
                                            {
                                                foreach (bool l in boolArray) // Fail crit ou pas
                                                {
                                                    AttackMove(InBattleEnemy, InBattleSelf, attack, k, l);
                                                    List<Pokemon> LE2 = new List<Pokemon>(pokemonsSelfTest);
                                                    if (!InBattleSelf.IsAlive())
                                                    {
                                                        List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                                        foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                        {
                                                            if (poke.IsAlive() && InBattleSelf != poke)
                                                            {
                                                                InBattleSelf = poke;
                                                                //chosenActions.Add($"AI_CHANGE_{poke.GetName()}");
                                                                // appel r�cursif
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID,branches);
                                                                maxEval = Math.Max(maxEval, eval);
                                                                Branch newBranch = new Branch(branchID); // Create a new branch for each action
                                                                newBranch.Actions.Add($"AIATTACK{move.GetName()} && Died AICHANGE{poke.GetName()}");
                                                                newBranch.EvalValue = eval;
                                                                branches.Add(newBranch);
                                                                // Reset Lists at previous states
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                            }
                                                            ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE2);
                                                            }
                                                        }
                                                    }
                                                    List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                    List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);
                                                    foreach (bool m in boolArray) // Fail acc ou pas
                                                    {
                                                        foreach (bool n in boolArray) // Fail crit ou pas
                                                        {
                                                            AttackMove(InBattleSelf, InBattleEnemy, move, m, n);
                                                            List<Pokemon> LS4 = new List<Pokemon>(pokemonsSelfTest);
                                                            List<Pokemon> LE4 = new List<Pokemon>(pokemonsEnemyTest);
                                                            if (!InBattleEnemy.IsAlive())
                                                            {
                                                                foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                                {
                                                                    if (poke.IsAlive() && InBattleEnemy != poke)
                                                                    {
                                                                        InBattleEnemy = poke;
                                                                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                        maxEval = Math.Max(maxEval, eval);
                                                                        //chosenActions.RemoveAt(chosenActions.Count - 1);    

                                                                        ResetList(pokemonsSelfTest, LS4, pokemonsEnemyTest, LE4);
                                                                        // appel r�cursif
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                maxEval = Math.Max(maxEval, eval);
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                // appel r�cursif
                                                            }

                                                        }
                                                    }
                                                }
                                            
                                        }
                                        else
                                        {
                                            foreach (bool k in boolArray) // Fail acc ou pas
                                            {
                                                foreach (bool l in boolArray) // Fail crit ou pas
                                                {
                                                    Branch newBranch = new Branch(branchID); // Create a new branch for each action
                                                    newBranch.Actions.Add($"AIATTACK{move.GetName()}");
                                                    branches.Add(newBranch);
                                                    List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                    List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);

                                                    AttackMove(InBattleSelf, InBattleEnemy, move, k, l);
                                                    if (!InBattleEnemy.IsAlive())
                                                    {
                                                        foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                        {
                                                            if (poke.IsAlive() && InBattleEnemy != poke)
                                                            {
                                                                InBattleEnemy = poke;
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                maxEval = Math.Max(maxEval, eval);
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                // appel r�cursif
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        foreach (bool m in boolArray) // Fail acc ou pas
                                                        {
                                                            foreach (bool n in boolArray) // Fail crit ou pas
                                                            {
                                                                AttackMove(InBattleEnemy, InBattleSelf, attack, m, n);
                                                                if (!InBattleSelf.IsAlive())
                                                                {
                                                                    List<Pokemon> LE4 = new List<Pokemon>(pokemonsEnemyTest);
                                                                    List<Pokemon> LS4 = new List<Pokemon>(pokemonsSelfTest);
                                                                    foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                                    {
                                                                        if (poke.IsAlive() && InBattleSelf != poke)
                                                                        {
                                                                            //chosenActions.Add($"AI_CHANGE_{poke.GetName()}");
                                                                            InBattleSelf = poke;
                                                                            int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                            maxEval = Math.Max(maxEval, eval2);
                                                                            Branch newBranch2 = new Branch(branchID); // Create a new branch for each action
                                                                            newBranch.Actions.Add($"AIATTACK{move.GetName()} && Died AICHANGE{poke.GetName()}");
                                                                            newBranch.EvalValue = eval2;
                                                                            branches.Add(newBranch);
                                                                            // appel r�cursif
                                                                            //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                            // Reset Lists at previous states
                                                                            ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                        }
                                                                    }
                                                                }
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                maxEval = Math.Max(maxEval, eval);
                                                                Branch newBranch3 = new Branch(branchID); // Create a new branch for each action
                                                                newBranch.Actions.Add($"AIATTACK{move.GetName()}");
                                                                newBranch.EvalValue = eval;
                                                                branches.Add(newBranch);
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                // appel r�cursif
                                                            }
                                                        }
                                                    }


                                                }
                                            }

                                        }
                                    }
                                }
                                else if (i == "CHANGE")
                                {
                                    List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                    List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);
                                    Pokemon InBattleE = LE3[LE3.IndexOf(InBattleEnemy)];
                                    foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                    {
                                        if (poke.IsAlive() && InBattleEnemy != poke) 
                                        {

                                            InBattleEnemy = poke; // Remttre le pokemon avant les trucs
                                            foreach (bool k in boolArray) // Fail acc ou pas
                                            {
                                                foreach (bool l in boolArray) // Fail crit ou pas
                                                {
                                                    AttackMove(InBattleSelf, InBattleEnemy, move, k, l);
                                                    if (!InBattleEnemy.IsAlive())
                                                    {
                                                        Pokemon InBattleE2 = LE3[LE3.IndexOf(InBattleEnemy)];
                                                        foreach (Pokemon en in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                        {
                                                            if (en.IsAlive() && InBattleEnemy != en)
                                                            {
                                                                InBattleEnemy = en;
                                                                int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                maxEval = Math.Max(maxEval, eval2);
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                                
                                                                // appel r�cursif
                                                            }
                                                            InBattleEnemy = InBattleE2;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                        maxEval = Math.Max(maxEval, eval);
                                                        //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                        ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                    }
                                                    RestoreHpAttack(InBattleSelf, InBattleEnemy, move, k, l);
                                                }
                                            }

                                        }
                                        InBattleEnemy = InBattleE;
                                    }
                                }
                            }
                        }
                    }

                    else if (i == "CHANGE")
                    {
                        foreach (Pokemon x in pokemonsSelfTest)
                        {
                            //chosenActions.Add($"AI_CHANGE_{x.GetName()}");
                            if (x != InBattleSelf && x.IsAlive())
                            {
                                // Foreach de states si le mec en face change de pokemon
                                foreach (string j in States) // PLAYER STATE
                                {
                                    if (i == "ATTACK")
                                    {
                                        List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                        List<Pokemon> LE2 = new List<Pokemon>(pokemonsEnemyTest);
                                        foreach (Attack attack in InBattleEnemy.Moves)
                                        {
                                            foreach (bool m in boolArray) // Fail acc ou pas
                                            {
                                                foreach (bool n in boolArray) // Fail crit ou pas
                                                {
                                                    AttackMove(InBattleEnemy, InBattleSelf, attack, m, n);
                                                    if (!InBattleSelf.IsAlive())
                                                    {
                                                        List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                        foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                        {
                                                            if (poke.IsAlive() && InBattleSelf != poke)
                                                            {
                                                                //chosenActions.Add($"AI_CHANGE_{poke.GetName()}");
                                                                InBattleSelf = poke;
                                                                int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                                maxEval = Math.Max(maxEval, eval2);
                                                                Branch newBranch = new Branch(branchID); // Create a new branch for each action
                                                                newBranch.Actions.Add($"AICHANGE{x.GetName()} && Pokemon Died AICHANGE{poke.GetName()} ");
                                                                newBranch.EvalValue = eval2;
                                                                branches.Add(newBranch);
                                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                                // appel r�cursif

                                                                // Reset Lists at previous states
                                                                ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE2);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                        maxEval = Math.Max(maxEval, eval);
                                                        Branch newBranch = new Branch(branchID); // Create a new branch for each action
                                                        newBranch.Actions.Add($"AICHANGE{x.GetName()}");
                                                        newBranch.EvalValue = eval;
                                                        branches.Add(newBranch);
                                                        //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                        // appel r�cursif
                                                        ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE2);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                    else if (i == "CHANGE")
                                    {
                                        // Le inbattleEnemy se reset pas ducoup c'est la merde
                                        // Faire un truc pour tout les changements de pokemon, pour remettre celui de base
                                        List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                        List<Pokemon> LE2 = new List<Pokemon>(pokemonsEnemyTest);
                                        foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                        {
                                            if (poke.IsAlive() && InBattleEnemy != poke)
                                            {

                                                InBattleEnemy = poke;

                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, branchID, branches);
                                                //chosenActions.RemoveAt(chosenActions.Count - 1);
                                                ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE2);
                                                maxEval = Math.Max(maxEval, eval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return maxEval;
            }


            /*

                        // Ici c'est pour avoir la meilleur valeur, dans le else c'est le contraire
                        else
                        {
                            int minEval = int.MaxValue;
                            List<Pokemon> enemylistnew = new List<Pokemon>(pokemonsEnemyTest);
                            List<Pokemon> selflistnew = new List<Pokemon>(pokemonsSelfTest);
                            foreach (string i in States) // IA STATE
                            {
                                if (i == "ATTACK")
                                {
                                    foreach (Attack move in InBattleSelf.Moves)
                                    { // foreach Move of the AI

                                        // Foreach de states si le mec en face change de pokemon
                                        foreach (string j in States) // PLAYER STATE
                                        {
                                            if (i == "ATTACK")
                                            {
                                                foreach (Attack attack in InBattleEnemy.Moves)
                                                {
                                                    if (InBattleEnemy.Speed > InBattleSelf.Speed)
                                                    {
                                                        foreach (bool k in boolArray) // Fail acc ou pas
                                                        {
                                                            foreach (bool l in boolArray) // Fail crit ou pas
                                                            {
                                                                List<Pokemon> LS1 = new List<Pokemon>(pokemonsSelfTest);
                                                                List<Pokemon> LE1 = new List<Pokemon>(pokemonsEnemyTest);
                                                                AttackMove(InBattleEnemy, InBattleSelf, attack, k, l);
                                                                if (!InBattleSelf.IsAlive())
                                                                {
                                                                    List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                                                    foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                                    {
                                                                        if (poke.IsAlive() && InBattleSelf != poke)
                                                                        {
                                                                            InBattleSelf = poke;

                                                                            // appel r�cursif
                                                                            int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                            minEval = Math.Min(minEval, eval);
                                                                            // Reset Lists at previous states
                                                                            ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE1);
                                                                        }
                                                                    }
                                                                }
                                                                List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                                List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);
                                                                foreach (bool m in boolArray) // Fail acc ou pas
                                                                {
                                                                    foreach (bool n in boolArray) // Fail crit ou pas
                                                                    {
                                                                        AttackMove(InBattleSelf, InBattleEnemy, move, m, n);
                                                                        if (!InBattleEnemy.IsAlive())
                                                                        {
                                                                            foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                                            {
                                                                                if (poke.IsAlive() && InBattleEnemy != poke)
                                                                                {
                                                                                    InBattleEnemy = poke;
                                                                                    int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                                    minEval = Math.Min(minEval, eval2);
                                                                                    ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                                                    // appel r�cursif
                                                                                }
                                                                            }
                                                                        }
                                                                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                        minEval = Math.Min(minEval, eval);
                                                                        ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                        // appel r�cursif
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        foreach (bool k in boolArray) // Fail acc ou pas
                                                        {
                                                            foreach (bool l in boolArray) // Fail crit ou pas
                                                            {
                                                                List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                                List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);

                                                                AttackMove(InBattleSelf, InBattleEnemy, move, k, l);
                                                                if (!InBattleEnemy.IsAlive())
                                                                {
                                                                    foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                                    {
                                                                        if (poke.IsAlive() && InBattleEnemy != poke)
                                                                        {
                                                                            InBattleEnemy = poke;
                                                                            int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                            minEval = Math.Min(minEval, eval);
                                                                            ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                                            // appel r�cursif
                                                                        }
                                                                    }
                                                                }
                                                                foreach (bool m in boolArray) // Fail acc ou pas
                                                                {
                                                                    foreach (bool n in boolArray) // Fail crit ou pas
                                                                    {
                                                                        AttackMove(InBattleEnemy, InBattleSelf, attack, m, n);
                                                                        if (!InBattleSelf.IsAlive())
                                                                        {
                                                                            List<Pokemon> LE1 = new List<Pokemon>(pokemonsEnemyTest);
                                                                            List<Pokemon> LS1 = new List<Pokemon>(pokemonsSelfTest);
                                                                            foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                                            {
                                                                                if (poke.IsAlive() && InBattleSelf != poke)
                                                                                {
                                                                                    InBattleSelf = poke;
                                                                                    int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                                    minEval = Math.Min(minEval, eval2);
                                                                                    // appel r�cursif

                                                                                    // Reset Lists at previous states
                                                                                    ResetList(pokemonsSelfTest, LS1, pokemonsEnemyTest, LE1);
                                                                                }
                                                                            }
                                                                        }
                                                                        int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                        minEval = Math.Min(minEval, eval);
                                                                        ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                                        // appel r�cursif
                                                                    }
                                                                }

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else if (i == "CHANGE")
                                            {
                                                List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                List<Pokemon> LE3 = new List<Pokemon>(pokemonsEnemyTest);
                                                foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                {
                                                    if (poke.IsAlive() && InBattleEnemy != poke)
                                                    {
                                                        foreach (bool k in boolArray) // Fail acc ou pas
                                                        {
                                                            foreach (bool l in boolArray) // Fail crit ou pas
                                                            {
                                                                AttackMove(InBattleSelf, InBattleEnemy, move, k, l);
                                                                if (!InBattleEnemy.IsAlive())
                                                                {
                                                                    foreach (Pokemon en in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                                    {
                                                                        if (poke.IsAlive() && InBattleEnemy != en)
                                                                        {
                                                                            InBattleEnemy = poke;
                                                                            int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                            minEval = Math.Min(minEval, eval2);
                                                                            ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                                            // appel r�cursif
                                                                        }
                                                                    }
                                                                }
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                minEval = Math.Min(minEval, eval);
                                                                ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE3);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (i == "CHANGE")
                                {
                                    foreach (Pokemon x in pokemonsSelfTest)
                                    {
                                        if (x != InBattleSelf && x.IsAlive())
                                        {
                                            // Foreach de states si le mec en face change de pokemon
                                            foreach (string j in States) // PLAYER STATE
                                            {
                                                if (i == "ATTACK")
                                                {
                                                    List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                                    List<Pokemon> LE2 = new List<Pokemon>(pokemonsEnemyTest);
                                                    foreach (Attack attack in InBattleEnemy.Moves)
                                                    {
                                                        foreach (bool m in boolArray) // Fail acc ou pas
                                                        {
                                                            foreach (bool n in boolArray) // Fail crit ou pas
                                                            {
                                                                AttackMove(InBattleEnemy, InBattleSelf, attack, m, n);
                                                                if (!InBattleSelf.IsAlive())
                                                                {
                                                                    List<Pokemon> LS3 = new List<Pokemon>(pokemonsSelfTest);
                                                                    foreach (Pokemon poke in pokemonsSelfTest) // Pour chaque pokemon encore en vie
                                                                    {
                                                                        if (poke.IsAlive() && InBattleSelf != poke)
                                                                        {
                                                                            InBattleSelf = poke;
                                                                            int eval2 = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                            minEval = Math.Min(minEval, eval2);
                                                                            // appel r�cursif

                                                                            // Reset Lists at previous states
                                                                            ResetList(pokemonsSelfTest, LS3, pokemonsEnemyTest, LE2);
                                                                        }
                                                                    }
                                                                }
                                                                int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                                minEval = Math.Min(minEval, eval);
                                                                // appel r�cursif
                                                                ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE2);
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (i == "CHANGE")
                                                {
                                                    List<Pokemon> LS2 = new List<Pokemon>(pokemonsSelfTest);
                                                    List<Pokemon> LE2 = new List<Pokemon>(pokemonsEnemyTest);
                                                    foreach (Pokemon poke in pokemonsEnemyTest) // Pour chaque pokemon encore en vie de l'ennemie
                                                    {
                                                        if (poke.IsAlive() && InBattleEnemy != poke)
                                                        {

                                                            InBattleEnemy = poke;

                                                            int eval = MiniMax(pokemonsEnemyTest, pokemonsSelfTest, InBattleSelf, InBattleEnemy, depth - 1, true, chosenActions);
                                                            ResetList(pokemonsSelfTest, LS2, pokemonsEnemyTest, LE2);
                                                            minEval = Math.Min(minEval, eval);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            return minEval;
                        }*/
            return 0;
        }


    }
}
