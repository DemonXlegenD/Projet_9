using NEngine;
using NGlobal;
using NModules;
using NSave;
using NUIElements;
using System;
using System.Collections.Generic;

namespace NScene
{
    internal class MenuScene : SceneAbstract
    {
        public MenuScene() : base("Menu Scene") { }

        private int selected = 0;
        private List<UIButton> selections = new List<UIButton>();

        public override void Init()
        {
            UIButton loadGameButton = new UIButton("Charger Partie")
            {
                IsHovered = true
            };
            loadGameButton.AddEvent(() =>
            {
                Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
            });

            UIButton newGameButton = new UIButton("Créer Partie");
            newGameButton.AddEvent(() =>
            {
                Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
                Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
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

                do
                {
                    Console.Write("Entrez votre prénom, pas ton pseudo: ");
                    FirstName = Console.ReadLine();
                } while (!IsOnlyCharacter(FirstName));

                new SavePlayer(LastName, FirstName, playerUid);

                Console.WriteLine("BIENVENUE, " + LastName + " " + FirstName);

                System.Threading.Thread.Sleep(3000);

                Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
            });

            UIButton optionsButton = new UIButton("Options");
            

            optionsButton.AddEvent(() =>
            {
                Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
                Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
                Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
            });

            UIButton leaveButton = new UIButton("Quitter");
            leaveButton.AddEvent(() =>
            {
                Engine.GetInstance().Quit();
            });

            selections.Add(loadGameButton);
            selections.Add(newGameButton);
            selections.Add(optionsButton);
            selections.Add(leaveButton);

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
            Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
            foreach (UIButton button in selections)
            {
                button.Display();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                selected++;
                if (selected == selections.Count)
                {
                    selected = 0;
                }
                foreach (UIButton button in selections)
                {
                    button.IsHovered = false;
                }
                selections[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                selected--;
                if (selected < 0)
                {
                    selected = selections.Count - 1;
                }
                foreach (UIButton button in selections)
                {
                    button.IsHovered = false;
                }
                selections[selected].IsHovered = true;
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                selections[selected].Clear();
                selections[selected].Click();
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