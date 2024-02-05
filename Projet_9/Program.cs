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

            SetConsoleWindowSize(50, 50);
            UserManager userManager = UserManager.GetInstance();
            userManager.NewUser("Jawad", "Mamague28021");
            Console.WriteLine("C'est bien : " + userManager.ActualUser.Username);


            engine.Run();

            Player player = new Player();
            List<string> list = new List<string>
            {
                "Water"
            };
            List<JsonConverter> listConverter = new List<JsonConverter>
            {
                new PlayerJsonConverter(),
                new PokemonJsonConverter()
            };


            player.AddItem(new Potion());
            player.AddPokemon(new Pokemon("Jarod", list, 40, 50, 70, 50, 50, 70));

            SavePlayer.GetInstance(player.FirstName, player.LastName, player.Id).WriteSave(player, 2, listConverter);
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
