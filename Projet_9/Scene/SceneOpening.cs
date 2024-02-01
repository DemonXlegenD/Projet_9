using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScene
{
    internal class SceneOpening : SceneAbstract
    {
        public SceneOpening() : base("Scene Opening") { }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Console.ReadKey();
        }
    }
}
