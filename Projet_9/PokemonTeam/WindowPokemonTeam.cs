using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using Csharp_Tpt;
using System.Windows.Input;
using Map;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Projet_9.PokemonTeam
{
    public class panel
    {
        public panel(StackPanel stack,int id)
        {
            stackpanel = stack;
            ID = id;
        }
        public StackPanel stackpanel;
        public int ID;
    }

    public class WindowPokemonTeam
    {
        private int x = -1;
        private int y = -1;
        List<string> ids = new List<string>();
        StackPanel Canvas;
        StackPanel AllPokemons;
        List<Pokemon> Pokemons = new List<Pokemon>();



        Window w = null;
        private TextBlock textblock
            (
            string text, 
            Brush color = null, 
            int fontSize = 24, 
            HorizontalAlignment horizontal = HorizontalAlignment.Center, 
            VerticalAlignment vertical = VerticalAlignment.Center,
            Thickness thickness = default
            ) 
        {
            return new TextBlock() { 
                Text = text, 
                Foreground = color ?? Brushes.White, 
                FontSize = fontSize,
                HorizontalAlignment = horizontal,
                VerticalAlignment = vertical,
                Margin = thickness,
                FontFamily = new FontFamily("Monocraft")
            };
        }

        private StackPanel Panel2Text(
            string Text1,
            string Text2,
            Brush ColorText2 = null,
            Thickness thickness = default
            )
        {
            return new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children = { textblock(Text1) , textblock(Text2, color: ColorText2) },
                Margin = thickness
            };
        }


        private StackPanel PokemonInfos(Pokemon pokemon)
        {
            StackPanel stackPanel = new StackPanel() { Margin = new Thickness(0,0,0,20), Background = new SolidColorBrush(Color.FromArgb(175, 30, 30, 30)) };


            TextBlock namePokemon = textblock(pokemon.GetName(), horizontal: HorizontalAlignment.Left);

            StackPanel PokemonStats = new StackPanel() { Orientation = Orientation.Horizontal };
            //PokemonStats.Children.Add(textblock("Hp:" + pokemon.GetHp().ToString()+"/"+pokemon.GetMaxHp().ToString(), color: Brushes.Red, thickness: new Thickness(10,0,0,0)));
            PokemonStats.Children.Add(Panel2Text("Hp:", pokemon.GetHp().ToString()+ "/" + pokemon.GetMaxHp().ToString(), ColorText2: Brushes.Red ));
            if (pokemon.GetTypes().Length > 1) { PokemonStats.Children.Add(new StackPanel() { Margin = new Thickness(10, 0, 0, 0), Orientation = Orientation.Horizontal, Children = { Panel2Text("Types:", pokemon.GetTypes()[0], Global.TypeToColor(pokemon.GetTypes()[0])), Panel2Text(",", pokemon.GetTypes()[1], Global.TypeToColor(pokemon.GetTypes()[1])) } } ); }
            else { PokemonStats.Children.Add( Panel2Text("Type:", pokemon.GetTypes()[0], Csharp_Tpt.Global.TypeToColor(pokemon.GetTypes()[0]), thickness: new Thickness(10, 0, 0, 0)) ); }
            PokemonStats.Children.Add(Panel2Text("Level:", pokemon.GetLevel().ToString(), ColorText2: Brushes.CornflowerBlue, thickness: new Thickness(10, 0, 0, 0) ));

            stackPanel.Children.Add(namePokemon);
            stackPanel.Children.Add(PokemonStats);

            // Create a Border
            //Border border = new Border();
            //border.CornerRadius = new System.Windows.CornerRadius(5); // Set the corner radius
            //border.BorderBrush = Brushes.Black;
            //border.BorderThickness = new System.Windows.Thickness(1);
            //border.Child = stackPanel; // Add the StackPanel as a child of the Border
            //Console.WriteLine("IDS:"+stackPanel.Uid);
            stackPanel.MouseDown += StackPanel_MouseDown;
            stackPanel.Uid = ids.Count.ToString();
            ids.Add(stackPanel.Uid); // Ajouter l'Uid à la liste ids
            return stackPanel;
        }
        

        [STAThread] // Définir le thread en mode STA
        public void WindowRun()
        {
            Pokemons.Add(new Pokemon("Jarod", new string[] { "Water" }, 100, 100, 100, 100, 100, 100, 10));
            Pokemons.Add(new Pokemon("Francois", new string[] { "Fire" }, 80, 10, 10, 10, 10, 10, 2));
            Pokemons.Add(new Pokemon("Maurad", new string[] { "Grass" }, 80, 10, 10, 10, 10, 10, 50));
            Pokemons.Add(new Pokemon("Adrien", new string[] { "Ground" }, 80, 10, 10, 10, 10, 10, 99));
            Pokemons.Add(new Pokemon("Kyle", new string[] { "Dragon" }, 80, 10, 10, 10, 10, 10, 40));
            Pokemons.Add(new Pokemon("Ethan", new string[] { "Bug", "Grass" }, 80, 10, 10, 10, 10, 10, 5));

            Thread thread = new Thread(() =>
            {
                w = new Window();
                w.Width = 600;
                w.Height = 600;
                w.ResizeMode = ResizeMode.NoResize;

                StackPanel canvas = new StackPanel(); // Top Node, ne pas toucher

                // Stack avec touts les éléments, comparable au Control node sur Godot
                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                stackPanel.Margin = new Thickness(25, 10, 0, 0);


                TextBlock PokemonteamText = textblock("Pokemon Team", thickness: new Thickness(0, 10, 0, 20), horizontal: HorizontalAlignment.Center);
                canvas.Children.Add(PokemonteamText);

                
                foreach (Pokemon pokemon in Pokemons)
                {
                    stackPanel.Children.Add(PokemonInfos(pokemon));
                }
                AllPokemons = stackPanel;
                canvas.Children.Add(AllPokemons);
                Canvas = canvas;
                w.Content = Canvas;

                w.HorizontalAlignment = HorizontalAlignment.Center;
                w.VerticalAlignment = VerticalAlignment.Center;

                // Activer la transparence
                w.AllowsTransparency = true;
                // Définir le style de la fenêtre sur None
                w.WindowStyle = WindowStyle.None;

                // Définir le fond transparent
                w.Background = Brushes.Transparent;

                // Utiliser un canal alpha pour rendre la fenêtre semi-transparente
                w.Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)); // 128 représente la transparence (0 à 255)

                w.MouseDown += (sender, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        w.DragMove(); // Permet de déplacer la fenêtre
                    }
                };

                w.Show();
                System.Windows.Threading.Dispatcher.Run(); // Maintenir le thread STA en cours d'exécution
            });

            thread.SetApartmentState(ApartmentState.STA); // Définir l'état de l'appartement sur STA
            thread.Start(); // Démarrer le thread

        }
        public void WindowClose()
        { 
            w.Dispatcher.Invoke(() => w.Close());
        }
        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel clickedStackPanel = (StackPanel)sender;
            if (x == -1)
            {
                x = int.Parse(clickedStackPanel.Uid);
            }
            else
            {
                y = int.Parse(clickedStackPanel.Uid);
                Console.WriteLine("Size"+Pokemons.Count()+"X:"+x+"Y:"+y);
                Global.ChangePokemonOrder(Pokemons,x,y);
                AllPokemons.Children.Clear();
                ids.Clear();
                foreach (Pokemon pokemon in Pokemons)
                {
                    AllPokemons.Children.Add(PokemonInfos(pokemon));
                }
                x = -1;
                y = -1;
            }
            //StackPanel clickedStackPanel = (StackPanel)sender;
            //string id = clickedStackPanel.Uid;
            //MessageBox.Show(clickedStackPanel.Uid+"List:"+ids.Count());

        }
    }
}
