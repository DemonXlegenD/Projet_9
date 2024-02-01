using NGlobal;
using NModules;
using NPokemon;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ToWrite();
            Console.ReadKey();
            Console.WriteLine("Update : Hello it's new here");
        }


        private void ToWrite()
        {
            // Write le perso et le perso ennemie

            PokemonInfo(P2);
            SauterLignes(2);
            PokemonInfo(P1);


            // En fonction du state, print les choix dispo


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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(P.Types[0]+" ");
                Console.Write(P.Types[1]);
            }
            else 
            { 
                Console.Write(P.Name + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(P.Hp+"/"+P.MaxHp+" ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(P.Types[0]);
            }
        }

        public override void Render()
        {
            //Console.WriteLine("Render : Hello it's new here");
        }
    }
}