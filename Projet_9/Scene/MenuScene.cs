using NEngine;
using NEntity;
using NGlobal;
using NModules;
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
        private UIPanel panel = new UIPanel();
        private bool stop = false;

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
            
            panel.AddButton(loadGameButton);
            panel.AddButton(newGameButton);
            panel.AddButton(optionsButton);
            panel.AddButton(leaveButton);

            panel.AddEventToAll(() => stop = true);

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
            string userName;
            string password;
            bool validUsername = false; 
            bool validPassword = false;
            Guid uniqueId = Guid.NewGuid();

            Global.WriteSprites(new List<string> { "", "", "CREER UN NOUVEL UTILISATEUR" }, 3);

            UserManager userManager = UserManager.GetInstance();

            do
            {
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.Write("Créez votre nom d'utilisateur : ");
                userName = Console.ReadLine();
                if (userManager.IsUserAlreadyExistingByName(userName))
                {
                    Console.WriteLine($"Utilisateur {userName} déjà existant");
                    validUsername = false;
                }
                else
                {
                    validUsername = Security.ValidationPseudo(userName);
                }

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
            string userName;
            string password;
            bool validUsername = false;
            bool validPassword = false;

            Global.WriteSprites(new List<string> { "","","SE CONNECTER" }, 3);

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
            playerManager.SetUserUid(userManager.ActualUser.Username + "_" + userManager.ActualUser.Id);

            List<string> files = userManager.GetSavesOfUser();
            UIPanel panel = new UIPanel();

            foreach (string file in files)
            {
                string nameFile = file.Substring(file.LastIndexOf("\\") + 1, 5);
                UIButton saveButton = new UIButton(nameFile);
                saveButton.AddEvent(() => { playerManager.LoadPlayerFromSaveFile(file); });
                panel.AddButton(saveButton);
            }

            do
            {
                Console.WriteLine("Choisissez une sauvegarde");
                panel.SelectButton();
            } while (playerManager.GetActualPlayer() == null);

            Console.WriteLine("Chargement ..." + playerManager.GetActualPlayer().FirstName);
            System.Threading.Thread.Sleep(2000);

            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
        }

        public void DisplaySelection()
        {
            Global.WriteSprites(new List<string> { "██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████     ███████ ██    ██ ██████ ", "██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██          ██      ██    ██ ██   ██", "██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████       ███████ ██    ██ ██████ ", "██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██               ██ ██    ██ ██   ██", "██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████     ███████  ██████  ██   ██", "                                                                                                     " }, 3);
            Global.WriteSprites(new List<string> { "███████╗██╗   ██╗ ██████╗  █████╗ ███████╗ ██████╗██╗██╗", "██╔════╝██║   ██║██╔═══██╗██╔══██╗██╔════╝██╔════╝██║██║", "█████╗  ██║   ██║██║   ██║███████║███████╗██║     ██║██║", "██╔══╝  ╚██╗ ██╔╝██║   ██║██╔══██║╚════██║██║     ██║██║", "███████╗ ╚████╔╝ ╚██████╔╝██║  ██║███████║╚██████╗██║██║", "╚══════╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚═╝", "                                                        " }, 3, 2);
            do
            {
                panel.SelectButton();
            } while (!stop);
        }
    }
}