﻿using Map;
using NGlobal;
using NModules;
using NPokemon;
using NScene;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using static NGlobal.Global;
using System.Security.Cryptography;


namespace NScene
{
    public class FightScene : SceneAbstract
    {
        private enum States
        {
            SELECT,
            MOVES,
            ITEMS,
            CHANGE,
            TURN,
            LEARN,
            NOTHING
        }
        private States STATE = States.SELECT;

        private int SelectedIndex = 0;
        private int PSelectIndex = 0;

        bool P1Used = false;
        bool P2Used = false;

        List<Pokemon> List1;
        List<Pokemon> List2;

        Pokemon P1;
        Pokemon P2;

        List<String> TextQueue = new List<String>();

        int BackChoice = 0;
        int MaxChoixe = 0;

        List<ConsoleKey> Inputs1 = new List<ConsoleKey>() { ConsoleKey.Q,ConsoleKey.A, ConsoleKey.LeftArrow };
        List<ConsoleKey> Inputs2 = new List<ConsoleKey>() { ConsoleKey.D, ConsoleKey.RightArrow };
        List<ConsoleKey> Inputs3 = new List<ConsoleKey>() { ConsoleKey.Z, ConsoleKey.W, ConsoleKey.UpArrow };
        List<ConsoleKey> Inputs4 = new List<ConsoleKey>() { ConsoleKey.S, ConsoleKey.DownArrow };

        string[] List_Actions_Select = { "1: Move ","2: Items ","3: Pokemons ","4: Escape " };

        public FightScene() : base("FightScene")
        {

            List1 = new List<Pokemon>() { new Pokemon("jarod", new List<string> { "Water" }, 10, 10, 10, 10, 10, 10, 5), new Pokemon("Francois", new List<string> { "Fire" }, 10, 10, 10, 10, 10, 10, 5) };
            List1[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));
            List1[1].Moves.Add(new Attack("Brule", "Fire", "Special", 20, 100, 25));
            List2 = new List<Pokemon>() { new Pokemon("maurad", new List<string> { "Grass" }, 10, 10, 10, 10, 10, 10, 5) };
            List2[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));

