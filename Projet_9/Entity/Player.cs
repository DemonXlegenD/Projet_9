using Csharp_Tpt;
using Maths;
using NComponents;
using Newtonsoft.Json;
using NInventory;
using System;
using System.Collections.Generic;
using NPokemon;
using NSecurity;

namespace NEntity
{
    public class Player : Component
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Description { get; set; }

        public List<Pokemon> TeamPokemons { get; set; }
        public List<Pokemon> PCPokemons { get; set; }

        public Vector2i Position { get; set; }
        public Dictionary<string, ItemAbstract> Inventory { get; set; }

        public Player()
        {
            FirstName = "Sacha";
            LastName = "Ketchup";
            Age = 15;
            Description = "Moi ze veut devenir un maitre pokémon";
            TeamPokemons = new List<Pokemon>();
            PCPokemons = new List<Pokemon>();
            Position = Vector2i.Zero;
            Inventory = new Dictionary<string, ItemAbstract>();
        }

        public Player(string id, string firstName, string lastName, int age, string description, Pokemon pokemon)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Description = description;
            TeamPokemons = new List<Pokemon>();
            TeamPokemons.Add(pokemon);
            PCPokemons = new List<Pokemon>();
            Position = Vector2i.Zero;
            Inventory = new Dictionary<string, ItemAbstract>();
        }

            public Player(string id, string firstName, string lastName, int age, string description, List<Pokemon> teamPokemons, List<Pokemon> pCPokemons, Vector2i position, Dictionary<string, ItemAbstract> inventory)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Description = description;
            TeamPokemons = teamPokemons;
            PCPokemons = pCPokemons;
            Position = position;
            Inventory = inventory;
        }
        public Player(Player player)
        {
            Id = player.Id;
            FirstName = player.FirstName;
            LastName = player.LastName;
            Age = player.Age;
            Description = player.Description;
            TeamPokemons = player.TeamPokemons;
            PCPokemons = player.PCPokemons;
            Position = player.Position;
            Inventory = player.Inventory;
        }

        public override string ToString()
        {
            return $"Player : FirstName: {FirstName}, LastName: {LastName}, Age: {Age}, Description: {Description} }}";
        }

        public void AddPokemon(Pokemon pokemon)
        {
            if (TeamPokemons.Count < 6)
            {
                TeamPokemons.Add(pokemon);
            }
            else
            {
                PCPokemons.Add(pokemon);
            }
        }

        public void RemovePokemon(Pokemon pokemon)
        {
            TeamPokemons.Remove(pokemon);
        }

        public void AddItem(ItemAbstract item)
        {
            if (!Inventory.ContainsKey(item.Name))
            {
                Inventory.Add(item.Name, item);
                Console.WriteLine($"Added {item.Name} to the inventory.");
            }
            else
            {
                Console.WriteLine($"Item with ID {item.Name} is already in the inventory.");
            }
        }

        public void RemoveItem(ItemAbstract item)
        {
            if (Inventory.ContainsKey(item.Name))
            {
                Inventory.Remove(item.Name);
                Console.WriteLine($"Removed {item.Name} from the inventory.");
            }
            else
            {
                Console.WriteLine($"Item with ID {item.Name} is not in the inventory.");
            }
        }
    }
}
