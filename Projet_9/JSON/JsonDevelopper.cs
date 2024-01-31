using NPokemon;
using NEntity;
using Newtonsoft.Json;
using System.IO;

namespace NJSON
{
    public class JsonDevelopper
    {
        public static JsonDevelopper instance;

        public static JsonDevelopper GetInstance()
        {
            if(instance == null)
            {
                instance = new JsonDevelopper();
            }
               return instance;
        }

        public string SerializeObjectToJson(object data)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonString;
        }

        public void SerializeObjectToJsonFile(object obj, string filePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new PlayerJsonConverter());
            serializer.Converters.Add(new PokemonJsonConverter());

            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public bool CreateFile(object data, string pathFile)
        {

            string jsonData = this.SerializeObjectToJson(data);
            if (jsonData != null)
            {
                if (!File.Exists(pathFile))
                {
                    File.Create(pathFile).Close();
                }

                File.WriteAllText(pathFile, jsonData);  
                return true;
            }
            return false;
        }
        public T DeserializeJsonFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Le fichier JSON spécifié n'existe pas.", filePath);
            }
            string jsonString = File.ReadAllText(filePath);
            T obj = JsonConvert.DeserializeObject<T>(jsonString);

            return obj;
        }
    }
}
