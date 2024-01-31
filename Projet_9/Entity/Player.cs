using Csharp_Tpt;
using Maths;
using NComponents;
using Newtonsoft.Json;
using NInventory;
using System;
using System.Collections.Generic;
using NPokemon;

namespace NEntity
{
    public class Player : Component
    {

        public int Id { get; set; }

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
            Id = 0;
            FirstName = "Sacha";
            LastName = "Ketchup";
            Age = 15;
            Description = "Moi ze veut devenir un maitre pokémon";
            TeamPokemons = new List<Pokemon>();
            PCPokemons = new List<Pokemon>();
            Position = Vector2i.Zero;
            Inventory = new Dictionary<string, ItemAbstract>();
        }

        public Player(int id, string firstName, string lastName, int age, string description, List<Pokemon> teamPokemons, List<Pokemon> pCPokemons, Vector2i position, Dictionary<string, ItemAbstract> inventory)
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

        public override string ToString()
        {
            return $"Player : {{Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Age: {Age}, Description: {Description} }}";
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

    public class PlayerJsonConverter : JsonConverter<Player>
    {
        public override void WriteJson(JsonWriter writer, Player value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Player");
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(Player.Id));
            serializer.Serialize(writer, value.Id);

            writer.WritePropertyName(nameof(Player.FirstName));
            serializer.Serialize(writer, value.FirstName);

            writer.WritePropertyName(nameof(Player.LastName));
            serializer.Serialize(writer, value.LastName);

            writer.WritePropertyName(nameof(Player.Age));
            serializer.Serialize(writer, value.Age);

            writer.WritePropertyName(nameof(Player.Description));
            serializer.Serialize(writer, value.Description);

            writer.WritePropertyName("Pokemons");
            writer.WriteStartObject();
            foreach (var pokemon in value.TeamPokemons)
            {
                new PokemonJsonConverter().WriteJson(writer, pokemon, serializer);  
            }
            writer.WriteEndObject();

            writer.WritePropertyName(nameof(Player.Inventory));
            writer.WriteStartObject();
            foreach (var item in value.Inventory)
            {
                writer.WritePropertyName(item.Value.Name);
                serializer.Serialize(writer, item.Value.Quantity);
            }
            writer.WriteEndObject();

            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override Player ReadJson(JsonReader reader, Type objectType, Player existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Player player = new Player();

            // Avancer vers le début de l'objet JSON
            reader.Read();

            // Lire les propriétés de l'objet JSON
            while (reader.TokenType == JsonToken.PropertyName)
            {
                string propertyName = reader.Value.ToString();

                switch (propertyName)
                {
                    case "Player":
                        // Avancer vers le début de l'objet player
                        reader.Read();
                        break;

                    case nameof(Player.Id):
                        // Lire la valeur de la propriété Age
                        reader.Read();
                        player.Id = serializer.Deserialize<int>(reader);
                        break;

                    case nameof(Player.FirstName):
                        // Lire la valeur de la propriété FirstName
                        reader.Read();
                        player.FirstName = serializer.Deserialize<string>(reader);
                        break;

                    case nameof(Player.LastName):
                        // Lire la valeur de la propriété LastName
                        reader.Read();
                        player.LastName = serializer.Deserialize<string>(reader);
                        break;

                    case nameof(Player.Age):
                        // Lire la valeur de la propriété Age
                        reader.Read();
                        player.Age = serializer.Deserialize<int>(reader);
                        break;

                    case nameof(Player.Description):
                        // Lire la valeur de la propriété LastName
                        reader.Read();
                        player.Description = serializer.Deserialize<string>(reader);
                        break;

                    default:
                        // Ignorer les propriétés inconnues si nécessaire
                        reader.Read();
                        reader.Skip();
                        break;
                }
                // Passer à la propriété suivante
                reader.Read();
            }

            return player;
        }
    }
}
