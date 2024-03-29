using Maths;
using NDatas;
using NInventory;
using NPokemon;
using NScene;
using NTrainer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Media;

namespace NGlobal
{
    public class Global
    {

        public static bool IsInBattle = false; /// <summary> If the player is in battle </summary>
        public static bool IsWildFight = true; /// <summary> If the battle is battle with a wild pokemon </summary>

        public static int Pokeballs = 100;

        public static Attack DefaultAttack = new Attack("Def", "Normal", "Physical", 10, 100, 999);
        /// <summary> List of the pokemons of the player </summary>
        public static List<Pokemon> PlayerPokemons = new List<Pokemon>() {
            new Pokemon("1", "Jarod", new List<string> { "Water" }, 20, 20, 20, 20, 20, 20, 10),
            new Pokemon("3", "Francois", new List<string> { "Fire" }, 10, 10, 10, 10, 10, 10, 5)
        };


        public static string map { get; set; } = "Map1";

        public static bool passTrainers = true;
        public static bool capturer { get; set; } = false;
        public static bool firstFight { get; set; } = false;
        public static bool foundLigue { get; set; } = false;

        public static Dictionary<string, ItemAbstract> PlayerItems = new Dictionary<string, ItemAbstract>();

        /// <summary> List of trainers available </summary>
        public static Dictionary<string, List<Trainer>> AllTrainers = new Dictionary<string, List<Trainer>>();

        public static Trainer actualTrainer { get; set; }

        public static List<Trainer> Map1Trainers = new List<Trainer>()
        {
            new Trainer("D","Maximilien premier le frero", new Vector2i(10, 4), new List<Pokemon>()
            {
                new Pokemon("1", "Jarod", new List<string> { "Water" }, 40, 20, 20, 20, 20, 20, 10)
            }),

            new Trainer("D","Maximilien second le frero", new Vector2i(10, 6), new List<Pokemon>()
            {
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 50, 10, 10, 10, 10, 10, 5),
            })
        };

        public static List<Trainer> Map2Trainers = new List<Trainer>()
        {
            new Trainer("D","Jean Baptout le fragil", new Vector2i(36, 21), new List<Pokemon>()
            {
                new Pokemon("1", "Jarod", new List<string> { "Water" }, 60, 20, 20, 20, 20, 20, 10),
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 100, 10, 10, 10, 10, 10, 5),
            }),

