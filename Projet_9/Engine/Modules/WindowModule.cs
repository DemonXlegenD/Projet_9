using NEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModules
{

    public sealed class WindowModule : Module
    {
        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;

        public override void Init()
        {
            Console.SetWindowSize(1000, 700);
            Console.BackgroundColor = ConsoleColor.Blue;
        }

        public override void Start()
        {
            // Implement Start logic for WindowModule
        }

        public override void Update()
        {
            // Implement Update logic for WindowModule
        }

        public override void PreRender()
        {
            Console.Clear();
        }

        public override void Render()
        {
            // Implement Render logic for WindowModule
        }

        public override void PostRender()
        {
            // Implement PostRender logic for WindowModule
        }

        public override void Release()
        {
            Console.Write(Console.ForegroundColor);
        }
    }
}
