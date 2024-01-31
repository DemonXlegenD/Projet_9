using NPokemon;
using NSave;
using NEntity;
using NPotionType;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Projet_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
/*            List<Pokemon> Pokemons = new List<Pokemon>();
            Pokemons.Add(new Pokemon("Jarod", new List<string> { "Water" }, 100, 100, 100, 100, 100, 100, 100));
            Pokemons.Add(new Pokemon("Francois", new List<string> { "Fire" }, 80, 10, 10, 10, 10, 10, 20));
            Pokemons.Add(new Pokemon("Maurad", new List<string> { "Grass" }, 80, 10, 10, 10, 10, 10, 50));
            Pokemons.Add(new Pokemon("Adrien", new List<string> { "Ground" }, 80, 10, 10, 10, 10, 10, 99));
            Pokemons.Add(new Pokemon("Kyle", new List<string> { "Dragon" }, 80, 10, 10, 10, 10, 10, 40));
            Pokemons.Add(new Pokemon("Ethan", new List<string> { "Bug", "Grass" }, 80, 10, 10, 10, 10, 10, 50));
            Pokemons[0].Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            foreach (Pokemon p in Pokemons)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }

            List<Pokemon> Pokemons2 = new List<Pokemon>();
            Pokemons2.Add(new Pokemon("Jarod", new List<string> { "Ground" }, 80, 54, 64, 100, 100, 100, 100));
            Pokemons2.Add(new Pokemon("Francois", new List<string> { "Grass" }, 80, 10, 24, 10, 10, 10, 20));
            Pokemons2.Add(new Pokemon("Maurad", new List<string> { "Grass" }, 14, 24, 10, 10, 10, 10, 50));
            Pokemons2.Add(new Pokemon("Adrien", new List<string> { "Grass" }, 78, 10, 10, 54, 10, 10, 99));
            Pokemons2.Add(new Pokemon("Kyle", new List<string> { "Grass" }, 80, 54, 78, 10, 22, 10, 40));
            Pokemons2.Add(new Pokemon("Ethan", new List<string> { "Bug", "Grass" }, 80, 10, 10, 25, 10, 10, 50));
            foreach (Pokemon p in Pokemons2)
            {
                p.Moves.Add(new Attack("Cool", "Fire", "Physical", 50, 100, 25));
            }


            Hacker hackertest = new Hacker(Pokemons,Pokemons2,"Jarod","Francois");*/

            /* WindowPokemonTeam window = new WindowPokemonTeam();
             window.WindowRun();
             Engine engine = Engine.GetInstance();

             engine.Run();
             window.WindowClose();*/
            Player player = new Player();
            List<string> list = new List<string>();
            list.Add("Water");
            List<JsonConverter> listConverter = new List<JsonConverter>();
            listConverter.Add(new PlayerJsonConverter());
            listConverter.Add(new PokemonJsonConverter());

            player.AddItem(new Potion());
            player.AddPokemon(new Pokemon("Jarod", list, 40, 50, 70, 50, 50, 70));

            Save.GetInstance().WriteSave(player, 2, listConverter);
  /*          Player player = Save.GetInstance().ReadTestSave<Player>(2);
            Console.WriteLine(player.ToString());*/
        }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Person : {{ FirstName: {FirstName}, LastName: {LastName}, Age: {Age} }}";
        }
    }
}
