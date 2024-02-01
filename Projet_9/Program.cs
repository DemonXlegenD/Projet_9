using NPokemon;
using NSave;
using NEntity;
using NPotionType;
using System.Collections.Generic;
using Newtonsoft.Json;
using Projet_9.Save;
using NSecurity;
using System;
namespace Projet_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* WindowPokemonTeam window = new WindowPokemonTeam();
             window.WindowRun();
             Engine engine = Engine.GetInstance();

             engine.Run();
             window.WindowClose();*/
            /*  Player player = new Player();
              List<string> list = new List<string>();
              list.Add("Water");
              List<JsonConverter> listConverter = new List<JsonConverter>();
              listConverter.Add(new PlayerJsonConverter());
              listConverter.Add(new PokemonJsonConverter());

              player.AddItem(new Potion());
              player.AddPokemon(new Pokemon("Jarod", list, 40, 50, 70, 50, 50, 70));

              SavePlayer.GetInstance(player.FirstName, player.LastName, player.Id).WriteSave(player, 2, listConverter);*/
            /*          Player player = Save.GetInstance().ReadTestSave<Player>(2);
                      Console.WriteLine(player.ToString());*/
/*            SaveUser saveUser = SaveUser.GetInstance();*/
            UserManager userManager = UserManager.GetInstance();
     /*       userManager.AddUser("Zarod", "azerty");
            saveUser.SaveUsers();*/
            User.LoadUser("Zarod", "azerty");
            Console.WriteLine("C'est bien : " + userManager.ActualUser.Username);
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