            new Trainer("D","Shanky le disparu", new Vector2i(36, 22), new List<Pokemon>()
            {
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 30, 10, 10, 10, 10, 10, 5),
            }),

            new Trainer("D", "Maurad le dormeur", new Vector2i(36, 23), new List<Pokemon>()
            {
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 30, 10, 10, 10, 10, 10, 5),
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 30, 10, 10, 10, 10, 10, 5),
                new Pokemon("3", "Francois", new List<string> { "Fire" }, 30, 10, 10, 10, 10, 10, 5),
            }),

            new Trainer("D", "Jarod le fou", new Vector2i(18, 10), new List<Pokemon>()
            {
                new Pokemon("12", "TrollFlame", new List<string> { "Fire" }, 50, 70, 25, 55, 55, 60, 5),
            }),

            new Trainer("D", "Francois le Tyran", new Vector2i(10, 20), new List<Pokemon>()
            {
                new Pokemon("10", "GigaGamer", new List<string> { "Steel" }, 90, 110, 100, 80, 90, 95, 10),
            }),

            new Trainer("D", "Quentin 21", new Vector2i(10, 25), new List<Pokemon>()
            {
                new Pokemon("9", "Memeosaur", new List<string> { "Dragon","Psychic" }, 80, 100, 120, 70, 80, 100, 10),
            })
        };

        public static List<Trainer> League1Trainer = new List<Trainer>()
        {
            new Trainer("D", "Maximilien premier le frero", new Vector2i(9, 5), new List<Pokemon>()
            {
                new Pokemon("10", "GigaGamer", new List<string> { "Steel" }, 90, 110, 100, 80, 90, 95, 10),
                new Pokemon("7", "MystiWitch", new List<string> { "Ghost","Fairy" }, 85, 75, 95, 65, 85, 110, 10),
                new Pokemon("9", "Memeosaur", new List<string> { "Dragon","Psychic" }, 80, 100, 120, 70, 80, 100, 10)
            })
        };

        public static List<Trainer> League2Trainer = new List<Trainer>()
        {
            new Trainer("D", "Maximilien premier le frero", new Vector2i(9, 5), new List<Pokemon>()
            {
                new Pokemon("6", "Aurora", new List<string> { "Ice" }, 70, 60, 65, 55, 50, 45, 5),
                new Pokemon("25", "Lucas", new List<string> { "Psychic" }, 60, 40, 80, 50, 60, 55, 10),
                new Pokemon("0", "Sylvia", new List<string> { "Electric" }, 45, 65, 70, 40, 55, 85, 10)
            })
        };

        public static List<Trainer> League3Trainer = new List<Trainer>()
        {
            new Trainer("D", "Maximilien premier le frero", new Vector2i(9, 5), new List<Pokemon>()
            {
                new Pokemon("29", "SonicBolt", new List<string> { "Normal" }, 75, 85, 110, 55, 60, 140, 5),
                new Pokemon("29", "SonicBolt", new List<string> { "Normal" }, 75, 85, 110, 55, 60, 140, 5),
            })
        };

        public static List<Trainer> League4Trainer = new List<Trainer>()
        {
            new Trainer("D", "Maximilien premier le frero", new Vector2i(9, 5), new List<Pokemon>()
            {
                new Pokemon("12", "TrollFlame", new List<string> { "Fire" }, 50, 70, 25, 55, 55, 60, 5),
                new Pokemon("14", "DogeVine", new List<string> { "Grass" }, 35, 35, 90, 30, 30, 100, 10),
                new Pokemon("16", "PikaPool", new List<string> { "Electric","Fairy" }, 70, 80, 90, 60, 70, 120, 10)
            })
        };

        public static List<Trainer> League5Trainer = new List<Trainer>()
        {
            new Trainer("D", "Maximilien premier le frero", new Vector2i(9, 5), new List<Pokemon>()
            {
                new Pokemon("20", "PixelKnight", new List<string> { "Dark","Steel" }, 95, 120, 85, 85, 100, 80, 10),
                new Pokemon("23", "Sonicachu", new List<string> { "Electric","Normal" }, 75, 85, 110, 55, 60, 140, 10),
                new Pokemon("17", "MetalMage", new List<string> { "Steel", "Psychic" }, 90, 110, 100, 80, 90, 95, 15),
                new Pokemon("25", "Minecraftian", new List<string> { "Ground" }, 55, 60, 30, 50, 65, 70, 10)
            })
        };

        public static bool isLoadedTrainers = false;
        public static void LoadTrainers()
        {
            if (!isLoadedTrainers)
            {
                AllTrainers.Add("Map1", Map1Trainers);
                AllTrainers.Add("Map2", Map2Trainers);
                AllTrainers.Add("League1", League1Trainer);
                AllTrainers.Add("League2", League2Trainer);
                AllTrainers.Add("League3", League3Trainer);
                AllTrainers.Add("League4", League4Trainer);
                AllTrainers.Add("League5", League5Trainer);
                isLoadedTrainers = true;
            }

        }

        /// <summary> List of the pokemons of the enemy </summary>
        public static List<Pokemon> EnemyPokemons = new List<Pokemon>() { new Pokemon("3", "Maurad", new List<string> { "Grass" }, 20, 20, 20, 20, 20, 20, 10) };

        /// <summary> Paths to look for the assets </summary>
        public static string TXTArtCollectionPath = "./Assets/__ASCII Text-Art Collection/";/// <summary> Paths to look for the assets </summary>
        public static string TXTAttacksPath = "./Assets/TXT_files_Attacks/";/// <summary> Paths to look for the assets </summary>
        public static string TXTCharactersPath = "./Assets/TXT_files_Characters/";/// <summary> Paths to look for the assets </summary>
        public static string TXTDressersPath = "./Assets/TXT_files_Dressers/";/// <summary> Paths to look for the assets </summary>
        public static string TXTGeneralPath = "./Assets/TXT_files_General/";/// <summary> Paths to look for the assets </summary>

        // public Dictionary PlayerItems = { "Pokeball":{"Num":100} }
        // public List<Pokemon_Class> PC

        // public List<Pokemon_Class> EnemyPokemons

        /// <summary> Enum of all the pokemontypes </summary>
        public enum PokemonType
        {
            Normal, Fire, Water, Grass, Electric, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Steel, Dark, Fairy, Dragon, Unknown
        }

        /// <summary> Chart containing all the types things </summary>
    	public static float[,] Chart { get; } = new float[18, 19]
        {
    	    //               Normal  Fire  Water  Grass  Electric Ice   Fighting  Poison  Ground  Flying  Psychic  Bug   Rock   Ghost  Steel  Dark  Fairy  Dragon Unknown
    	    /* Normal */    {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    1.0f, 1.0f,  0.0f,  0.5f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Fire */      {1.0f,  0.5f, 0.5f,  2.0f,  1.0f,    2.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    2.0f, 0.5f,  1.0f,  2.0f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Water */     {1.0f,  2.0f, 0.5f,  0.5f,  1.0f,    1.0f,  1.0f,    1.0f,   2.0f,   1.0f,   1.0f,    1.0f, 2.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Grass */     {1.0f,  0.5f, 2.0f,  0.5f,  1.0f,    1.0f,  1.0f,    0.5f,   2.0f,   0.5f,   1.0f,    2.0f, 0.5f,  1.0f,  0.5f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Electric */  {1.0f,  1.0f, 2.0f,  0.5f,  0.5f,    1.0f,  1.0f,    1.0f,   0.0f,   2.0f,   1.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 1.0f,  0.5f,  0.0f },
    	    /* Ice */       {1.0f,  0.5f, 0.5f,  2.0f,  1.0f,    0.5f,  1.0f,    1.0f,   2.0f,   2.0f,   1.0f,    1.0f, 2.0f,  1.0f,  0.5f,  1.0f, 1.0f,  2.0f,  0.0f },
    	    /* Fighting */  {2.0f,  1.0f, 1.0f,  1.0f,  1.0f,    2.0f,  1.0f,    0.5f,   1.0f,   0.5f,   0.5f,    0.5f, 2.0f,  0.0f,  1.0f,  2.0f, 2.0f,  0.5f,  0.0f },
    	    /* Poison */    {1.0f,  1.0f, 1.0f,  2.0f,  1.0f,    1.0f,  1.0f,    0.5f,   0.5f,   1.0f,   1.0f,    1.0f, 0.5f,  0.5f,  1.0f,  1.0f, 2.0f,  1.0f,  0.0f },
    	    /* Ground */    {1.0f,  2.0f, 1.0f,  0.5f,  2.0f,    1.0f,  1.0f,    2.0f,   1.0f,   0.0f,   1.0f,    0.5f, 2.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Flying */    {1.0f,  1.0f, 1.0f,  2.0f,  0.5f,    2.0f,  2.0f,    1.0f,   1.0f,   1.0f,   1.0f,    0.5f, 1.0f,  1.0f,  1.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Psychic */   {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  2.0f,    2.0f,   1.0f,   1.0f,   0.5f,    1.0f, 1.0f,  1.0f,  1.0f,  0.0f, 1.0f,  1.0f,  0.0f },
    	    /* Bug */       {1.0f,  2.0f, 1.0f,  0.5f,  1.0f,    1.0f,  0.5f,    1.0f,   0.5f,   0.5f,   2.0f,    1.0f, 1.0f,  0.5f,  1.0f,  2.0f, 0.5f,  1.0f,  0.0f },
    	    /* Rock */      {1.0f,  0.5f, 2.0f,  2.0f,  1.0f,    2.0f,  0.5f,    0.5f,   2.0f,   0.5f,   1.0f,    2.0f, 1.0f,  1.0f,  2.0f,  1.0f, 1.0f,  1.0f,  0.0f },
    	    /* Ghost */     {0.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  1.0f,    1.0f,   1.0f,   1.0f,   2.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 0.5f,  1.0f,  0.0f },
    	    /* Steel */     {0.5f,  2.0f, 1.0f,  0.5f,  0.5f,    2.0f,  1.0f,    1.0f,   2.0f,   0.5f,   0.5f,    0.5f, 2.0f,  1.0f,  0.5f,  1.0f, 0.5f,  0.5f,  0.0f },
    	    /* Dark */      {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  2.0f,    1.0f,   1.0f,   1.0f,   2.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 0.5f,  1.0f,  0.0f },
    	    /* Fairy */     {1.0f,  1.0f, 1.0f,  1.0f,  1.0f,    1.0f,  0.5f,    2.0f,   1.0f,   1.0f,   1.0f,    0.5f, 1.0f,  1.0f,  2.0f,  1.0f, 2.0f,  2.0f,  0.0f },
    	    /* Dragon */    {1.0f,  1.0f, 1.0f,  1.0f,  0.5f,    2.0f,  1.0f,    1.0f,   1.0f,   1.0f,   1.0f,    1.0f, 1.0f,  1.0f,  1.0f,  1.0f, 2.0f,  2.0f,  0.0f }
        };

        /// <summary> For the calculation of the type </summary>
		public static PokemonType TypeToIndex(string typeName)
        {
            if (Enum.TryParse(typeName, out PokemonType pokemonType))
            {
                return pokemonType;
            }
            else { return PokemonType.Unknown; }
        }

        /// <summary> For the window text color, to return the color based on the type </summary>
		public static SolidColorBrush TypeToColor(string typeName)
        {
            if (typeName == "Water") { return Brushes.Blue; }
            else if (typeName == "Fire") { return Brushes.Red; }
            else if (typeName == "Grass") { return Brushes.Green; }
            else if (typeName == "Electric") { return Brushes.Yellow; }
            else if (typeName == "Normal") { return Brushes.Gray; }
            else if (typeName == "Ice") { return Brushes.Cyan; }
            else if (typeName == "Fighting") { return Brushes.Brown; }
            else if (typeName == "Poison") { return Brushes.Violet; }
            else if (typeName == "Ground") { return Brushes.SandyBrown; }
            else if (typeName == "Flying") { return Brushes.AliceBlue; }
            else if (typeName == "Psychic") { return Brushes.DeepPink; }
            else if (typeName == "Bug") { return Brushes.GreenYellow; }
            else if (typeName == "Rock") { return Brushes.SaddleBrown; }
            else if (typeName == "Ghost") { return Brushes.DarkViolet; }
            else if (typeName == "Steel") { return Brushes.SteelBlue; }
            else if (typeName == "Dark") { return Brushes.DarkGray; }
            else if (typeName == "Fairy") { return Brushes.Pink; }
            else if (typeName == "Dragon") { return Brushes.LightBlue; }
            else { return Brushes.Gray; }
        }

        /// <summary> Gives the color the console should be based on the type </summary>
		public static ConsoleColor TypeToConsoleColor(string typeName)
        {
            if (typeName == "Water") { return ConsoleColor.Blue; }
            else if (typeName == "Fire") { return ConsoleColor.Red; }
            else if (typeName == "Grass") { return ConsoleColor.Green; }
            else if (typeName == "Electric") { return ConsoleColor.Yellow; }
            else if (typeName == "Normal") { return ConsoleColor.Gray; }
            else if (typeName == "Ice") { return ConsoleColor.Cyan; }
            else if (typeName == "Fighting") { return ConsoleColor.DarkRed; }
            else if (typeName == "Poison") { return ConsoleColor.Magenta; }
            else if (typeName == "Ground") { return ConsoleColor.DarkGreen; }
            else if (typeName == "Flying") { return ConsoleColor.Gray; }
            else if (typeName == "Psychic") { return ConsoleColor.DarkMagenta; }
            else if (typeName == "Bug") { return ConsoleColor.DarkGreen; }
            else if (typeName == "Rock") { return ConsoleColor.DarkRed; }
            else if (typeName == "Ghost") { return ConsoleColor.DarkMagenta; }
            else if (typeName == "Steel") { return ConsoleColor.Gray; }
            else if (typeName == "Dark") { return ConsoleColor.DarkGray; }
            else if (typeName == "Fairy") { return ConsoleColor.Magenta; }
            else if (typeName == "Dragon") { return ConsoleColor.Gray; }
            return ConsoleColor.White;
        }

        /// <summary> To color using an int </summary>
        public static ConsoleColor IntToConsoleColor(int x)
        {
            if (x == 1) { return ConsoleColor.Black; }
            else if (x == 2) { return ConsoleColor.Blue; }
            else if (x == 3) { return ConsoleColor.Cyan; }
            else if (x == 4) { return ConsoleColor.DarkBlue; }
            else if (x == 5) { return ConsoleColor.DarkCyan; }
            else if (x == 6) { return ConsoleColor.DarkGray; }
            else if (x == 7) { return ConsoleColor.DarkGreen; }
            else if (x == 8) { return ConsoleColor.DarkMagenta; }
            else if (x == 9) { return ConsoleColor.DarkRed; }
            else if (x == 10) { return ConsoleColor.DarkYellow; }
            else if (x == 11) { return ConsoleColor.Gray; }
            else if (x == 12) { return ConsoleColor.Green; }
            else if (x == 13) { return ConsoleColor.Magenta; }
            else if (x == 14) { return ConsoleColor.Red; }
            else if (x == 15) { return ConsoleColor.White; }
            else if (x == 16) { return ConsoleColor.Yellow; }
            return ConsoleColor.White;
        }

        /// <summary> For the console, to get the color in a rainbow order </summary>
        public static List<ConsoleColor> rainbowColors = new List<ConsoleColor>
        {
            ConsoleColor.Red,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkMagenta
        };

        /// <summary> Return if the attack failed or not </summary>
        public static bool SuccessAcc(int Acc)
        {
            Random rnd = new Random();
            int Num = rnd.Next(1, 100);

            if (Num > Acc)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary> Returns the int of the critical </summary>
		public static int SuccessCritical(int Speed)
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 100);
            int Chance = Speed / 512;
            if (random > Chance * 100) { return 1; }
            else { return 2; }
        }

        /// <summary> calculates the stab of the pokemon </summary>
		public static float StabCalculator(string[] PokemonTypes, string AttackType)
        {
            if (PokemonTypes.Contains(AttackType)) { return 1.5f; }
            else { return 1.0f; }
        }

        /// <summary> The damage calculation  </summary>

        public static int DamageCalculator(Pokemon P1, Pokemon P2, Attack MoveP1, int Critical)
        {


            // MOVE VARIABLES
            int MovePower = MoveP1.GetPower();
            string MoveType = MoveP1.GetType();
            string MoveCat = MoveP1.GetCat();

            // P1 VARIABLES
            int P1Attack = P1.GetAttack();
            int P1AttackSpe = P1.GetAttackSpe();
            int P1Level = P1.GetLevel();
            List<string> P1Types = P1.GetTypes();
            // P2 VARIABLES
            int P2Defense = P2.GetDefense();
            int P2DefenseSpe = P2.GetDefenseSpe();
            List<string> P2Types = P2.GetTypes();


            // COMBAT VARIABLES
            float stab;
            float type1;
            float type2;
            float random;

            string PokemonTypes1 = "Normal";
            string AttackType1 = "Normal";
            int critical = Critical;

            type1 = Chart[(int)TypeToIndex(AttackType1), (int)TypeToIndex(PokemonTypes1)];

            if (P2Types.Count() > 2) { type2 = Chart[(int)TypeToIndex(MoveType), (int)TypeToIndex(P2Types[1])]; }
            else { type2 = 1.0f; }
            random = 1;
            stab = StabCalculator(P2Types.ToArray(), MoveType);
            int Damage = 1;

            if (MoveCat == "Physical") { Damage = (int)Math.Floor(((((2 * P1Level / 5) + 2) * MovePower * (P1Attack / P2Defense) / 50 * Critical) + 2) * stab * type1 * type2 * random); }
            else { Damage = (int)Math.Floor(((((2 * P1Level / 5) + 2) * MovePower * (P1AttackSpe / P2DefenseSpe) / 50 * Critical) + 2) * stab * type1 * type2 * random); }
            return Damage;
        }

        /// <summary> Calculates the catch rate of the pokemon, if it's able to catch it or not.</summary>
        public static bool Cath(Pokemon PokemonToCatch)
        {
            Random rnd = new Random();

            int rate = 1;
            int BonusBall = 1;
            int BonusStatus = 1;
            int a = ((3 * PokemonToCatch.GetMaxHp()) - (2 * PokemonToCatch.GetHp())) / 3 * PokemonToCatch.GetMaxHp() * rate * BonusBall * BonusStatus;
            return rnd.Next() > a;
        }

        /// <summary> Odd of a pokemon to escape </summary>
		public static bool OddsEscape(int SpeedP, int SpeedW)

		{
			Random rnd = new Random();
			int Attempts = 1;
			int OddEscape = ( SpeedP*32/(SpeedW/4 % 256 ) ) + 30 * Attempts;
			if (OddEscape < 255){
				if (rnd.Next(1,255) < OddEscape){return true;}
				else{ return false;}
			}
			else{return true;}
		}


        /// <summary> Change the order of 2 pokemons in a team </summary>
		public static void ChangePokemonOrder(List<Pokemon> PokemonList, int x, int y)
        {
            Pokemon X = PokemonList[x];
            Pokemon Y = PokemonList[y];

            PokemonList[x] = Y;
            PokemonList[y] = X;
        }

        /// <summary> Heals all the pokemons of the player </summary> <param name="PokemonList"></param>
        public static void HealTeamPokemon(List<Pokemon> PokemonList)
        {
            foreach (Pokemon i in PokemonList) { i.Restore(); }
        }

        /// <summary> After a fight it make all the stats go to normal, expect the hp </summary>
        public static void AfterFightTeamPokemon(List<Pokemon> PokemonList)
        {
            foreach (Pokemon i in PokemonList) { i.AfterFight(); }
        }


        /// <summary> Read the pokemon in the dictionary datas </summary>
    	public static Pokemon ReadPokemonDatas(string nameP, int level = 1)
        {
            Console.WriteLine("Name: " + nameP);

            Dictionary<string, object> P = PokemonsData.pokemons[nameP];
            List<string> PTypes = new List<string>();
            foreach (string type in (List<object>)P["Types"])
            {
                PTypes.Add(type);
            }

            return new Pokemon("0", (string)P["Name"], PTypes, (int)P["Hp"], (int)P["Att"], (int)P["AttSpe"], (int)P["Def"], (int)P["DefSpe"], (int)P["Speed"], level);
        }

        /// <summary> Read the datas of the attacks </summary>
    	public static Attack ReadAttacksDatas(string nameA)
        {
            Dictionary<string, object> A = AttacksDic.attacks[nameA];
            return new Attack((string)A["Name"], (string)A["Type"], (string)A["Cat"], (int)A["Power"], (int)A["Acc"], (int)A["Pp"]);
        }

        /// <summary> Read the file and returns it as a list of string containing all the lines of the file </summary>
		public static List<string> ReadFilesText(string filepath)
        {
            List<string> lines = new List<string>();
            FileStream file = File.OpenRead(filepath);
            StreamReader reader = new StreamReader(filepath);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    Console.WriteLine("Error reading the line");
                }
                lines.Add(line);
            }
            return lines;
        }

        /// <summary> Function to generate a random number using RNGCryptoServiceProvider, parce que le random utilise la m�me seed </summary>
        public static int GenerateRandomNumber(RNGCryptoServiceProvider rng, int minValue, int maxValue)
        {
            byte[] randomNumber = new byte[4];
            rng.GetBytes(randomNumber);

            int value = BitConverter.ToInt32(randomNumber, 0);

            return Math.Abs(value % (maxValue - minValue + 1)) + minValue;
        }

        /// <summary> Write sprites, text, with a color, a position </summary>
        public static void WriteSprites(List<string> sprite, int Position = 0, int color = 0, bool all = true)
        {
            int leftPosition = 0;
            int topPosition = Console.CursorTop;
            foreach (string line in sprite)
            {
                switch (Position)
                {
                    case 0:
                        leftPosition = 0;
                        topPosition = Console.CursorTop;
                        break;

                    case 1:
                        leftPosition = 5;
                        topPosition = Console.CursorTop;
                        break;

                    case 2: // Center
                        if ((int)Math.Ceiling((decimal)(Console.WindowWidth / 2)) - (sprite.Max().Length / 2) < 0)
                        {
                            leftPosition = 0;
                            topPosition = Console.CursorTop;
                        }
                        else
                        {
                            leftPosition = Console.WindowWidth - sprite.Max().Length - 2;
                            topPosition = Console.CursorTop;
                        }
                        break;

                    case 3: // Right (normalement)
                        if ((int)Math.Ceiling((decimal)(Console.WindowWidth / 2)) - (sprite.Max().Length / 2) < 0)
                        {
                            leftPosition = 0;
                            topPosition = Console.CursorTop;
                        }
                        else
                        {
                            leftPosition = (Console.WindowWidth / 2) - (sprite.Max().Length / 2);
                            topPosition = Console.CursorTop;
                        }
                        break;
                }

                Console.SetCursorPosition(leftPosition, topPosition);
                int x = 0;
                int linesize = line.Length;
                int segmentSize = (linesize / rainbowColors.Count) - 1;
                foreach (char c in line)
                {
                    switch (color)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 1:
                            if (all)
                            {
                                var rng = new RNGCryptoServiceProvider();
                                Console.ForegroundColor = IntToConsoleColor(GenerateRandomNumber(rng, 1, 16));
                            }
                            else
                            {
                                if (c != line[0] && c != line.Last() && sprite[0] != line && sprite.Last() != line && c != line[line.Count() - 3])
                                {
                                    var rng = new RNGCryptoServiceProvider();
                                    Console.ForegroundColor = IntToConsoleColor(GenerateRandomNumber(rng, 1, 16));
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            break;

                        case 2:
                            segmentSize = linesize / rainbowColors.Count;
                            int colorIndex = x / segmentSize;
                            Console.ForegroundColor = rainbowColors[Math.Min(colorIndex, rainbowColors.Count - 1)];

                            // al�atoire couleurs si efficace
                            // Si critique rainbow not all
                            // Si critique et efficace, rainbow all

                            // Pareils pour les textes critique etc ? 

                            break;
                    }
                    Console.Write(c);
                    x++;
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary> Get the file x in a directory </summary>
        public static string GetFileAtIndex(string folderPath, int index)
        {
            try
            {
                // Obtenir la liste des fichiers dans le dossier
                string[] files = Directory.GetFiles(folderPath);

                // R�cup�rer le fichier correspondant � l'index
                string fileToAccess = files.ElementAtOrDefault(index);

                return fileToAccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la recherche des fichiers : {ex.Message}");
                return null;
            }
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        public static void ClearLines(int defaultTop)
        {
            int Cursor = Console.CursorTop;
            do
            {
                Cursor--;
                Console.SetCursorPosition(0, Cursor);
                Console.Write(new string(' ', Console.WindowWidth));
            } while (defaultTop < Cursor);
            Console.SetCursorPosition(0, Cursor);
        }
 
    }
}

/*
func StartWildFight(body) -> void:
	#var ListPokemonsWild: Array[Dictionary] = [Pokemons.maurad,Pokemons.francois,Pokemons.jean]
	var ListPokemonsWild: Array[Dictionary] = [Pokemons.XPFARM,Pokemons.XPFARM,Pokemons.XPFARM]
	var EP = ListPokemonsWild[randi_range(0,ListPokemonsWild.size()-1)] #Enemy Pokemon
	EnemyPokemons.clear()

	var EPTypes:Array[String]
	for i in EP.Types:
		EPTypes.append(i)
	
	#var EP2 = ListPokemonsWild[randi_range(0,ListPokemonsWild.size()-1)] #Enemy Pokemon
	#var EPTypes2:Array[String]
	#for i in EP2.Types:
	#	EPTypes2.append(i)
	
	EnemyPokemons.append(Pokemon_Class.new(EP.Name,EPTypes,EP.Hp,EP.Att,EP.AttSpe,EP.Def,EP.DefSpe,EP.Speed,randi_range(1,3)))
	EnemyPokemons[0].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	
	#EnemyPokemons.append(Pokemon_Class.new(EP2.Name,EPTypes2,EP2.Hp,EP2.Att,EP2.AttSpe,EP2.Def,EP2.DefSpe,EP2.Speed,randi_range(1,3)))
	#EnemyPokemons[1].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	
	get_tree().root.get_node("/root/Battle").get_child(0).StartBattle(PlayerPokemons,EnemyPokemons, body)

func StartFight(body,Pokemon:Array[String],PokemonLevel:Array[int],npc) -> void:
	EnemyPokemons.clear()
	for i:int in range(Pokemon.size()):
		EnemyPokemons.append(ReadPokemonsDatas(Pokemon[i],PokemonLevel[i]))
	for i:int in range(EnemyPokemons.size()):
		EnemyPokemons[i].Moves.append(Attack_Class.new("Charge","Normal","Physical",20,100,30))

	#EnemyPokemons.append(Pokemon_Class.new(EP.Name,EPTypes,EP.Hp,EP.Att,EP.AttSpe,EP.Def,EP.DefSpe,EP.Speed,PokemonLevel))
	#EnemyPokemons[0].Moves.append(Attack_Class.new("Count","Grass","Physical",70,80,10))
	get_tree().root.get_node("/root/Battle").get_child(0).StartBattle(PlayerPokemons,EnemyPokemons, body,npc,false)

*/