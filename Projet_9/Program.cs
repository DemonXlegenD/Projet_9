using Projet_9.PokemonTeam;
using NEngine;
namespace Projet_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WindowPokemonTeam window = new WindowPokemonTeam();
            window.WindowRun();
            Engine engine = Engine.GetInstance();

            engine.Run();
            window.WindowClose();
        }
    }
}
