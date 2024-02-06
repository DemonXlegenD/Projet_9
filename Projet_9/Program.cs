using NPokemon;
using NSave;
using NEntity;
using NPotionType;
using System.Collections.Generic;
using Newtonsoft.Json;
using NEngine;
using Projet_9.PokemonTeam;
using NModules;
using NScene;
using Projet_9.Save;
using NSecurity;
using System;
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
            List<Pokemon> Pokemons = new List<Pokemon>();
            Pokemons.Add(new Pokemon("Jarod", new List<string> { "Water" }, 100, 100, 100, 100, 100, 100, 1000));
            Pokemons.Add(new Pokemon("Francois", new List<string> { "Fire" }, 80, 10, 10, 10, 10, 10, 200));
            Pokemons.Add(new Pokemon("Maurad", new List<string> { "Grass" }, 80, 10, 10, 10, 10, 10, 500));
            Pokemons.Add(new Pokemon("Adrien", new List<string> { "Ground" }, 80, 10, 10, 10, 10, 10, 99));
            Pokemons.Add(new Pokemon("Kyle", new List<string> { "Dragon" }, 80, 10, 10, 10, 10, 10, 400));
            Pokemons.Add(new Pokemon("Ethan", new List<string> { "Bug", "Grass" }, 80, 10, 10, 10, 10, 10, 500));
            Pokemons[0].Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            foreach (Pokemon p in Pokemons)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }

            List<Pokemon> Pokemons2 = new List<Pokemon>();
            Pokemons2.Add(new Pokemon("c", new List<string> { "Ground" }, 80, 54, 64, 100, 100, 100, 100));
            Pokemons2.Add(new Pokemon("b", new List<string> { "Grass" }, 80, 10, 24, 10, 10, 10, 200));
            Pokemons2.Add(new Pokemon("a", new List<string> { "Grass" }, 14, 24, 10, 10, 10, 10, 500));
            Pokemons2.Add(new Pokemon("d", new List<string> { "Grass" }, 78, 10, 10, 54, 10, 10, 99));
            Pokemons2.Add(new Pokemon("e", new List<string> { "Grass" }, 80, 54, 78, 10, 22, 10, 400));
            Pokemons2.Add(new Pokemon("f", new List<string> { "Bug", "Grass" }, 80, 10, 10, 25, 10, 10, 500));
            foreach (Pokemon p in Pokemons2)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }

            // Marche mais la recherche marche pas ? 
            Hacker hackertest = new Hacker(Pokemons,Pokemons2,"Jarod","Francois",true);

            /*WindowPokemonTeam window = new WindowPokemonTeam();
            window.WindowRun();*/
            Engine engine = Engine.GetInstance();

            SetConsoleWindowSize(400, 100);



            engine.Run();
        /*    window.WindowClose();*/

            Player player = new Player();
            List<string> list = new List<string>();
            list.Add("Water");
            List<JsonConverter> listConverter = new List<JsonConverter>();
            listConverter.Add(new PlayerJsonConverter());
            listConverter.Add(new PokemonJsonConverter());


              player.AddItem(new Potion());
              player.AddPokemon(new Pokemon("Jarod", list, 40, 50, 70, 50, 50, 70));

              SavePlayer.GetInstance(player.FirstName, player.LastName, player.Id).WriteSave(player, 2, listConverter);
            /*          Player player = Save.GetInstance().ReadTestSave<Player>(2);
                      Console.WriteLine(player.ToString());*/
/*            SaveUser saveUser = SaveUser.GetInstance();*/
            UserManager userManager = UserManager.GetInstance();
     /*       userManager.AddUser("Zarod", "azerty");
            saveUser.SaveUsers();*/
            User.LoadUser("Zarod", "azerty");
            Console.WriteLine("C'est bien : " + userManager.ActualUser.Username);
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