            P1 = List1[0];
            P2 = List2[0];
            

        }


        public override void Launch()
        {
            //System.Drawing.FontFamily fontFamily = new System.Drawing.FontFamily(@"");
            Console.Clear();
            ToWrite();
            TextQueue.Clear();
            // Peut read le truc pour l'attaque
            List<string> sprite = ReadFilesText("C:\\Users\\Kyle\\Documents\\GitHub\\Projet_9\\Projet_9\\Assets\\TXT_files_Attacks\\attack_hit_Dark_Ghost_1.txt");
            WriteSprites(sprite,3,2,false);
            ConsoleKeyInfo key = Console.ReadKey();
            ActionToDo(key);
            Input(key);
        }

        private void Input(ConsoleKeyInfo key)
        {
            if (Inputs1.Contains(key.Key) || Inputs3.Contains(key.Key))
            {
                PSelectIndex--;
                if (PSelectIndex < 0)
                {
                    PSelectIndex = BackChoice-1;
                }
            }
            else if (Inputs2.Contains(key.Key) || Inputs4.Contains(key.Key))
            {
                PSelectIndex++;
                if (PSelectIndex > BackChoice-1)
                {
                    PSelectIndex = 0;
                }
            }
        }

        private void ActionToDo(ConsoleKeyInfo key)
        {
            switch (STATE)
            {
                case States.SELECT:
                    if (char.IsDigit(key.KeyChar))
                    {
                        int selectedNumber = int.Parse(key.KeyChar.ToString());
                        switch (selectedNumber)
                        {
                            case 1:
                                STATE = States.MOVES;
                                PSelectIndex = 0;
                                break;
                            
                            case 2:
                                // Items
                                break;
                            
                            case 3:
                                STATE = States.CHANGE;
                                PSelectIndex = 0;
                                break;
                            
                            case 4:
                                if (OddsEscape(P1.Speed, P2.Speed))
                                {
                                    // Escape
                                }
                                else
                                {
                                    // Doesnt escape and take damage
                                }
                                break;
                        }

                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        switch (PSelectIndex)
                        {
                            case 0:
                                STATE = States.MOVES;
                                PSelectIndex = 0;
                                break;

                            case 1:
                                // Items
                                break;

                            case 2:
                                STATE = States.CHANGE;
                                PSelectIndex = 0;
                                break;

                            case 3:
                                // Escape
                                break;
                        }
                    }
                    break;

                case States.MOVES:
                    if (char.IsDigit(key.KeyChar))
                    {
                        int selectedNumber = int.Parse(key.KeyChar.ToString());
                        SelectedIndex = selectedNumber;
                        if (selectedNumber >= BackChoice)
                        {
                            STATE = States.SELECT;
                        }
                        else
                        {
                            DoMove();
                            STATE = States.TURN;
                        }

                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (PSelectIndex >= BackChoice-1)
                        {
                            STATE = States.SELECT;
                            PSelectIndex = 0;
                        }
                        else
                        {
                            SelectedIndex = PSelectIndex+1;
                            DoMove();
                            STATE = States.TURN;
                            PSelectIndex = 0;
                        }
                    }
                    break;

                case States.CHANGE:
                    if (char.IsDigit(key.KeyChar))
                    {
                        int selectedNumber = int.Parse(key.KeyChar.ToString());
                        SelectedIndex = selectedNumber-1;
                        if (selectedNumber >= BackChoice && P1.IsAlive())
                        {
                            STATE = States.SELECT;
                        }
                        else
                        {
                            if(List1[SelectedIndex].IsAlive() && List1[SelectedIndex] != P1 && !P1Used)
                            {
                                if (P1.IsAlive())
                                {
                                    P1 = List1[SelectedIndex];
                                    P1Used = true;
                                    STATE = States.TURN;
                                }
                                else
                                {
                                    P1 = List1[SelectedIndex];
                                    STATE = States.SELECT;
                                }
                            }
                            else { TextQueue.Add("Pokemon selected is either in battle or dead"); }
                        }
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        SelectedIndex = PSelectIndex;
                        if (PSelectIndex >= BackChoice-1 && P1.IsAlive())
                        {
                            STATE = States.SELECT;
                        }
                        else
                        {
                            if (List1[SelectedIndex].IsAlive() && List1[SelectedIndex] != P1 && !P1Used)
                            {
                                if (P1.IsAlive())
                                {
                                    P1 = List1[SelectedIndex];
                                    P1Used = true;
                                    STATE = States.TURN;
                                }
                                else
                                {
                                    P1 = List1[SelectedIndex];
                                    STATE = States.SELECT;
                                }
                            }
                            else { TextQueue.Add("Pokemon selected is either in battle or dead"); }
                        }
                    }
                    break;

                case States.LEARN: 
                    
                    break;

                case States.TURN:

                    DoMove();

                    break;
            }
        }

        private void DisplayHealthBar(int currentHP, int maxHP, bool right = false)
        {
            if (right)
            {
                int leftPosition = Console.WindowWidth - 27;
                int topPosition = Console.CursorTop;
                Console.SetCursorPosition(leftPosition, topPosition);
            }
            Console.Write("HP : ");
            int barLength = 20;

            int filledLength = (int)Math.Ceiling((double)currentHP / maxHP * barLength);

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < filledLength; i++)
            {
                Console.Write("█");
            }

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = filledLength; i < barLength; i++)
            {
                Console.Write("█");
            }
            Console.Write("  ");
        }

        private void DisplayXpBar(int currentXp, int nextXp,bool right = false)
        {
            Console.WriteLine();
            if (right)
            {
                int leftPosition = Console.WindowWidth - 25;
                int topPosition = Console.CursorTop;
                Console.SetCursorPosition(leftPosition, topPosition);
            }
            Console.Write("XP : ");
            int barLength = 10;

            int filledLength = (int)Math.Ceiling((double)currentXp / nextXp * barLength);

            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < filledLength; i++)
            {
                Console.Write("█");
            }

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = filledLength; i < barLength; i++)
            {
                Console.Write("█");
            }
            Console.Write("  ");
        }


        private void ToWrite()
        {
            // Write le perso et le perso ennemie
            int leftPosition = Console.WindowWidth - 25;
            int topPosition = Console.CursorTop;
            Console.SetCursorPosition(leftPosition, topPosition);

            PokemonInfo(P2,right:true);
            SauterLignes(1);
            string[] pika = {"       _ _         ", " _ __ (_) | ____ _ ", "| '_ \\| | |/ / _` |", "| |_) | |   < (_| |", "| .__/|_|_|\\_\\__,_|", "|_|                " };

            foreach (string i in pika)
            {
                leftPosition = Console.WindowWidth - 25;
                topPosition = Console.CursorTop;
                Console.SetCursorPosition(leftPosition, topPosition);
                Console.WriteLine(i);
            }
            SauterLignes(2);
            Console.WriteLine("       _ _         \n _ __ (_) | ____ _ \n| '_ \\| | |/ / _` |\n| |_) | |   < (_| |\n| .__/|_|_|\\_\\__,_|\n|_|                ");
            // Mettre des pokemons si on veut 

            PokemonInfo(P1,true);
            SauterLignes(2);

            // En fonction du state, print les choix dispo

            switch(STATE)
            {
                case States.SELECT:
                    for(int i =0; i < List_Actions_Select.Count();i++)
                    {
                        if(i == PSelectIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(List_Actions_Select[i]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(List_Actions_Select[i]);
                        }
                        BackChoice = i+1;
                    }
                    //Console.WriteLine("1: Moves  2: Items  3:Pokemons  4: Escape");
                    SauterLignes(1);
                    Console.WriteLine("Write your choice bellow ! Or use the arrows !");
                    SauterLignes(1);
                    break;

                case States.MOVES:
                    int x = 1;
                    foreach (Attack i in P1.Moves)
                    {
                        if (x == PSelectIndex+1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(x + ": " + i.GetName() + "  ");
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else
                        {
                            Console.Write(x + ": " + i.GetName() + "  ");
                        }
                        x++;
                    }
                    BackChoice = x;
                    if (PSelectIndex == BackChoice - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(x + ": Back");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(x + ": Back");
                    }
                    SauterLignes(2);
                    break;

                case States.CHANGE:
                    int y = 1;
                    foreach (Pokemon i in List1)
                    {
                        if (y == PSelectIndex+1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(y + ": " + i.GetName() + "  ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(y + ": " + i.GetName() + "  ");
                        }
                        y++;
                    }
                    BackChoice = y-1;
                    if (P1.IsAlive())
                    {
                        if (PSelectIndex == BackChoice - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(y + ": Back");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(y + ": Back");
                        }
                    }
                    SauterLignes(2);
                    break;

                case States.LEARN:

                    break;

                case States.TURN:

                    Console.WriteLine(" Appuyez sur une touche pour continuer");
                    SauterLignes(2);



                    break;
            }

            foreach(string i in TextQueue)
            {
                Console.WriteLine(i);
            }
            SauterLignes(2);
        }


        private void DoMove()
        {
            if (P2Used && P1Used)
            {
                P1Used = false;
                P2Used = false;
                STATE = States.SELECT;
                return;
            }
            else if (P1.GetSpeed() >= P2.GetSpeed() && STATE == States.TURN)
            {
                if (!P1Used)
                {
                    P1Used = true;
                    AttackMove(P1, P2, P1.Moves[SelectedIndex-1]);
                }
                else if (!P2Used)
                {
                    P2Used = true;
                    AttackMove(P2, P1, P2.Moves[0]);
                }
            }
            else if (P1.GetSpeed() < P2.GetSpeed() && STATE == States.TURN)
            {
                if (!P2Used)
                {
                    P2Used = true;
                    AttackMove(P2, P1, P2.Moves[0]);
                }
                else if (!P1Used)
                {
                    P1Used = true;
                    AttackMove(P1, P2, P1.Moves[SelectedIndex-1]);
                }
            }

            if (!P1.IsAlive())
            {
                P1.DeathHp();
                P1Used = false;
                STATE = States.CHANGE;
            }
            if (!P2.IsAlive())
            {
                P2.DeathHp();
                int o = 0;
                foreach (Pokemon p in List2)
                {
                    o++;
                }
                if (o == List2.Count-1) { STATE = States.NOTHING; }
                P2Used = false;
            }

        }

        public void AttackMove(Pokemon Attacker,Pokemon Defenser ,Attack a)
        {
            if (Global.SuccessAcc(a.GetAcc()))
            {
                if (a.GetCat() == "Heal")
                {
                    Attacker.HealPercentage(a.GetPower());
                }
                else
                {
                    var Critical = Global.SuccessCritical(P1.GetSpeed());
                    if (Critical == 2)
                    {
                        TextQueue.Add("Critical hit!");
                    }
                    if (Chart[(int)TypeToIndex(a.GetType()), (int)TypeToIndex(Defenser.Types[0])] > 1) { TextQueue.Add("Super Efficace !"); }
                    if (Chart[(int)TypeToIndex(a.GetType()), (int)TypeToIndex(Defenser.Types[0])] < 1) { TextQueue.Add("Ce n'est pas très efficace..."); }

                    var Damage = Global.DamageCalculator(Attacker, Defenser, a, Critical);
                    Defenser.TakeDamage(Damage);
                }
            }
            else
            {
                TextQueue.Add("The Attack mised !");
            }
            a.UseAttack();
        }




        private void SauterLignes(int x)
        {
            for (int i = 0; i < x; i++) { Console.WriteLine(" "); }
        }

        private void PokemonInfo(Pokemon P,bool player = false,bool right = false)
        {
            Console.Write(P.Name + " ");
            Console.Write("Lv."+P.Level+" ");
            if (P.Types.Count > 1)
            {
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[0]);
                Console.Write(P.Types[0] + " ");
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[1]);
                Console.Write(P.Types[1]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[0]);
                Console.Write(P.Types[0]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            SauterLignes(1);
            DisplayHealthBar(P.Hp, P.MaxHp,right);
            if (player)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(P.Hp + "/" + P.MaxHp + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (player) { 
                DisplayXpBar(P.Xp,P.XpNext,right);
                Console.Write(P.Xp+ "/"+P.XpNext);
            }

        }


    }
}