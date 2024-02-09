using NEngine;
using NGlobal;
using NModules;
using NPokemon;
using NScene;
using NUIElements;
using Projet_9.PokemonTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NScene
{
    public class PauseMenu : SceneAbstract
    {
        private int X = -1;
        private int Y = -1;

        UIPanel panelOrder = null;
        WindowPokemonTeam windowPokemonTeam = null;

        private bool quit1, quit2 = false;

        public PauseMenu():base("Pause Menu")
        {
            windowPokemonTeam = new WindowPokemonTeam();
        }
         
        public override void Launch()
        {
           
            SelectSTATE();
        }

        public void SelectSTATE()
        {
            quit1 = false;
            UIButton Zero = new UIButton("0.Back");
            UIButton first = new UIButton("1.Save");
            UIButton second = new UIButton("2.Pokemon");
            UIButton third = new UIButton("3.Leave");

            Zero.AddEvent(() => ReturnMap());
            first.AddEvent(() => Save());
            second.AddEvent(() => PokemonSTATE());
            third.AddEvent(() => Quit());

            UIPanel panel = new UIPanel();
            panel.AddButton(Zero);
            panel.AddButton(first);
            panel.AddButton(second);
            panel.AddButton(third);

            do
            {
                Console.Clear();
                panel.SelectButton();
            } while (!quit1);
            Console.Clear();

        }

        public void Save()
        {
            PlayerManager.SavePlayerInFile(1);
            Console.WriteLine("Sauvegardé!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public void ShowPanel() {
            panelOrder.ClearPanel();
            
            int o = 1;
            foreach (Pokemon p in Global.PlayerPokemons)
            {
                UIButton button = null;
                if (p.Types.Count > 1)
                {
                    button = new UIButton(o + "." + p.Name + " Types:" + p.Types[0] + " " + p.Types[1] + " Hp:" + p.Hp + "/" + p.MaxHp + " Xp:" + p.Xp + "/" + p.XpNext);
                }
                else
                {
                    button = new UIButton(o + "." + p.Name + " Type:" + p.Types[0] + " Hp:" + p.Hp + "/" + p.MaxHp + " Xp:" + p.Xp + "/" + p.XpNext);
                }

                button.AddEvent(() => ChangeOrder());

                panelOrder.AddButton(button);
                o++;
            }
            UIButton button2 = new UIButton(o + ".Back");
            button2.AddEvent(() => quit2 = true);

            panelOrder.AddButton(button2);
        }

        public void PokemonSTATE()
        {
            quit2 = false;
            
            windowPokemonTeam.WindowRun();
            Console.WriteLine();
            UIPanel panel = new UIPanel();
            panelOrder = panel;

            ShowPanel();
            
            do
            {
                panel.SelectButton();
            } while (!quit2);
            windowPokemonTeam.WindowClose();
        }

        public void Quit()
        {
            quit1 = true;
            Engine.GetInstance().Quit();
        }



        public void ChangeOrder()
        {
            if (X == -1)
            {
                X = panelOrder.selected;
            }
            else
            {
                Y = panelOrder.selected;
                Global.ChangePokemonOrder(Global.PlayerPokemons,X,Y);
                
                Y = -1;
                X = -1;
                ShowPanel();
                windowPokemonTeam.WindowClose();
                windowPokemonTeam = new WindowPokemonTeam();
                windowPokemonTeam.WindowRun();
            }
        }

        public void ReturnMap()
        {
            quit1 = true;
            Engine.GetInstance().ModuleManager.GetModule<SceneModule>().SetScene<MapScene>();
        }

    }



}
