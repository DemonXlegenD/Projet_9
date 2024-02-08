using NEngine;
using NEntity;
using Newtonsoft.Json;
using NPokemon;
using NPotionType;
using NSave;
using NSecurity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NGlobal;
using static NGlobal.Global;
using System.Runtime.InteropServices.ComTypes;

namespace Projet_9
{
    internal class Program
    {


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SCREEN_BUFFER_INFO
        {
            public COORD dwSize;
            public COORD dwCursorPosition;
            public ushort wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool bAbsolute, [In] ref SMALL_RECT lpConsoleWindow);

        static void SetConsoleWindowSize(int width, int height)
        {
            IntPtr consoleOutput = GetStdHandle(-11); // STD_OUTPUT_HANDLE
            CONSOLE_SCREEN_BUFFER_INFO bufferInfo;
            GetConsoleScreenBufferInfo(consoleOutput, out bufferInfo);

            SMALL_RECT rect = new SMALL_RECT
            {
                Left = 0,
                Top = 0,
                Right = (short)(width - 1),
                Bottom = (short)(height - 1)
            };

            SetConsoleWindowInfo(consoleOutput, true, ref rect);
        }


        static void Main(string[] args)
        {
            Engine engine = Engine.GetInstance();
            SetConsoleWindowSize(400, 100);
            List<Pokemon> Pokemons = new List<Pokemon>();
            Pokemons.Add(new Pokemon("1","Jarod", new List<string> { "Water" }, 100, 100, 100, 100, 100, 100, 100));
            Pokemons.Add(new Pokemon("1","Francois", new List<string> { "Fire" }, 80, 10, 10, 10, 10, 10, 20));
            Pokemons.Add(new Pokemon("1","Maurad", new List<string> { "Grass" }, 80, 10, 10, 10, 10, 10, 50));
            Pokemons.Add(new Pokemon("1","Adrien", new List<string> { "Ground" }, 80, 10, 10, 10, 10, 10, 99));
            Pokemons.Add(new Pokemon("1","Kyle", new List<string> { "Dragon" }, 80, 10, 10, 10, 10, 10, 40));
            Pokemons.Add(new Pokemon("1", "Ethan", new List<string> { "Bug", "Grass" }, 80, 10, 10, 10, 10, 10, 50));
            Pokemons[0].Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            foreach (Pokemon p in Pokemons)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }

            List<Pokemon> Pokemons2 = new List<Pokemon>();
            Pokemons2.Add(new Pokemon("1","Jarod", new List<string> { "Ground" }, 80, 54, 64, 100, 100, 100, 100));
            Pokemons2.Add(new Pokemon("1","Francois", new List<string> { "Grass" }, 80, 10, 24, 10, 10, 10, 200));
            Pokemons2.Add(new Pokemon("1","Maurad", new List<string> { "Grass" }, 14, 24, 10, 10, 10, 10, 500));
            Pokemons2.Add(new Pokemon("1","Adrien", new List<string> { "Grass" }, 78, 10, 10, 54, 10, 10, 99));
            Pokemons2.Add(new Pokemon("1","Kyle", new List<string> { "Grass" }, 80, 54, 78, 10, 22, 10, 400));
            Pokemons2.Add(new Pokemon("1", "Ethan", new List<string> { "Bug", "Grass" }, 80, 10, 10, 25, 10, 10, 500));
            foreach (Pokemon p in Pokemons2)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }

            
            //Hacker hackertest = new Hacker(Pokemons,Pokemons2,"Jarod","Francois",false);

            /*WindowPokemonTeam window = new WindowPokemonTeam();
            window.WindowRun();*/
            //Engine engine = Engine.GetInstance();

            SetConsoleWindowSize(400, 100);

            engine.Run();

            Environment.Exit(0);
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Person : {{ FirstName: {FirstName}, LastName: {LastName}, Age: {Age} }}";
        }
    }
}
