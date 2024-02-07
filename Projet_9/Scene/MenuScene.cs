using NEngine;
using NEntity;
using NGlobal;
using NModules;
using NSave;
using NSecurity;
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
                LoadGame();
            });

            UIButton newGameButton = new UIButton("Créer Partie");
            newGameButton.AddEvent(() => { CreateNewGame(); });

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

        public void CreateNewGame()
        {
            Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
            string userName;
            string password;
            bool validUsername = false;
            bool validPassword = false;
            Guid uniqueId = Guid.NewGuid();
            string playerUid = uniqueId.ToString();

            Global.WriteSprites(new List<string> { "CREER UN NOUVEL UTILISATEUR" }, 3);

            UserManager userManager = UserManager.GetInstance();

            do
            {
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.Write("Créez votre nom d'utilisateur : ");
                userName = Console.ReadLine();
                validUsername = userManager.IsUserAlreadyExistingByName(userName) || Security.ValidationPseudo(userName);
            } while (!validUsername);

            do
            {
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.Write("Créez votre mot de passe : ");
                password = Console.ReadLine();
                validPassword = Security.ValidationMotDePasse(password);
            } while (!validPassword);

            userManager.NewUser(userName, password);

            System.Threading.Thread.Sleep(2000);

            Global.WriteSprites(new List<string> { "BIENVENUE " + userName });

            System.Threading.Thread.Sleep(3000);

            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<SceneIntroduction>(true);
        }

        public void LoadGame()
        {
            Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
            string userName;
            string password;
            bool validUsername = false;
            bool validPassword = false;
            Guid uniqueId = Guid.NewGuid();
            string playerUid = uniqueId.ToString();

            Global.WriteSprites(new List<string> { "SE CONNECTER" }, 3);

            UserManager userManager = UserManager.GetInstance();

            do
            {
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.Write("Entrez un nom d'utilisateur : ");
                userName = Console.ReadLine();
                validUsername = userManager.IsUserAlreadyExistingByName(userName);
                if (!validUsername)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{userName} introuvable");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (!validUsername);

            if (!userManager.CheckConnexion(userName))
            {
                do
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Entrez votre mot de passe : ");
                    password = Console.ReadLine();
                    validPassword = userManager.CheckConnexion(userName, password);
                    if (!validPassword)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Mot de passe incorrect");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (!validPassword);
            }
            System.Threading.Thread.Sleep(2000); 
            Console.WriteLine($"Bienvenue {userName}, content de vous revoir");

            System.Threading.Thread.Sleep(2000);
            PlayerManager playerManager = PlayerManager.GetInstance();
            playerManager.LoadPlayer();
            SavePlayer.GetInstance(playerManager.GetActualPlayer().Id, false);
            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
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
    }
}