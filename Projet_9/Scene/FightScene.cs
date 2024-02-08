using Map;
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
using System.Security.Policy;
using NEntity;
using System.Windows;
using NQuest;
using NEngine;


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

        private bool P1Used = false;
        private bool P2Used = false;

        List<Pokemon> List1;
        private List<Pokemon> List2;

        private Pokemon P1;
        private Pokemon P2;

        private int SelectedAI;


        private List<String> TextQueue = new List<String>();
        private string AnimationQueue = "";

        private int BackChoice = 0;
        private int MaxChoixe = 0;

        private List<ConsoleKey> Inputs1 = new List<ConsoleKey>() { ConsoleKey.Q,ConsoleKey.A, ConsoleKey.LeftArrow };
        private List<ConsoleKey> Inputs2 = new List<ConsoleKey>() { ConsoleKey.D, ConsoleKey.RightArrow };
        private List<ConsoleKey> Inputs3 = new List<ConsoleKey>() { ConsoleKey.Z, ConsoleKey.W, ConsoleKey.UpArrow };
        private List<ConsoleKey> Inputs4 = new List<ConsoleKey>() { ConsoleKey.S, ConsoleKey.DownArrow };

        private string[] List_Actions_Select = { "1: Move ","2: Items ","3: Pokemons ","4: Escape " };

        private QuestManager questManager;


        // TO START A FIGHT : CHANGE ( IsWildFight,EnemyPokemons,PlayerPokemons )
        public FightScene() : base("FightScene")
        {

            List1 = new List<Pokemon>() { new Pokemon("1","jarod", new List<string> { "Water" }, 10, 10, 10, 10, 10, 10, 5), new Pokemon("3", "Francois", new List<string> { "Fire" }, 10, 10, 10, 10, 10, 10, 5) };
            List1[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));
            List1[1].Moves.Add(new Attack("Brule", "Fire", "Special", 20, 100, 25));
            List2 = new List<Pokemon>() { new Pokemon("2", "maurad", new List<string> { "Grass" }, 10, 10, 10, 10, 10, 10, 5), new Pokemon("4", "hamid", new List<string> { "Grass" }, 10, 10, 10, 10, 10, 10, 5) };
            List2[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));

            P1 = List1[0];
            P2 = List2[0];

            questManager = new QuestManager();

            questManager.SubscribeToTrainerDefeatedEvent();
        }

        static void OnTrainerDefeated(object sender, TrainerDefeatedEventArgs e)
        {
            Console.WriteLine("Trainer has been defeated!");
        }


        public override void Launch()
        {
            //Console.BackgroundColor = ConsoleColor.DarkBlue; Pas de transparence donc jsp
            //System.Drawing.FontFamily fontFamily = new System.Drawing.FontFamily(@"");
            Console.Clear();
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            ToWrite();
            TextQueue.Clear();
            // Peut read le truc pour l'attaque
            //List<string> sprite = ReadFilesText(TXTAttacksPath+"attack_hit_Dark_Ghost_1.txt");

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
                                if (Global.IsWildFight)
                                {
                                    if (OddsEscape(P1.Speed, P2.Speed))
                                    {
                                        Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
                                        // GO TO MAP SCENE
                                    }
                                    else
                                    {
                                        // Doesnt escape and take damage
                                    }
                                }
                                else { TextQueue.Add("You can't escape from a trainer !"); }
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
                                if (Global.IsWildFight)
                                {
                                    if (OddsEscape(P1.Speed, P2.Speed))
                                    {
                                        // GO TO MAP SCENE
                                    }
                                    else
                                    {
                                        TextQueue.Add("The escape failed !");
                                        P1Used = true;
                                        STATE = States.TURN;
                                    }
                                }
                                else { TextQueue.Add("You can't escape from a trainer !"); }
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
                            int choice = Magnus.MakeChoice(List2, List1, P2, P1);
                            if (choice > 0)
                            {
                                SelectedAI = choice - 1;
                            }
                            else
                            {
                                P2 = List2[choice + 1];
                                P2Used = false;
                            }
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
                            int choice = Magnus.MakeChoice(List2, List1, P2, P1);
                            if (choice > 0)
                            {
                                SelectedAI = choice - 1;
                            }
                            else
                            {
                                P2 = List2[Math.Abs(choice + 1)];
                                P2Used = false;
                            }
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
                                    int choice = Magnus.MakeChoice(List2, List1, P2, P1);
                                    if (choice > 0)
                                    {
                                        SelectedAI = choice-1;
                                    }
                                    else
                                    {
                                        P2 = List2[choice+1];
                                        P2Used = false;
                                    }
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
                                    int choice = Magnus.MakeChoice(List2, List1, P2, P1);
                                    if (choice > 0)
                                    {
                                        SelectedAI = choice - 1;
                                    }
                                    else
                                    {
                                        P2 = List2[choice + 1];
                                        P2Used = false;
                                    }
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
                    // Continue de faire des turns pour le pokemon ennemie
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
            float healthPercentage = (float)currentHP / maxHP * 100;

            if (healthPercentage > 50){Console.ForegroundColor = ConsoleColor.Green;}
            else if (healthPercentage > 25){Console.ForegroundColor = ConsoleColor.DarkYellow;}
            else{Console.ForegroundColor = ConsoleColor.DarkRed;}

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
            List<string> sprite = ReadFilesText(GetFileAtIndex(TXTCharactersPath, 1)); // Nombre impaire pour les faces
            WriteSprites(sprite,2);
            // Mettre des pokemons si on veut 
            List<string> sprite1 = ReadFilesText(GetFileAtIndex(TXTCharactersPath, 5)); // Nombre impaire pour les faces
            WriteSprites(sprite1);
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
                            Console.Write(y + ": " + i.Name + " " + i.Hp+"/"+i.MaxHp+" ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(y + ": " + i.GetName() + "  ");
                        }
                        y++;
                    }
                    BackChoice = y;
                    if (P1.IsAlive())
                    {
                        if (BackChoice-1 == PSelectIndex)
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
                    if (AnimationQueue != "")
                    {
                        // aléatoire couleurs si efficace
                        // Si critique rainbow not all
                        // Si critique et efficace, rainbow all

                        // Pareils pour les textes critique etc
                        var rng = new RNGCryptoServiceProvider();
                        List<string> sprite2 = ReadFilesText(GetFileAtIndex(TXTAttacksPath, GenerateRandomNumber(rng, 1, 90)));
                        if (TextQueue.Contains("Critical hit!"))
                        {
                            if (TextQueue.Contains("Super Efficace !"))
                            {
                                WriteSprites(sprite2, 3, 2);
                            }
                            else
                            {
                                WriteSprites(sprite2, 3, 2, false);
                            }
                        }
                        else if (TextQueue.Contains("Super Efficace !"))
                        {
                            WriteSprites(sprite2, 3, 1, false);
                        }
                        else
                        {
                            WriteSprites(sprite2,3);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        AnimationQueue = "";
                    }
                    
                    Console.WriteLine(" Appuyez sur une touche pour continuer");
                    SauterLignes(2);



                    break;
            }

            foreach(string i in TextQueue)
            {
                Console.WriteLine(i);
            }
            //SauterLignes(2);
        }


        private void DoMove()
        {
            if (!P1.IsAlive())
            {
                P1.DeathHp();
                P1Used = true;
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
                if (o == List2.Count - 1) { STATE = States.NOTHING; }
                P2Used = true;
            }

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
                    AttackMove(P2, P1, P2.Moves[SelectedAI]);
                }
                AnimationQueue = "1";
            }
            else if (P1.GetSpeed() < P2.GetSpeed() && STATE == States.TURN)
            {
                if (!P2Used)
                {
                    P2Used = true;
                    AttackMove(P2, P1, P2.Moves[SelectedAI]);
                }
                else if (!P1Used)
                {
                    P1Used = true;
                    AttackMove(P1, P2, P1.Moves[SelectedIndex-1]);
                }
                AnimationQueue = "1";
            }

            if (!P1.IsAlive())
            {
                int o = 0;
                foreach(Pokemon p in List1)
                {
                    if (p.IsAlive()) { o++; }
                }
                // Death of the player
                if(o == 0)
                {
                    AfterFightTeamPokemon(List1);
                    HealTeamPokemon(List1);
                    // Go to main scene
                }
                P1.DeathHp();
                P1Used = true;
                STATE = States.CHANGE;
            }
            if (!P2.IsAlive())
            {
                P1.ChangeXp(P2.Level * 2 + 1);
                P2.DeathHp();
                int o = 0;
                foreach (Pokemon p in List2)
                {
                    if (p.IsAlive()) {  o++; }
                }
                // Death of the bot
                if (o <= 0) 
                {
                    AfterFightTeamPokemon(List1);
                    // Change to main scene
                    if (!IsWildFight)
                    {
                        questManager.TriggerTrainerDefeatedEvent("Ash");
                    }

                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
                }
                else
                {
                    foreach (Pokemon p in List2)
                    {
                        if(p.IsAlive()) { P2 = p; }
                    }
                    P1Used = false;
                    P2Used = false;
                    STATE = States.SELECT;
                    return;
                }
            }

        }

        public void AttackMove(Pokemon Attacker,Pokemon Defenser ,Attack a)
        {
            TextQueue.Add(Attacker.Name+" attacks !");
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
                TextQueue.Add("The Attack missed !");
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

        private void RandomArtAttack()
        {
            var rng = new RNGCryptoServiceProvider();
            List<string> sprite = ReadFilesText(GetFileAtIndex(TXTAttacksPath, GenerateRandomNumber(rng, 1, 90)));
            WriteSprites(sprite, 3, 2, false);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}