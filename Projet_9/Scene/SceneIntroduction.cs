using NDatas;
using NEngine;
using NEntity;
using NGlobal;
using NModules;
using NPokemon;
using NSecurity;
using NUIElements;
using System;
using System.Collections.Generic;

namespace NScene
{
    public class SceneIntroduction : SceneAbstract
    {
        public SceneIntroduction() : base("Scene Introduction") { }

        private int cursorDefault = 0;
        public override void Launch()
        {
            base.Launch();
            CreatePlayer();
        }

        public void WaitForPressing(string text)
        {
            while (!Console.KeyAvailable)
            {
              
                Console.WriteLine(text + "   =>");
                System.Threading.Thread.Sleep(500);
                Global.ClearLines(cursorDefault);


                Console.WriteLine(text);
                System.Threading.Thread.Sleep(500);
                Global.ClearLines(cursorDefault);
            }
            Console.ReadKey(true);
        }

        public bool Valider(string text)
        {
            string result;
            List<string> answers = new List<string>() { "o", "oui", "n", "non" };
            do
            {
                Console.Write(text + " (oui ou non) : ");
                result = Console.ReadLine();
                Global.ClearLines(cursorDefault - 1);
            } while (!answers.Contains(result.ToLower()));
            if (result.StartsWith("o"))
            {
                return true;
            }
            return false;
        }

        public void ShowProf1()
        {
            Global.WriteSprites(Global.ReadFilesText("Assets\\TXT_files_Dressers\\Professor_Oak.txt"), 3);
        }
        public void ShowProf2()
        {
            Global.WriteSprites(Global.ReadFilesText("Assets\\TXT_files_Dressers\\Professor_Oak2.txt"), 3);
        }

        public Pokemon PokemonToChoose()
        {
            Pokemon pokemon = null;
            List<Pokemon> list = new List<Pokemon>();
            list.Add(PokemonsData.GetPokemonWithId("1"));
            list.Add(PokemonsData.GetPokemonWithId("2"));
            list.Add(PokemonsData.GetPokemonWithId("3"));

            UIButton firstPokemon = new UIButton("1." + list[0].Name);
            UIButton secondPokemon = new UIButton("2." + list[1].Name);
            UIButton thirdPokemon = new UIButton("3." + list[2].Name);

            firstPokemon.AddEvent(() => { pokemon = list[0]; });
            secondPokemon.AddEvent(() => { pokemon = list[1]; });
            thirdPokemon.AddEvent(() => { pokemon = list[2]; });

            UIPanel panel = new UIPanel();
            panel.AddButton(firstPokemon);
            panel.AddButton(secondPokemon);
            panel.AddButton(thirdPokemon);
            do
            {
                do
                {
                    panel.SelectButton();
                } while (pokemon == null);
            } while (!Valider("Est-ce votre ultime bafouille ? "));
            return pokemon;
        }

        public void CreatePlayer()
        {
            
            WaitForPressing("Oh, mais qui voilà!");
            WaitForPressing("Ne serait-ce pas un nouveau dresseur?");
            ShowProf1();
            Console.Clear();
            ShowProf2();
            cursorDefault = Console.CursorTop;
            WaitForPressing("Je suis le Professeur Chêne. Je te souhaite la bienvenue");
            WaitForPressing("Dis moi comment tu t'appelles?");
            string firstname, lastname, age, description;
            UserManager userManager = UserManager.GetInstance();
            PlayerManager playerManager = PlayerManager.GetInstance();
            bool resultFirstName, resultLastName, valideName, resultAge;
            do
            {
                do
                {
                    Console.Write("Votre prénom : ");

                    firstname = Console.ReadLine();
                    resultFirstName = Security.ValidationName(firstname);
                    Global.ClearLines(cursorDefault - 1);
                    System.Threading.Thread.Sleep(500);
                    Global.ClearLines(cursorDefault - 1);
                } while (!resultFirstName);
                do
                {
                    Console.Write("Votre nom : ");
                    lastname = Console.ReadLine();
                    resultLastName = Security.ValidationName(lastname);
                    Global.ClearLines(cursorDefault - 1);
                    System.Threading.Thread.Sleep(500);
                    Global.ClearLines(cursorDefault - 1);
                } while (!resultLastName);
                valideName = Valider($"Est-ce bien vous : {firstname} {lastname} ?");
            } while (!valideName);

            WaitForPressing($"Bien. Heureux de te rencontrer : {firstname} {lastname}");
            WaitForPressing($"Parle moi un peu plus de toi. j'ai extremement hâte d'en apprendre plus sur toi {firstname}");
            int test = -1;
            do
            {
                test++;
                Console.Write("Votre âge : ");

                age = Console.ReadLine();
                Global.ClearLines(cursorDefault - 1);
                resultAge = Security.ValidationAge(age, test);
                System.Threading.Thread.Sleep(2000);
                Global.ClearLines(cursorDefault - 1);
            } while (!resultAge);

            int.TryParse(age, out int ageNumber);

            WaitForPressing("Interessant");
            if (ageNumber > 50)
            {
                WaitForPressing("Tu es presque aussi vieux que moi, bientôt la calvace à ce que je vois!");
            }
            else if (ageNumber < 50 && ageNumber > 20)
            {
                WaitForPressing("Ahhh, à cet âge là, j'étais comme toi. Jeune, dynamique, mais par contre bien plus beau que toi...");
            }
            else
            {
                WaitForPressing("Oooohhh, il est trop choupi. Gouzi Gouzi Gouzi...");
                WaitForPressing("Hmmmm hmmmm, excuse moi j'adore juste les enfants.");
            }

            do
            {
                Console.Write("Votre description : ");
                description = Console.ReadLine();
                Global.ClearLines(cursorDefault);
            } while (description == string.Empty);
            Global.ClearLines(cursorDefault);
            WaitForPressing("Interessant");

            WaitForPressing("Pour cette aventure tu devras choisir ton tout premier et fidèle compagnon ! ");

            Pokemon pokemon = PokemonToChoose();

            playerManager.NewPlayer(firstname, lastname, userManager.ActualUser.Id, ageNumber, description, pokemon);

            WaitForPressing("Bon, ce n'est pas que je suis pressé...");
            WaitForPressing("Mais j'ai aqua poney, tu me comprends surement.");
            if(Valider("Ta réponse sera fatale : "))
            {
                WaitForPressing("Parfait, je t'aime bien toi.");
            }
            else
            {
                WaitForPressing("Encore un cas déséspéré.");
            }

            WaitForPressing($"Aller, la bise {firstname}.");

            Console.Clear();
            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>(true);
        }
    }
}
