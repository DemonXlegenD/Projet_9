

using NEntity;
using Newtonsoft.Json;
using NJSON;
using NPokemon;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSave
{
    public class SavePlayer
    {
        private static SavePlayer instance;

        private JsonDevelopper jsonSaver;
        private int _actualIndex = 1;
        private string _fileName = "Save";
        private string _folderName = "Saves/Users";
        private string _fileType = ".json";
        private string _playerTag = string.Empty;

        public string Name
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public SavePlayer()
        {
            jsonSaver = JsonDevelopper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
        }

        public SavePlayer(string playerUid, bool needFolders = true)
        {
            jsonSaver = JsonDevelopper.GetInstance();
            _playerTag = playerUid;

            if(needFolders)
            {
                jsonSaver.CreateFolder(_folderName);
                jsonSaver.CreateFolder(_folderName +"/" + _playerTag);
            }  
        }

        public static SavePlayer GetInstance()
        {
            if (instance == null)
            {
                instance = new SavePlayer();
            }
            return instance;
        }

        public static SavePlayer GetInstance(string playerUid, bool needFolders)
        {
            if (instance == null)
            {
                instance = new SavePlayer(playerUid, needFolders);
            }
            return instance;
        }

        public static SavePlayer GetInstance(string playerUid)
        {
            if (instance == null)
            {
                instance = new SavePlayer(playerUid);
            }
            return instance;
        }

        public void ApplySave(object data)
        {
            jsonSaver.CreateFile(data, _folderName + "/" + _fileName + _actualIndex + _fileType);
        }
        public void ApplySave(object data, int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            jsonSaver.CreateFile(data, _folderName + "/" + _fileName + _actualIndex + _fileType);
        }

        public object ReadSave()
        {
            object objet = jsonSaver.DeserializeJsonFromFile<object>(_folderName + "/" + _fileName + _actualIndex + _fileType);
            return objet;
        }

        public object ReadSave(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            object objet = jsonSaver.DeserializeJsonFromFile<object>(_folderName + "/" + _fileName + _actualIndex + _fileType);
            return objet;
        }
        public T ReadSave<T>()
        {
            T objet = jsonSaver.DeserializeJsonFromFile<T>(_folderName + "/" + _fileName + _actualIndex + _fileType);
            return objet;
        }


        public T ReadSave<T>(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            T objet = jsonSaver.DeserializeJsonFromFile<T>(_folderName + "/" + _fileName + _actualIndex + _fileType);
            return objet;
        }
        public T ReadTestSave<T>(List<JsonConverter> converters)
        {
            return jsonSaver.DeserializeObjectToJsonFile<T>(_folderName + "/" + _playerTag + "/" + _fileName + _actualIndex + _fileType, converters);
        }

        public T ReadTestSave<T>(int indexSave, List<JsonConverter> converters)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }    
            return jsonSaver.DeserializeObjectToJsonFile<T>(_folderName + "/" + _playerTag + "/" + _fileName + _actualIndex + _fileType, converters);
        }

        public void WriteSave(object data, int indexSave, List<JsonConverter> converters)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            jsonSaver.SerializeObjectToJsonFile(data, _folderName + "/" + _playerTag + "/" + _fileName +  _actualIndex + _fileType, converters);
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

            writer.WritePropertyName(nameof(Player.TeamPokemons));
            writer.WriteStartArray();
            foreach (var pokemon in value.TeamPokemons)
            {
                new PokemonJsonConverter().WriteJson(writer, pokemon, serializer);
            }
            writer.WriteEndArray();

            writer.WritePropertyName(nameof(Player.PCPokemons));
            writer.WriteStartArray();
            foreach (var pokemon in value.PCPokemons)
            {
                new PokemonJsonConverter().WriteJson(writer, pokemon, serializer);
            }
            writer.WriteEndArray();

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
                        player.Id = serializer.Deserialize<string>(reader);
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

                    case nameof(Player.TeamPokemons):
                        player.TeamPokemons = new List<Pokemon>();
                        while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                        {
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                // Lire un objet Pokémon en utilisant votre méthode ReadJson personnalisée pour Pokemon
                                Pokemon pokemon = serializer.Deserialize<Pokemon>(reader);
                                player.TeamPokemons.Add(pokemon);
                            }
                        }
                        break;
                    case nameof(Player.PCPokemons):
                        player.PCPokemons = new List<Pokemon>();
                        while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                        {
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                // Lire un objet Pokémon en utilisant votre méthode ReadJson personnalisée pour Pokemon
                                Pokemon pokemon = serializer.Deserialize<Pokemon>(reader);
                                player.PCPokemons.Add(pokemon);
                            }
                        }
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
