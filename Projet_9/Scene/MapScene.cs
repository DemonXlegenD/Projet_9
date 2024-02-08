using Maths;
using NEngine;
using NEntity;
using Newtonsoft.Json;
using NModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NGlobal;
using System.Linq;
using System.Windows.Documents;

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
        bool enemyFound = false;

        private Dictionary<string, Vector2i> enemy1 = new Dictionary<string, Vector2i>();

        List<Dictionary<string, Vector2i>> enemies = new List<Dictionary<string, Vector2i>>();
        
        private string[,] map;
        private string mapName;
        private int height = 0;
        private int width = 0;
        private Vector2i spawn = Vector2i.Zero;

        public MapScene() : base("MapScene") { }

        public override void Init()
        {
            Console.OutputEncoding = Encoding.UTF8;

            LoadMap("League1", false);

            if (collidable.Contains(map[GetPlayer().Position.GetX(), GetPlayer().Position.GetY()]))
            {
                GetPlayer().Position = spawn;
            }

            enemy1.Add("D", new Vector2i(10, 2));

            enemies.Add(enemy1);
        }

        private List<string> collidable = new List<string>() { "C", "T", "W" };
        private void GetTiles(string tile, bool display)
        {
            string character = " ";

            if (tile == "T")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                character = "♣";
            }
            else if (tile == "C")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
                character = "■";
            }
            else if (tile == "G")
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (tile == "Y")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (tile == "R")
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (tile == "W")
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else if (tile == "DR")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else if (tile == "DM")
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            }
            else if (tile == "DB")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else if (tile == "#")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                character = "#";
            }

            if (display)
            {
                Console.Write(" " + character + " ");
            }

            return;
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
            HandleTeleport();

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
                        if (!collidable.Contains(map[GetPlayer().Position.GetY() + 1, (GetPlayer().Position.GetX())]))
                        {
                            GetPlayer().Position.SetY(GetPlayer().Position.GetY() + 1);
                        }
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (!collidable.Contains(map[GetPlayer().Position.GetY() - 1, GetPlayer().Position.GetX()]))
                        {
                            GetPlayer().Position.SetY(GetPlayer().Position.GetY() - 1);
                        }
                    }
                    else if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (!collidable.Contains(map[GetPlayer().Position.GetY(), GetPlayer().Position.GetX() - 1]))
                        {
                            GetPlayer().Position.SetX(GetPlayer().Position.GetX() - 1);
                        }
                    }
                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (!collidable.Contains(map[GetPlayer().Position.GetY(), GetPlayer().Position.GetX() + 1]))
                        {
                            GetPlayer().Position.SetX(GetPlayer().Position.GetX() + 1);
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
                                if (GetPlayer().Position.GetX() == enemy.Value.GetX() && GetPlayer().Position.GetY() == enemy.Value.GetY())
                                {
                                    // Changer les enemyPokemons avant
                                    enemyFound = true;
                                    Global.IsWildFight = false;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                                }
                                else
                                {
                                    GetTiles(map[i, j], false);
                                    Console.Write(" " + enemy.Key + " ");
                                    enemyTile = true;
                                }
                            }
                        }
                    }

                    if (!enemyTile)
                    {
                        if (GetPlayer().Position.GetX() == j && GetPlayer().Position.GetY() == i)
                        {
                            GetTiles(map[i, j], false);
                            if (map[i, j] == "#")
                            {
                                Random rnd = new Random();
                                int chance = rnd.Next(1, 10);
                                if (chance == 1)
                                {
                                    enemyFound = true;
                                    Global.IsWildFight = true;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                                }
                            }
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

        private void HandleTeleport()
        {
            if (File.Exists("Maps/" + map[GetPlayer().Position.GetY(), GetPlayer().Position.GetX()] + ".json"))
            {
                LoadMap(map[GetPlayer().Position.GetY(), GetPlayer().Position.GetX()], true);
            }
        }

        private void LoadMap(string _map, bool teleportToSpawn)
        {
            string jsonContent = System.IO.File.ReadAllText("Maps/" + _map + ".json");
            var deserializedObject = JsonConvert.DeserializeAnonymousType(jsonContent, new { Tiles = new string[0], Size = new int[0], Spawn1 = new int[0], Spawn2 = new int[0] });

            Console.WriteLine(deserializedObject.Size[0]);
            //System.Threading.Thread.Sleep(10000);
            width = deserializedObject.Size[0];
            height = deserializedObject.Size[1];

            spawn = new Vector2i(deserializedObject.Spawn1[0], deserializedObject.Spawn1[1]);

            map = new string[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = deserializedObject.Tiles[i * width + j];
                    if (map[i, j] == mapName)
                    {
                        spawn = new Vector2i(deserializedObject.Spawn2[0], deserializedObject.Spawn2[1]);
                    }
                }
            }

            if (teleportToSpawn)
            {
                GetPlayer().Position = spawn;
            }

            mapName = _map;

            return;
        }
    }
}