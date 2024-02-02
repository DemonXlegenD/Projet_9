using NGlobal;
using NModules;
using NPokemon;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace NScene
{
    public class FightScene : SceneAbstract
    {
        private enum States
        {
            SELECT,
            MOVES,
            CHANGE,
            NOTHING
        }
        private States STATE = States.SELECT;

        List<Pokemon> List1;
        List<Pokemon> List2;

        Pokemon P1;
        Pokemon P2;

        int BackChoice = 0;
        List<ConsoleKey> Inputs1 = new List<ConsoleKey>() { ConsoleKey.D1,ConsoleKey.NumPad1, ConsoleKey.Z };
        List<ConsoleKey> Inputs2 = new List<ConsoleKey>() { ConsoleKey.D2, ConsoleKey.NumPad2, ConsoleKey.Q };
        List<ConsoleKey> Inputs3 = new List<ConsoleKey>() { ConsoleKey.D3, ConsoleKey.NumPad3, ConsoleKey.S };
        List<ConsoleKey> Inputs4 = new List<ConsoleKey>() { ConsoleKey.D4, ConsoleKey.NumPad4, ConsoleKey.D };
        List<ConsoleKey> Inputs5 = new List<ConsoleKey>() { ConsoleKey.D5, ConsoleKey.NumPad5, ConsoleKey.A };


        public FightScene() : base("FightScene")
        {

            List1 = new List<Pokemon>() { new Pokemon("jarod", new List<string> { "Water" }, 10, 10, 10, 10, 10, 10, 5) };
            List1[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));
            List2 = new List<Pokemon>() { new Pokemon("maurad", new List<string> { "Grass" }, 10, 10, 10, 10, 10, 10, 5) };
            List2[0].Moves.Add(new Attack("Charge", "Normal", "Physical", 20, 100, 25));

            P1 = List1[0];
            P2 = List2[0];

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            //Console.Clear();
            ToWrite();
            ConsoleKeyInfo key = Console.ReadKey();
            char keyChar = key.KeyChar;
            int keyInt = (int)keyChar;
            Console.WriteLine("Key: "+keyChar.ToString());
            ActionToDo(key);
        }

        private void ActionToDo(ConsoleKeyInfo key)
        {
            switch (STATE)
            {
                case States.SELECT:
                    if (Inputs1.Contains(key.Key))
                    {
                        STATE = States.MOVES;
                    }
                    break;

                case States.MOVES:
                    if (Inputs1.Contains(key.Key))
                    {
                        if (BackChoice == 1)
                        {
                            STATE = States.SELECT;
                        }
                    }
                    if (Inputs2.Contains(key.Key))
                    {
                        if (BackChoice == 2)
                        {
                            STATE = States.SELECT;
                        }
                    }
                    if (Inputs3.Contains(key.Key))
                    {
                        if (BackChoice == 3)
                        {
                            STATE = States.SELECT;
                        }
                    }
                    if (Inputs4.Contains(key.Key))
                    {
                        if (BackChoice == 4)
                        {
                            STATE = States.SELECT;
                        }
                    }
                    if (Inputs5.Contains(key.Key))
                    {
                        if (BackChoice == 5)
                        {
                            STATE = States.SELECT;
                        }
                    }

                    break;

                case States.CHANGE:

                    break;
            }
        }


        private void ToWrite()
        {
            // Write le perso et le perso ennemie
            PokemonInfo(P2);
            SauterLignes(2);
            PokemonInfo(P1);
            SauterLignes(1);

            // En fonction du state, print les choix dispo

            switch(STATE)
            {
                case States.SELECT:
                    Console.WriteLine("1: Moves  2: Items  3:Pokemons  4: Escape");
                    Console.WriteLine("Write your choice bellow !");
                    SauterLignes(1);
                    break;

                case States.MOVES:
                    int x = 1;
                    foreach (Attack i in P1.Moves)
                    {
                        Console.Write(x+": "+i.GetName()+"  ");
                        x++;
                    }
                    BackChoice = x;
                    Console.Write(x+": Back");
                    break;

                case States.CHANGE:

                    break;
            }

        }

        private void SauterLignes(int x)
        {
            for (int i = 0; i < x; i++) { Console.WriteLine(" "); }
        }

        private void PokemonInfo(Pokemon P)
        {
            if (P.Types.Count > 1) 
            {
                Console.Write(P.Name + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(P.Hp + "/" + P.MaxHp + " ");
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[0]);
                Console.Write(P.Types[0]+" ");
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[1]);
                Console.Write(P.Types[1]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else 
            { 
                Console.Write(P.Name + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(P.Hp+"/"+P.MaxHp+" ");
                Console.ForegroundColor = Global.TypeToConsoleColor(P.Types[0]);
                Console.Write(P.Types[0]);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Render()
        {
            //Console.WriteLine("Render : Hello it's new here");
        }
    }
}