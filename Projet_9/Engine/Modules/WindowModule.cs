using NEngine;
using System;
using System.Media;

namespace NModules
{

    public sealed class WindowModule : Module
    {
        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;

    }
}
