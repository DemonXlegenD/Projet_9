﻿using Maths;
using NEngine;
using Newtonsoft.Json;
using NGlobal;
using NModules;
using NPokemon;
using NTrainer;
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
            SAVE,
            PAUSE,
            LEAVE
        }

        private Actions currentAction = Actions.MOVING;
        private MenuActions selectedMenuAction = MenuActions.INVENTORY;

        private string playerCharacter = "☺";
        private Vector2i playerPosition = new Vector2i(1, 1);

        private int ligneSelection = 0;
        private int finMapLigne = 0;
        private bool stop = false;
        private bool selection = false;

        private Dictionary<string, Vector2i> enemy1 = new Dictionary<string, Vector2i>();

        List<Dictionary<string, Vector2i>> enemies = new List<Dictionary<string, Vector2i>>();

        private string[,] map;
        private string mapName;
        private int height = 0;
        private int width = 0;
        private List<string> collidable = new List<string>() { "C", "T" };

        private List<string> trainer = new List<string>() { "A", "B", "C", "D", };

        private Vector2i spawn = Vector2i.Zero;

        public MapScene() : base("MapScene") { }

        public override void Init()
        {
            Console.OutputEncoding = Encoding.UTF8;
            mapName = "Map2";
            LoadMap(mapName, false);
            Global.LoadTrainers();
            if (collidable.Contains(map[GetPlayer().Position.GetX(), GetPlayer().Position.GetY()]))
            {
                GetPlayer().Position = spawn;
            }
        }
        //T : Tree ; C : Mur; G : Sol vert; 
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
            else if (tile == "DY")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
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

        public override void Launch()
        {
            Console.Clear();
            stop = false;
            selection = false;
            DisplayMap();
            DisplaySelection();
            do
            {
                HandleInput();
                if (selection)
                {
                    Global.ClearLines(ligneSelection);
                    DisplaySelection();
                }
            } while (!stop);
            /*HandleTeleport();*/

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
                        if (!collidable.Contains(map[playerPosition.GetY() + 1, playerPosition.GetX()]))
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
                    foreach (var entry in Global.AllTrainers)
                    {
                        if (entry.Key == mapName && entry.Value != null)
                        {
                            foreach (Trainer enemy in entry.Value)
                            {
                                if (enemy.Position.GetX() == playerPosition.GetX() && enemy.Position.GetY() == playerPosition.GetY())
                                {
                                    stop = true;
                                    GetTiles(map[playerPosition.GetY(), playerPosition.GetX()], false);
                                    Global.IsWildFight = false;
                                    Global.EnemyPokemons = enemy.Pokemons;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                                }
                            }
                        }
                    }
                    if (map[playerPosition.GetY(), playerPosition.GetX()] == "#")
                    {
                        Random rnd = new Random();
                        int chance = rnd.Next(1, 10);
                        if (chance == 1)
                        {
                            stop = true;
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Black;
                            Global.IsWildFight = true;
                            Global.EnemyPokemons.Clear();
                            Pokemon pokemonRandom = new Pokemon(Pokemon.LoadIPokemonAndReturn("" + rnd.Next(1, 3)))
                            {
                                Level = rnd.Next(1, 5)
                            };
                            Global.EnemyPokemons.Add(pokemonRandom);
                            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<FightScene>(true);
                        }
                    }
                    Console.SetCursorPosition(0, ligneSelection+1);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        selection = true;
                        currentAction = Actions.MENU;
                    }
                    break;

                case Actions.MENU:

                    int ActionCount = Enum.GetNames(typeof(MenuActions)).Length;
                    if (key.Key == ConsoleKey.Escape)
                    {
                        selection = false;
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
                        switch (selectedMenuAction)
                        {
                            case MenuActions.PAUSE:
                                {
                                    selection = false;
                                    stop = true;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<PauseMenu>(true);
                                    break;
                                }
                            case MenuActions.SAVE:
                                {
                                    PlayerManager.SavePlayerInFile(1);
                                    Console.WriteLine("Sauvegardé !");
                                    System.Threading.Thread.Sleep(2000);
                                    break;
                                }
                            case MenuActions.LEAVE:
                                {
                                    selection = false;
                                    stop = true;
                                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MenuScene>(true);
                                    break;
                                }
                            default:
                                {
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
                    foreach (var entry in Global.AllTrainers)
                    {
                        if (entry.Key == mapName && entry.Value != null)
                        {
                            foreach (Trainer enemy in entry.Value)
                            {
                                if (enemy.Position.GetX() == j && enemy.Position.GetY() == i)
                                {
                                    GetTiles(map[i, j], true);
                                    Console.Write(" " + enemy.Id + " ");
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
                                    stop = true;
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
            finMapLigne = Console.CursorTop;
        }

        private void DisplaySelection()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ligneSelection = Console.CursorTop;
            Console.Write("\n");
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║                                    ║");

            if (currentAction == Actions.MENU)
            {
                if (selectedMenuAction == MenuActions.INVENTORY)
                {
                    Console.WriteLine("║ > INVENTAIRE <            SAVE     ║");
                }
                else if (selectedMenuAction == MenuActions.SAVE)
                {
                    Console.WriteLine("║ INVENTAIRE              > SAVE <   ║");
                }
                else
                {
                    Console.WriteLine("║ INVENTAIRE                SAVE     ║");
                }

                Console.WriteLine("║                                    ║");

                if (selectedMenuAction == MenuActions.PAUSE)
                {
                    Console.WriteLine("║ > PAUSE <                  QUITTER ║");
                }
                else if (selectedMenuAction == MenuActions.LEAVE)
                {
                    Console.WriteLine("║ PAUSE                  > QUITTER < ║");
                }
                else
                {
                    Console.WriteLine("║ PAUSE                      QUITTER ║");
                }
            }
            else
            {
                Console.WriteLine("║ INVENTAIRE                  SAVE   ║");
                Console.WriteLine("║                                    ║");
                Console.WriteLine("║ PAUSE                      QUITTER ║");
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
            string jsonContent = System.IO.File.ReadAllText("Data/Maps/" + _map + ".json");
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
                    map[i, j] = deserializedObject.Tiles[(i * width) + j];
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