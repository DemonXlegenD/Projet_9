using NEngine;
using System;
using System.Threading;

namespace NModules
{
    public sealed class InputModule : Module
    {
        private Engine engine;
        public override void Start()
        {
            base.Start();
            engine = Engine.GetInstance();
        }

        public override void Update()
        {
            base.Update();
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Escape");
                    engine.Quit();
                }
                if (key.Key == ConsoleKey.Z)
                {
                    Console.WriteLine("Je suis Jarooooooooooooooooooooooooooooooooooooooooooooooooooooood");
                }
            }
        }
    }
}
