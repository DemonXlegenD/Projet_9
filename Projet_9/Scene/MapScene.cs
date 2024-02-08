using Maths;
using NEngine;
using NEntity;
using Newtonsoft.Json;
using NModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NScene
{
    public class MapScene : SceneAbstract
    {
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
        private bool stop = false;

        private Dictionary<string, Vector2i> enemy1 = new Dictionary<string, Vector2i>();

        List<Dictionary<string, Vector2i>> enemies = new List<Dictionary<string, Vector2i>>();
        
        private string[,] map;
        private int height = 0;
        private int width = 0;
        private List<string> collidable = new List<string>() { "C", "T", "W" };
        private List<string> trainer = new List<string>() { "D" };
        public MapScene() : base("MapScene") { }

        public override void Init()
        {
            Console.OutputEncoding = Encoding.UTF8;

            LoadMap("Map1");

            enemy1.Add("D", new Vector2i(10, 2));

            enemies.Add(enemy1);
        }

        
        private void GetTiles(string tile, bool display)
        {
            string character = "   ";

            if (tile == "T")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                if (display)
                {

                }
                character = " ♣ ";
            }
            else if (tile == "C")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
                character = " ■ ";
            }
            else if (tile == "G")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                character = "   ";
            }

            if (display)
            {
                Console.Write(character);
            }

            return;
        }

        public override void Launch() {
            stop = false;
            DisplayMap();
            DisplaySelection();
            do
            {
                HandleInput();
            } while (!stop);
            

            Console.Clear();
        }

        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            switch (currentAction)
            {
                case Actions.MOVING:
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (!collidable.Contains(map[playerPosition.GetY() + 1, (playerPosition.GetX())]))
                        {
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], true);
                            playerPosition.SetY(playerPosition.GetY() + 1);
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], false);
                            Console.Write(" " + playerCharacter + " ");
                        }
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (!collidable.Contains(map[playerPosition.GetY() - 1, playerPosition.GetX()]))
                        {
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], true);
                            playerPosition.SetY(playerPosition.GetY() - 1);
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], false);
                            Console.Write(" " + playerCharacter + " ");
                        }
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (!collidable.Contains(map[playerPosition.GetY(), playerPosition.GetX() - 1]))
                        {
                            
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], true);
                            playerPosition.SetX(playerPosition.GetX() - 1);
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], false);
                            Console.Write(" " + playerCharacter + " ");
                        }
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (!collidable.Contains(map[playerPosition.GetY(), playerPosition.GetX() + 1]))
                        {
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], true);
                            playerPosition.SetX(playerPosition.GetX() + 1);
                            Console.SetCursorPosition(playerPosition.GetX() * 3, playerPosition.GetY());
                            GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], false);
                            Console.Write(" " + playerCharacter + " ");

                        }
                    }
                    foreach (var entry in enemies)
                    {
                        foreach (KeyValuePair<string, Vector2i> enemy in entry)
                        {
                            if (enemy.Value.GetX() == playerPosition.GetX() && enemy.Value.GetY() == playerPosition.GetY())
                            {
                                stop = true;
                                Console.Clear();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                            }
                            
                        }
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
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    var enemyTile = false;

                    foreach (var entry in enemies)
                    {
                        foreach (KeyValuePair<string, Vector2i> enemy in entry)
                        {
                            if (enemy.Value.GetX() == j && enemy.Value.GetY() == i)
                            {  
                                GetTiles(map[i, j], false);
                                Console.Write(" " + enemy.Key + " ");
                                enemyTile = true;
                            }
                        }
                    }

                    if (!enemyTile)
                    {
                        if (playerPosition.GetX() == j && playerPosition.GetY() == i)
                        {
                            GetTiles(map[i, j], false);
                            Console.Write(" " + playerCharacter + " ");
                        }
                        else
                        {
                            GetTiles(map[i, j], true);
                        }
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

        private void LoadMap(string _map)
        {
            string jsonContent = System.IO.File.ReadAllText("Data/Maps/" + _map + ".json");
            var deserializedObject = JsonConvert.DeserializeAnonymousType(jsonContent, new { Tiles = new string[0], Size = new int[0] });

            Console.WriteLine(deserializedObject.Size[0]);
            //System.Threading.Thread.Sleep(10000);
            width = deserializedObject.Size[0];
            height = deserializedObject.Size[1];

            map = new string[width, height];

            for (int i = 0; i < deserializedObject.Size[0]; i++)
            {
                for (int j = 0; j < deserializedObject.Size[1]; j++)
                {
                    map[i, j] = deserializedObject.Tiles[i * deserializedObject.Size[1] + j];
                }
            }

            return;
        }
    }
}