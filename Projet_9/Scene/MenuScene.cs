using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NEngine;
using NModules;
using NSave;

namespace NScene
{
    internal class MenuScene : SceneAbstract
    {
        public MenuScene() : base("Menu Scene") { }

        private enum Menu
        {
            MAIN_MENU,
            CREATE_SAVE,
            LOAD_SAVE,
            SETTINGS,
            EXIT
        }

        private Menu selected_menu = Menu.MAIN_MENU;

        private int selected = 0;
        private List<string> selections = new List<string>();

        public override void Init()
        {
            selections.Add("jveu joué un nouvo :D");
            selections.Add("jveu joué lonciene :D");
            selections.Add("oskour ge ve shangai lai paramaitre !!!");
            selections.Add("moi jveu kitai D:");
        }

        public override void Launch()
        {
            base.Launch();

            DisplaySelection();

            Console.Out.Flush();
            Console.Clear();
        }

        public void DisplaySelection()
        {
            switch(selected_menu)
            {
                case Menu.MAIN_MENU:
                    Console.WriteLine("bienvunu sur evoascii !!!!!!!!!");
                    Console.WriteLine("seleksione 1 truc a fer !!!!!!!!!\n");

                    for (int i = 0; i < selections.Count; i++)
                    {
                        if (selected == i)
                        {
                            Console.Write("> " + selections[i] + " <\n");
                        }
                        else
                        {
                            Console.WriteLine("  " + selections[i]);
                        }
                    }

                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        selected++;
                        if (selected == selections.Count)
                        {
                            selected = 0;
                        }
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        selected--;
                        if (selected < 0)
                        {
                            selected = selections.Count - 1;
                        }
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        switch (selected)
                        {
                            case 0:
                                selected_menu = Menu.CREATE_SAVE;
                                break;
                            case 1:
                                selected_menu = Menu.LOAD_SAVE;
                                break;
                            case 2:
                                selected_menu = Menu.SETTINGS;
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                        }
                    }

                    break;

                case Menu.CREATE_SAVE:
                    string LastName;
                    string FirstName;

                    Guid uniqueId = Guid.NewGuid();
                    string playerUid = uniqueId.ToString();

                    Console.WriteLine("CREER UNE NOUVELLE SAUVEGARDE\n");

                    do
                    {
                        Console.Write("Entrez votre nom de famille et j'ai bien dit 'Famille': ");
                        LastName = Console.ReadLine();
                    } while (!IsOnlyCharacter(LastName));

                    do {
                        Console.Write("Entrez votre prénom, pas ton pseudo: ");
                        FirstName = Console.ReadLine();
                    } while (!IsOnlyCharacter(FirstName));

                    new SavePlayer(LastName, FirstName, playerUid);

                    Console.WriteLine("BIENVENUE, " + LastName + " " + FirstName);

                    System.Threading.Thread.Sleep(3000);

                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);

                    break;

                case Menu.LOAD_SAVE:
                    Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
                    break;

                default: 
                    break;
            }
        }

        static bool IsOnlyCharacter(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    Console.WriteLine("La saisie ne doit contenir que des lettres. Réessayez.");
                    return false;
                }
            }

            return true;
        }
    }
}