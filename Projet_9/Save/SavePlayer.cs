

using NEntity;
using Newtonsoft.Json;
using NJSON;
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
        private string _folderName = "Saves";
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

        public SavePlayer(string playerFirstname, string playerLastname, string playerUid)
        {
            jsonSaver = JsonDevelopper.GetInstance();
            jsonSaver.CreateFolder(_folderName);
            _playerTag = playerFirstname + "_" + playerLastname + "_" + playerUid;
            jsonSaver.CreateFolder(_folderName +"/" + _playerTag);
        }

        public static SavePlayer GetInstance()
        {
            if (instance == null)
            {
                instance = new SavePlayer();
            }
            return instance;
        }

        public static SavePlayer GetInstance(string playerFirstname, string playerLastname, string playerUid)
        {
            if (instance == null)
            {
                instance = new SavePlayer(playerFirstname, playerLastname, playerUid);
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
        public T ReadTestSave<T>(int indexSave)
        {
            if (_actualIndex != indexSave)
            {
                _actualIndex = ((indexSave - 1) % 3) + 1;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new PlayerJsonConverter());

            using (StreamReader sr = new StreamReader(_folderName + "/" + _fileName + _actualIndex + _fileType))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(reader);
            }
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
}
