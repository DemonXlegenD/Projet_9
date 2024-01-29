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

namespace Projet_9.PokemonTeam
{
    public class WindowPokemonTeam
    {
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
                Margin = thickness
            };
        }

        private StackPanel PokemonInfos(Pokemon pokemon)
        {
            StackPanel stackPanel = new StackPanel();

            TextBlock namePokemon = textblock(pokemon.GetName(), horizontal: HorizontalAlignment.Left);

            StackPanel PokemonStats = new StackPanel() { Orientation = Orientation.Horizontal };
            PokemonStats.Children.Add(textblock("Hp : " + pokemon.GetHp().ToString(), color: Brushes.Red, thickness: new Thickness(10,0,0,0)));



            stackPanel.Children.Add(namePokemon);
            stackPanel.Children.Add(PokemonStats);

            return stackPanel;
        }
        

        [STAThread] // Définir le thread en mode STA
        public void WindowRun()
        {
            Thread thread = new Thread(() =>
            {
                w = new Window();
                w.Width = 600;
                w.Height = 600;
                w.Background = Brushes.Black;

                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;

                stackPanel.Children.Add(PokemonInfos(new Pokemon("Jarod", new string[] { "Water" },100,100,100,100,100,100)));
                stackPanel.Children.Add(PokemonInfos(new Pokemon("Francois", new string[] { "Fire" }, 80, 10, 10, 10, 10, 10)));
                w.Content = stackPanel;

                w.Left = 100;
                w.Top = 100;
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
    }
}
