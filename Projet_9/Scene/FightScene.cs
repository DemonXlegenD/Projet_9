using NModules;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public FightScene() : base("FightScene")
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Console.WriteLine("Update : Hello it's new here");
        }


        private void ToWrite()
        {
            // Write le perso et le perso ennemie

            // En fonction du state, print les choix dispo


        }

        public override void Render()
        {
            Console.WriteLine("Render : Hello it's new here");
        }
    }
}