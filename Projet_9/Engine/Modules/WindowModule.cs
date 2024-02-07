using NEngine;
using System;   
namespace NModules
{

    public sealed class WindowModule : Module
    {
        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;
    }
}
