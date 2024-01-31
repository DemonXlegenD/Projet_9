using NModules;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScene
{
    public class FightScene : Scene
    {
        public FightScene() : base("FightScene")
        {

        }

        public override void Update(float deltaTime)
        {
            Console.WriteLine("Hello it's new here");
        }

        public override void Render()
        {
            Console.WriteLine("Hello it's new here");
        }
    }
}