using Map;
using Maths;
using NEngine;
using NEntity;
using Newtonsoft.Json;
using NGlobal;
using NModules;
using NPokemon;
using NScene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace NScene
{
    public class MapScene : SceneAbstract
    {
        private void GetTiles(string _tile)
        {

            if (_tile == "T")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(" ♣ ");
            }
        }

        private enum Actions
        {
            MOVING,
            MENU
        }

        private enum MenuActions
        {
            INVENTORY,
            FIGHT,
            STATS,
            LEAVE
        }

        private Actions currentAction = Actions.MOVING;
        private MenuActions selectedMenuAction = MenuActions.INVENTORY;

        private string playerCharacter = "☺";
        private Vector2i playerPosition = new Vector2i(1, 1);
        bool enemyFound = false;

        private Dictionary<string, Vector2i> enemy1 = new Dictionary<string, Vector2i>();

        List<Dictionary<string, Vector2i>> enemies = new List<Dictionary<string, Vector2i>>();
        
        private string[,] map;
        private int height = 0;
        private int width = 0;

        public MapScene() : base("MapScene") { }

        public override void Init()
        {
            Console.OutputEncoding = Encoding.UTF8;

            enemy1.Add("D", new Vector2i(10, 2));

            enemies.Add(enemy1);
        }

        public override void Launch() {
            enemyFound = false;
            DisplayMap();
            if (enemyFound) {
                Console.Clear();
                return;
            }
            DisplaySelection();
            HandleInput();

            Console.Clear();
        }

        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            switch (currentAction)
            {
                case Actions.MOVING:
                    if (key.Key == ConsoleKey.DownArrow && playerPosition.GetY() < height - 2)
                    {
                        playerPosition.SetY(playerPosition.GetY() + 1);
                    }
                    else if (key.Key == ConsoleKey.UpArrow && playerPosition.GetY() > 1)
                    {
                        playerPosition.SetY(playerPosition.GetY() - 1);
                    }
                    else if (key.Key == ConsoleKey.LeftArrow && playerPosition.GetX() > 1)
                    {
                        playerPosition.SetX(playerPosition.GetX() - 1);
                    }
                    else if (key.Key == ConsoleKey.RightArrow && playerPosition.GetX() < width - 2)
                    {
                        playerPosition.SetX(playerPosition.GetX() + 1);
                    }

                    if (key.Key == ConsoleKey.Escape)
                    {
                        currentAction = Actions.MENU;
                    }
                    break;

                case Actions.MENU:

                    int ActionCount = Enum.GetNames(typeof(MenuActions)).Length;
                    if (key.Key == ConsoleKey.Escape)
                    {
                        currentAction = Actions.MOVING;
                    }

                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        selectedMenuAction += 1;
                        if ((int)selectedMenuAction > ActionCount - 1)
                        {
                            selectedMenuAction = 0;
                        }
                    }

                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        selectedMenuAction -= 1;
                        if ((int)selectedMenuAction < 0)
                        {
                            selectedMenuAction = (MenuActions)ActionCount - 1;
                        }
                    }

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        selectedMenuAction += 2;
                        if ((int)selectedMenuAction > ActionCount - 1)
                        {
                            selectedMenuAction = selectedMenuAction - 4;
                        }
                    }

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        selectedMenuAction -= 2;
                        if ((int)selectedMenuAction < 0)
                        {
                            selectedMenuAction = selectedMenuAction + 4;
                        }
                    }

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        switch(selectedMenuAction)
                        {
                            case MenuActions.LEAVE:
                            {
                                Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>(true);
                                break;
                            }
                        }
                    }

                    break;
            }
        }

        private void DisplayMap()
        {
            map = new string[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    map[i, j] = null;

                    foreach (var entry in enemies)
                    {
                        foreach (KeyValuePair<string, Vector2i> enemy in entry)
                        {
                            if (enemy.Value.GetX() == j && enemy.Value.GetY() == i)
                            {
                                if (playerPosition.GetX() == enemy.Value.GetX() && playerPosition.GetY() == enemy.Value.GetY())
                                {

                                    enemyFound = true;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                                }
                                else
                                {
                                    map[i, j] = " " + enemy.Key + " ";
                                }
                            }
                        }
                    }

                    if (playerPosition.GetX() == j && playerPosition.GetY() == i)
                    {
                        Console.Write(" " + playerCharacter + " ");
                    }
                    else if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        GetTiles("T");
                    }
                    else
                    {
                        Console.Write(map[i, j]);
                    }
                }
                Console.Write("\n");
            }

        }

        private void DisplaySelection()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\n");
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║                                    ║");

            if (currentAction == Actions.MENU)
            {
                if (selectedMenuAction == MenuActions.INVENTORY)
                {
                    Console.WriteLine("║ > INVENTAIRE <           SE BATTRE ║");
                }
                else if (selectedMenuAction == MenuActions.FIGHT)
                {
                    Console.WriteLine("║ INVENTAIRE           > SE BATTRE < ║");
                }
                else
                {
                    Console.WriteLine("║ INVENTAIRE               SE BATTRE ║");
                }

                Console.WriteLine("║                                    ║");

                if (selectedMenuAction == MenuActions.STATS)
                {
                    Console.WriteLine("║ > STATS <                  QUITTER ║");
                }
                else if (selectedMenuAction == MenuActions.LEAVE)
                {
                    Console.WriteLine("║ STATS                  > QUITTER < ║");
                }
                else
                {
                    Console.WriteLine("║ STATS                      QUITTER ║");
                }
            } else
            {
                Console.WriteLine("║ INVENTAIRE               SE BATTRE ║");
                Console.WriteLine("║                                    ║");
                Console.WriteLine("║ STATS                      QUITTER ║");
            }

            Console.WriteLine("║                                    ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("\n");
        }

        private void LoadMap(JsonReader reader, Type objectType, Player existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Avancer vers le début de l'objet JSON
            reader.Read();

            // Lire les propriétés de l'objet JSON
            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = reader.Value.ToString();

                switch (propertyName)
                {
                    case "Tiles":
                        reader.Read();
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;

                                map[i, j] = " " + serializer.Deserialize<List<string>>(reader)[i*width + j] + " ";
                                break;
                            }
                        }
                        break;


                    case "Size":
                        reader.Read();
                        map = new string[serializer.Deserialize<List<int>>(reader)[0], serializer.Deserialize<List<int>>(reader)[1]];
                        break;

                    default:
                        // Ignorer les propriétés inconnues si nécessaire
                        reader.Read();
                        reader.Skip();
                        break;
                }
                // Passer à la propriété suivante
                reader.Read();
            }
        }
    }
    }
}