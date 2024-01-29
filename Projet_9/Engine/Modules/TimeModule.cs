using NEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModules
{
    public sealed class TimeModule : Module
    {

        private float deltaTime = 0.0f;
        private Stopwatch deltaClock = new Stopwatch();
        private Stopwatch clock = new Stopwatch();

        public override void Init()
        {
            base.Init();
            clock.Start();
            deltaClock.Start();
        }

        public override void Update()
        {
            base.Update(); 
            deltaTime = (float)deltaClock.Elapsed.TotalSeconds;
            deltaClock.Restart();
        }


        public float GetDeltaTimeSinceBeginning()
        {
            return (float)clock.Elapsed.TotalSeconds;
        }

        public float GetDeltaTime()
        {
            return deltaTime;
        }
    }
}
