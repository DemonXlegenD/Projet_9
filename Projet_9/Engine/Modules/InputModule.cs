using NEngine;
using System;

namespace NModules
{
    public sealed class InputModule : Module
    {
        private WindowModule window;
        private Engine engine;
        public override void Start()
        {
            base.Start();
            window = ModuleManager.GetModule<WindowModule>();
            engine = Engine.GetInstance();
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Update");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Escape");
                engine.Quit();
            }
        }
    }
}
