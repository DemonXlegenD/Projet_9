using Map;
using Maths;
using NGlobal;
using NModules;
using NPokemon;
using NScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

        private string[,] map;
        private int height = 20;
        private int width = 20;


        public MapScene() : base("MapScene") { }

        public override void Init()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public override void Launch() {
            DisplayMap();
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

                    if (key.Key == ConsoleKey.Escape)
                    {
                        currentAction = Actions.MOVING;
                        if (key.Key == ConsoleKey.RightArrow && (selectedMenuAction == MenuActions.FIGHT || selectedMenuAction == MenuActions.LEAVE))
                        {
                            if (selectedMenuAction == MenuActions.FIGHT || selectedMenuAction == MenuActions.LEAVE)
                            {
                                selectedMenuAction -= 1;
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
                    if (playerPosition.GetX() == j && playerPosition.GetY() == i)
                    {
                        Console.Write(playerCharacter + " ");
                    }
                    else if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        map[i, j] = "# ";
                        Console.Write(map[i, j]);
                    } else
                    {
                        map[i, j] = "  ";
                        Console.Write(map[i, j]);
                    }
                }
                Console.Write("\n");
            }

        }

        private void DisplaySelection()
        {
            Console.Write("\n");
            for (int i = 0; i < width*2; i++)
            {
                Console.Write("#");
            }
            Console.Write("\n");
            for (int i = 0; i < width*2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("\n");
            Console.WriteLine("# INVENTAIRE           SE BATTRE #\n");
            Console.WriteLine("# STATS                  QUITTER #");
            for (int i = 0; i < width*2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("\n");
            for (int i = 0; i < width*2; i++)
            {
                Console.Write("#");
            }
            Console.Write("\n");
        }
    }
}